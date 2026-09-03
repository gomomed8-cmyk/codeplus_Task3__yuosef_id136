using ECommerce.Application.DTOs.Orders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Orders.Queries.GetCustomerOrders
{
    public sealed record GetCustomerOrdersQuery(int CustomerId) : IRequest<IReadOnlyList<OrderResponse>>;

}
