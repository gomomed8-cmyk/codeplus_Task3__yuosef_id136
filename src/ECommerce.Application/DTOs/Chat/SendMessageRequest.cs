namespace ECommerce.Application.DTOs.Chat;

public sealed record SendMessageRequest(
    int SenderId,
    string Content);