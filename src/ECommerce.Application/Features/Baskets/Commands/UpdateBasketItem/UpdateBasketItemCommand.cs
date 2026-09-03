using ECommerce.Application.DTOs.Baskets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Baskets.Commands.UpdateBasketItem
{
    public sealed record UpdateBasketItemCommand(
     int CustomerId,
     int ProductId,
     UpdateBasketItemRequest Request)
     : IRequest;
}
