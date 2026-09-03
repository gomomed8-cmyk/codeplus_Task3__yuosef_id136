using ECommerce.Application.Contracts.Services;
using ECommerce.Application.DTOs.Baskets;
using ECommerce.Application.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Queries.GetBasket
{
    public sealed class GetBasketQueryHandler(
     IBasketService basketService)
     : IRequestHandler<GetBasketQuery, BasketResponse>
    {
        public async Task<BasketResponse> Handle(
            GetBasketQuery request,
            CancellationToken cancellationToken)
        {
            var basket = await basketService.GetOrCreateBasketAsync(
                request.CustomerId,
                cancellationToken);

            return basket.ToResponse();
        }
    }
}
