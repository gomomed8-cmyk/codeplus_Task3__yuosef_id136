using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Commands.CancelOrder
{
    public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
    {
        private readonly IOrderService _orderService;
        public CancelOrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
             await _orderService.CancelOrderAsync(request.OrderId, cancellationToken);
            return Unit.Value;

        }
    }
}
