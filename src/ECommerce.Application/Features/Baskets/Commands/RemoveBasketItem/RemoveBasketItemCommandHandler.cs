using ECommerce.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.RemoveBasketItem
{
    public sealed class RemoveBasketItemCommandHandler(
    IBasketService basketService)
    : IRequestHandler<RemoveBasketItemCommand>
    {
        public async Task Handle(
            RemoveBasketItemCommand request,
            CancellationToken cancellationToken)
        {
            await basketService.RemoveItemAsync(
                request.CustomerId,
                request.ProductId,
                cancellationToken);
        }
    }
}
