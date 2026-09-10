using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.Services;

public sealed class ChatService(
    IConversationRepository conversationRepository,
    IMessageRepository messageRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : IChatService
{
    public async Task<ConversationResponse> CreateConversationAsync(
        CreateConversationRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.CustomerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        var customer = await customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken);

        if (customer == null)
            throw new NotFoundException(
                nameof(Customer),
                request.CustomerId);

        var conversation = new Conversation(
            request.CustomerId);

        await conversationRepository.AddAsync(
            conversation,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new ConversationResponse(
            conversation.Id,
            conversation.CustomerId,
            conversation.Status.ToString(),
            conversation.CreatedAt,
            conversation.ClosedAt,
            new List<MessageResponse>());
    }

    public async Task<ConversationResponse> GetConversationAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        if (id <= 0)
            throw new DomainException(
                "Invalid Conversation ID.");

        var conversation = await conversationRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (conversation == null)
            throw new NotFoundException(
                nameof(Conversation),
                id);

        var messages = conversation.Messages
            .OrderBy(x => x.SentAt)
            .Select(x => new MessageResponse(
                x.Id,
                x.SenderId,
                x.SenderType.ToString(),
                x.Content,
                x.SentAt,
                x.IsRead))
            .ToList();

        return new ConversationResponse(
            conversation.Id,
            conversation.CustomerId,
            conversation.Status.ToString(),
            conversation.CreatedAt,
            conversation.ClosedAt,
            messages);
    }

    public async Task<MessageResponse> SendMessageAsync(
        int conversationId,
        SendMessageRequest request,
        CancellationToken cancellationToken = default)
    {
        if (conversationId <= 0)
            throw new DomainException(
                "Invalid Conversation ID.");

        if (request.SenderId <= 0)
            throw new DomainException(
                "Invalid Sender ID.");

        if (string.IsNullOrWhiteSpace(request.Content))
            throw new DomainException(
                "Message content cannot be empty.");

        var conversation = await conversationRepository.GetByIdAsync(
            conversationId,
            cancellationToken);

        if (conversation == null)
            throw new NotFoundException(
                nameof(Conversation),
                conversationId);

        if (conversation.Status == ConversationStatus.Closed)
            throw new DomainException(
                "Cannot send a message to a closed conversation.");

        var customer = await customerRepository.GetByIdAsync(
            request.SenderId,
            cancellationToken);

        if (customer == null)
            throw new NotFoundException(
                nameof(Customer),
                request.SenderId);

        if (conversation.CustomerId != request.SenderId)
            throw new DomainException(
                "Customer does not belong to this conversation.");

        var message = new Message(
            conversationId,
            request.SenderId,
            MessageType.Customer,
            request.Content);

        await messageRepository.AddAsync(
            message,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new MessageResponse(
            message.Id,
            message.SenderId,
            message.SenderType.ToString(),
            message.Content,
            message.SentAt,
            message.IsRead);
    }
}