
using ECommerce.Application.DTOs.Baskets;
using ECommerce.Application.Features.Baskets.Commands.AddBasketItem;
using ECommerce.Application.Features.Baskets.Commands.ClearBasket;
using ECommerce.Application.Features.Baskets.Commands.RemoveBasketItem;
using ECommerce.Application.Features.Baskets.Commands.UpdateBasketItem;
using ECommerce.Application.Features.Baskets.Queries.GetBasket;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public sealed class BasketController : BaseApiController
{
    private readonly ISender _sender;

    public BasketController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<BasketResponse>> GetBasket(
        int customerId,
        CancellationToken cancellationToken)
    {
        var basket = await _sender.Send(
            new GetBasketQuery(customerId),
            cancellationToken);

        return Ok(basket);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem(
        int customerId,
        [FromBody] AddBasketItemRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new AddBasketItemCommand(
                customerId,
                request),
            cancellationToken);

        return NoContent();
    }

    [HttpPut("items/{productId:int}")]
    public async Task<IActionResult> UpdateItem(
        int customerId,
        int productId,
        [FromBody] UpdateBasketItemRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new UpdateBasketItemCommand(
                customerId,
                productId,
                request),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("items/{productId:int}")]
    public async Task<IActionResult> RemoveItem(
        int customerId,
        int productId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new RemoveBasketItemCommand(
                customerId,
                productId),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear(
        int customerId,
        CancellationToken cancellationToken)
    {
        await _sender.Send(
            new ClearBasketCommand(customerId),
            cancellationToken);

        return NoContent();
    }
}
