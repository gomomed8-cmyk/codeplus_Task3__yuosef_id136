using ECommerce.Domain.Common;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Entities;

public sealed class BasketItem : Entity
{
    public int BasketId { get; private set; }
    public Basket? Basket { get; private set; }

    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    public int Quantity { get; private set; }

    public DateTime AddedAt { get; private set; }

    public DateTime? ReminderSentAt { get; private set; }

    private BasketItem() { }

    public BasketItem(int productId, int quantity)
    {
        if (productId <= 0)
            throw new DomainException(
                "Invalid Product ID.");

        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        ProductId = productId;
        Quantity = quantity;
        AddedAt = DateTime.UtcNow;
    }

    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        Quantity += quantity;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException(
                "Quantity must be greater than zero.");

        Quantity = quantity;
    }
    public void MarkReminderAsSent()
    {
        if (ReminderSentAt != null)
            return;

        ReminderSentAt = DateTime.UtcNow;
    }
}