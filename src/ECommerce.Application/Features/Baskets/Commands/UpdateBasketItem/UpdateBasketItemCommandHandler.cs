using ECommerce.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.UpdateBasketItem
{
    public sealed class UpdateBasketItemCommandHandler(
    IBasketService basketService)
    : IRequestHandler<UpdateBasketItemCommand>
    {
        public async Task Handle(
            UpdateBasketItemCommand request,
            CancellationToken cancellationToken)
        {
            await basketService.UpdateItemQuantityAsync(
                request.CustomerId,
                request.ProductId,
                request.Request.Quantity,
                cancellationToken);
        }
    }
}
