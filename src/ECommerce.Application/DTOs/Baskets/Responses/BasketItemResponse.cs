
namespace ECommerce.Application.DTOs.Baskets;

public sealed record BasketItemResponse(
    int ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal
);
