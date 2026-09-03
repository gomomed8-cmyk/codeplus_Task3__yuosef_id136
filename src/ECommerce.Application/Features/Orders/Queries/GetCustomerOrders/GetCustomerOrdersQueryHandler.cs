using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Queries.GetCustomerOrders
{
    public sealed class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, IReadOnlyList<OrderResponse>>
    {
        private readonly IOrderService _orderService;
        public GetCustomerOrdersQueryHandler(IOrderService orderService)
        { 
        _orderService = orderService;
        }
        public async Task<IReadOnlyList<OrderResponse>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
           return await _orderService.GetCustomerOrdersAsync(request.CustomerId, cancellationToken);
        }
    }

}
