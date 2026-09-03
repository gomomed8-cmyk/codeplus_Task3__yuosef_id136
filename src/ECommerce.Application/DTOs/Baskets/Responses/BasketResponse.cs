
namespace ECommerce.Application.DTOs.Baskets;

public sealed record BasketResponse(
    int Id,
    int CustomerId,
    IReadOnlyCollection<BasketItemResponse> Items
);
