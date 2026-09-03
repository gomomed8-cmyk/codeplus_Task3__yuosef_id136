
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Baskets;

public sealed record AddBasketItemRequest(
    [Range(1, int.MaxValue)] int ProductId,
    [Range(1, int.MaxValue)] int Quantity
);

