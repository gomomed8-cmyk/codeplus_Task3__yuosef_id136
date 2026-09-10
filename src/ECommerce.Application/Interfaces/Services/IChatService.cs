using ECommerce.Application.DTOs.Chat;

namespace ECommerce.Application.Interfaces.Services;

public interface IChatService
{
    Task<ConversationResponse> CreateConversationAsync(
        CreateConversationRequest request,
        CancellationToken cancellationToken = default);

    Task<ConversationResponse> GetConversationAsync(
        int id,
        CancellationToken cancellationToken = default);
    Task<MessageResponse> SendMessageAsync(
    int conversationId,
    SendMessageRequest request,
    CancellationToken cancellationToken = default);
}