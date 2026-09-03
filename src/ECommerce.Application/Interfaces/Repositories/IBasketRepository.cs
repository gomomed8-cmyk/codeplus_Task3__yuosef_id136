using ECommerce.Domain.Entities;

namespace ECommerce.Application.Contracts.Persistence;

public interface IBasketRepository
{
    Task<Basket?> GetByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Basket basket,
        CancellationToken cancellationToken = default);

    void Update(Basket basket);

    void Delete(Basket basket);

    Task<bool> ExistsAsync(
        int customerId,
        CancellationToken cancellationToken = default);

    Task<List<BasketItem>> GetItemsForReminderAsync(
    DateTime reminderCutoffDate,
    DateTime expirationCutoffDate,
    CancellationToken cancellationToken = default);

    Task<List<BasketItem>> GetExpiredItemsAsync(
        DateTime cutoffDate,
        CancellationToken cancellationToken = default);
}