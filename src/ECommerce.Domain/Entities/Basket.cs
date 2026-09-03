using ECommerce.Domain.Common;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public sealed class Basket : Entity
{
    public int CustomerId { get; private set; }
    public Customer? Customer { get; private set; }

    private readonly List<BasketItem> _items = new();
    public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();

    private Basket() { }

    public Basket(int customerId)
    {
        if (customerId <= 0)
            throw new DomainException("Invalid Customer ID.");

        CustomerId = customerId;
    }

    public void AddItem(int productId, int quantity)
    {
        if (productId <= 0)
            throw new DomainException("Invalid Product ID.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        var existingItem = _items
            .FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
            return;
        }

        _items.Add(new BasketItem(productId, quantity));
    }

    public void UpdateItemQuantity(int productId, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        var item = _items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            throw new DomainException(
                "Product does not exist in the basket.");

        item.UpdateQuantity(quantity);
    }

    public void RemoveItem(int productId)
    {
        var item = _items
            .FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            throw new DomainException(
                "Product does not exist in the basket.");

        _items.Remove(item);
    }

    public void RemoveItem(BasketItem item)
    {
        if (item == null)
            throw new DomainException(
                "Basket item cannot be null.");

        _items.Remove(item);
    }

    public void Clear()
    {
        _items.Clear();
    }
}