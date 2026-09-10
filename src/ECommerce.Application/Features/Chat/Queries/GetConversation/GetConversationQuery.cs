using ECommerce.Application.DTOs.Chat;
using MediatR;

namespace ECommerce.Application.Features.Chat.Queries.GetConversation;

public sealed record GetConversationQuery(
    int Id
) : IRequest<ConversationResponse>;