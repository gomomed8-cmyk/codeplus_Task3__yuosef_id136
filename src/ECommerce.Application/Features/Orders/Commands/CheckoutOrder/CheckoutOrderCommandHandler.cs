using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CheckoutOrder
{
    public sealed class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, CheckoutOrderResponse>
    {
        private readonly IOrderService _orderService;
        public CheckoutOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<CheckoutOrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
          return await  _orderService.CheckoutAsync(request.Request, cancellationToken);
        }
    }
}
