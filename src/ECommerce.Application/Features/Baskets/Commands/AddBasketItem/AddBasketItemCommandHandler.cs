using ECommerce.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.AddBasketItem
{
    public sealed class AddBasketItemCommandHandler(
    IBasketService basketService)
    : IRequestHandler<AddBasketItemCommand>
    {
        public async Task Handle(
            AddBasketItemCommand request,
            CancellationToken cancellationToken)
        {
            await basketService.AddItemAsync(
                request.CustomerId,
                request.Request.ProductId,
                request.Request.Quantity,
                cancellationToken);
        }
    }
}
