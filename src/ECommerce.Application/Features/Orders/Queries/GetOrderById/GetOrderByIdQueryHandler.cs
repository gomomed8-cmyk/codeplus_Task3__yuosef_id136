using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse>
    {
        private readonly IOrderService _orderService;
        public GetOrderByIdQueryHandler(IOrderService orderService)
        {
            _orderService =orderService;
        }
        public async Task<OrderResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
             return await _orderService.GetOrderByIdAsync(request.Id, cancellationToken);
        }
    }
}


