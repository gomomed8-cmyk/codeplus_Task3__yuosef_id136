using ECommerce.Application.DTOs.Orders;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;

public sealed record UpdateOrderStatusCommand(
    int OrderId,
    OrderStatus Status
) : IRequest<OrderStatusUpdateResponse>;