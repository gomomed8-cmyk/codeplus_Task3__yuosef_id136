using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Chat.Commands.SendMessage;

public sealed class SendMessageCommandHandler
    : IRequestHandler<SendMessageCommand, MessageResponse>
{
    private readonly IConversationRepository _conversationRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(
        IConversationRepository conversationRepository,
        IMessageRepository messageRepository,
        IUnitOfWork unitOfWork)
    {
        _conversationRepository = conversationRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageResponse> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationRepository.GetByIdAsync(
            request.ConversationId,
            cancellationToken);

        if (conversation is null)
            throw new KeyNotFoundException("Conversation not found.");

        var message = new Message(
            request.ConversationId,
            request.SenderId,
            Domain.Enums.MessageType.Customer,
            request.Content);

        await _messageRepository.AddAsync(
            message,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new MessageResponse(
            message.Id,
            message.SenderId,
            message.SenderType.ToString(),
            message.Content,
            message.SentAt,
            message.IsRead);
    }
}