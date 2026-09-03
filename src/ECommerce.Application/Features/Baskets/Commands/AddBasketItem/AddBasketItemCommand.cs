using ECommerce.Application.DTOs.Baskets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.AddBasketItem
{
    public sealed record AddBasketItemCommand(
       int CustomerId,
       AddBasketItemRequest Request)
       : IRequest;
}
