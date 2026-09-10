using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.Orders;

public sealed record OrderStatusUpdateResponse(
    int OrderId,
    OrderStatus Status,
    DateTime UpdatedAt
);