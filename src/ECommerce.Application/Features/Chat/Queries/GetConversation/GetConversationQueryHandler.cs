using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Interfaces.Services;
using MediatR;

namespace ECommerce.Application.Features.Chat.Queries.GetConversation;

public sealed class GetConversationQueryHandler(
    IChatService chatService)
    : IRequestHandler<GetConversationQuery, ConversationResponse>
{
    public async Task<ConversationResponse> Handle(
        GetConversationQuery request,
        CancellationToken cancellationToken)
    {
        return await chatService.GetConversationAsync(
            request.Id,
            cancellationToken);
    }
}