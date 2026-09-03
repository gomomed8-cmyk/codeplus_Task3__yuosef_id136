
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class BasketRepository(AppDbContext context)
    : IBasketRepository
{
    public async Task<Basket?> GetByCustomerIdAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        return await context.Baskets
            .Include(x => x.Items)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(
                x => x.CustomerId == customerId,
                cancellationToken);
    }

    public async Task AddAsync(
        Basket basket,
        CancellationToken cancellationToken = default)
    {
        await context.Baskets.AddAsync(
            basket,
            cancellationToken);
    }

    public void Update(Basket basket)
    {
        context.Baskets.Update(basket);
    }

    public void Delete(Basket basket)
    {
        context.Baskets.Remove(basket);
    }

    public async Task<bool> ExistsAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        return await context.Baskets
            .AnyAsync(
                x => x.CustomerId == customerId,
                cancellationToken);
    }

    public async Task<List<BasketItem>> GetItemsForReminderAsync(
    DateTime reminderCutoffDate,
    DateTime expirationCutoffDate,
    CancellationToken cancellationToken = default)
    {
        return await context.BasketItems
            .Include(x => x.Basket)
                .ThenInclude(x => x!.Customer)
            .Include(x => x.Product)
            .Where(x =>
                x.AddedAt <= reminderCutoffDate &&
                x.AddedAt > expirationCutoffDate &&
                x.ReminderSentAt == null)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<BasketItem>> GetExpiredItemsAsync(DateTime cutoffDate, CancellationToken cancellationToken = default)
    {
        return await context.BasketItems
        .Include(x => x.Basket)
            .ThenInclude(x => x!.Customer)
        .Include(x => x.Basket)
            .ThenInclude(x => x!.Items)
        .Include(x => x.Product)
        .Where(x => x.AddedAt <= cutoffDate)
        .ToListAsync(cancellationToken);
    }
}
