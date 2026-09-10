using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Interfaces.Services;
using MediatR;

namespace ECommerce.Application.Features.Chat.Commands.CreateConversation;

public sealed class CreateConversationCommandHandler(
    IChatService chatService)
    : IRequestHandler<CreateConversationCommand, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(
        CreateConversationCommand request,
        CancellationToken cancellationToken)
    {
        return await chatService.CreateConversationAsync(
            request.Request,
            cancellationToken);
    }
}