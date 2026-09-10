using ECommerce.API.Hubs;
using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Features.Orders.Commands.CancelOrder;
using ECommerce.Application.Features.Orders.Commands.CheckoutOrder;
using ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;
using ECommerce.Application.Features.Orders.Queries.GetCustomerOrders;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.Controllers;

public sealed class OrdersController : BaseApiController
{
    private readonly ISender _sender;
    private readonly IHubContext<OrderTrackingHub> _hubContext;

    public OrdersController(
        ISender sender,
        IHubContext<OrderTrackingHub> hubContext)
    {
        _sender = sender;
        _hubContext = hubContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var order = await _sender.Send(
            new GetOrderByIdQuery(id),
            cancellationToken);

        return Ok(order);
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetCustomerOrders(
        int customerId,
        CancellationToken cancellationToken)
    {
        var orders = await _sender.Send(
            new GetCustomerOrdersQuery(customerId),
            cancellationToken);

        return Ok(orders);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutOrderResponse>> Checkout(
        [FromBody] CreateOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new CheckoutOrderCommand(request),
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("cancel/{id:int}")]
    public async Task<IActionResult> Cancel(
        int id,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new CancelOrderCommand(id),
            cancellationToken);

        return Ok(new
        {
            message = "Order cancelled successfully."
        });
    }
    [Authorize]
    [HttpPut("{id:int}/status")]
    public async Task<IActionResult> UpdateStatus(
        int id,
        OrderStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new UpdateOrderStatusCommand(id, status),
            cancellationToken);

        await _hubContext.Clients
            .Group($"order-{result.OrderId}")
            .SendAsync(
                "OrderStatusUpdated",
                result,
                cancellationToken);

        return Ok(result);
    }
}