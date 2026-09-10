using ECommerce.Application.DTOs.Chat;
using MediatR;

namespace ECommerce.Application.Features.Chat.Commands.CreateConversation;

public sealed record CreateConversationCommand(
    CreateConversationRequest Request
) : IRequest<ConversationResponse>;