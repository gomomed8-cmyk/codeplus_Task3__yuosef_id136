using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Features.Orders.Commands.CancelOrder;
using ECommerce.Application.Features.Orders.Commands.CheckoutOrder;
using ECommerce.Application.Features.Orders.Queries.GetCustomerOrders;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public sealed class OrdersController : BaseApiController
{
    private readonly ISender _sender;

    public OrdersController(
        ISender sender)
    {
        _sender = sender;
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

        return Ok(new { message = "Order cancelled successfully." });
    }
}