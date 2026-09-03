
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Baskets;

public sealed record UpdateBasketItemRequest(
    [Range(1, int.MaxValue)] int Quantity
);
