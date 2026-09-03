using ECommerce.Application.DTOs.Baskets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Queries.GetBasket
{
    public sealed record GetBasketQuery(int CustomerId)
     : IRequest<BasketResponse>;
}
