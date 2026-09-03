using ECommerce.Application.DTOs.Baskets;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings;

public static class BasketMappings
{
    public static BasketResponse ToResponse(this Basket basket)
    {
        var items = basket.Items
            .Select(i => new BasketItemResponse(
                i.ProductId,
                i.Product?.Name ?? string.Empty,
                i.Quantity,
                i.Product?.Price ?? 0,
                i.Quantity * (i.Product?.Price ?? 0)
            ))
            .ToList()
            .AsReadOnly();

        return new BasketResponse(
            basket.Id,
            basket.CustomerId,
            items
        );
    }
}
