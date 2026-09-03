using ECommerce.Application.Contracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.ClearBasket
{
    public sealed class ClearBasketCommandHandler(
    IBasketService basketService)
    : IRequestHandler<ClearBasketCommand>
    {
        public async Task Handle(
            ClearBasketCommand request,
            CancellationToken cancellationToken)
        {
            await basketService.ClearBasketAsync(
                request.CustomerId,
                cancellationToken);
        }
    }
}
