using ECommerce.Application.DTOs.Orders;
using MediatR;


namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
        public sealed record GetOrderByIdQuery(int Id) : IRequest<OrderResponse>;
}
