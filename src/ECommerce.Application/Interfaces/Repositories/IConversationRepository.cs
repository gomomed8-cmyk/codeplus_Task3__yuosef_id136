using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IConversationRepository
{
    Task<Conversation?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Conversation conversation,
        CancellationToken cancellationToken = default);

    void Update(Conversation conversation);
}