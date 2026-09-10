namespace ECommerce.Application.DTOs.Chat;

public sealed record MessageResponse(
    int Id,
    int? SenderId,
    string SenderType,
    string Content,
    DateTime SentAt,
    bool IsRead);