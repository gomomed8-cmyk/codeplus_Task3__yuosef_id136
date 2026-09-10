using ECommerce.Application.DTOs.Chat;
using MediatR;

namespace ECommerce.Application.Features.Chat.Commands.SendMessage;

public sealed record SendMessageCommand(
    int ConversationId,
    int? SenderId,
    string Content
) : IRequest<MessageResponse>;