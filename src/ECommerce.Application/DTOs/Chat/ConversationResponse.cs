namespace ECommerce.Application.DTOs.Chat;

public sealed record ConversationResponse(
    int Id,
    int CustomerId,
    string Status,
    DateTime CreatedAt,
    DateTime? ClosedAt,
    IReadOnlyList<MessageResponse> Messages);