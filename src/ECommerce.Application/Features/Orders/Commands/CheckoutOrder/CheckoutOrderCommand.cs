using ECommerce.Application.DTOs.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CheckoutOrder
{
    public sealed record CheckoutOrderCommand(CreateOrderRequest Request) : IRequest<CheckoutOrderResponse>
    {
    }
}
