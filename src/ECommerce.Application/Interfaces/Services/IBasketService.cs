using ECommerce.Domain.Entities;

namespace ECommerce.Application.Contracts.Services;

public interface IBasketService
{
    Task<Basket> GetOrCreateBasketAsync(
        int customerId,
        CancellationToken cancellationToken = default);

    Task AddItemAsync(
        int customerId,
        int productId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task UpdateItemQuantityAsync(
        int customerId,
        int productId,
        int quantity,
        CancellationToken cancellationToken = default);

    Task RemoveItemAsync(
        int customerId,
        int productId,
        CancellationToken cancellationToken = default);

    Task ClearBasketAsync(
        int customerId,
        CancellationToken cancellationToken = default);
}