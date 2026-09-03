using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.Contracts.Services;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Application.Services;

public sealed class BasketService(
    IBasketRepository basketRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IBasketService
{
    public async Task<Basket> GetOrCreateBasketAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        var basket = await basketRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        if (basket != null)
            return basket;

        basket = new Basket(customerId);

        await basketRepository.AddAsync(
            basket,
            cancellationToken);

        return basket;
    }

    public async Task AddItemAsync(
        int customerId,
        int productId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        var product = await productRepository.GetByIdAsync(
            productId,
            cancellationToken);

        if (product == null)
            throw new NotFoundException(
                nameof(Product),
                productId);

        if (product.StockQuantity < quantity)
            throw new InsufficientStockException(
                product.Name,
                product.StockQuantity,
                quantity);

        var basket = await GetOrCreateBasketAsync(
            customerId,
            cancellationToken);

        basket.AddItem(productId, quantity);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);
    }

    public async Task UpdateItemQuantityAsync(
        int customerId,
        int productId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        var product = await productRepository.GetByIdAsync(
            productId,
            cancellationToken);

        if (product == null)
            throw new NotFoundException(
                nameof(Product),
                productId);

        if (product.StockQuantity < quantity)
            throw new InsufficientStockException(
                product.Name,
                product.StockQuantity,
                quantity);

        var basket = await basketRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        if (basket == null)
            throw new NotFoundException(
                nameof(Basket),
                customerId);

        basket.UpdateItemQuantity(
            productId,
            quantity);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);
    }

    public async Task RemoveItemAsync(
        int customerId,
        int productId,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        var basket = await basketRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        if (basket == null)
            throw new NotFoundException(
                nameof(Basket),
                customerId);

        basket.RemoveItem(productId);

        await unitOfWork.SaveChangesAsync(
            cancellationToken);
    }

    public async Task ClearBasketAsync(
        int customerId,
        CancellationToken cancellationToken = default)
    {
        if (customerId <= 0)
            throw new DomainException(
                "Invalid Customer ID.");

        var basket = await basketRepository.GetByCustomerIdAsync(
            customerId,
            cancellationToken);

        if (basket == null)
            throw new NotFoundException(
                nameof(Basket),
                customerId);

        basket.Clear();

        await unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}