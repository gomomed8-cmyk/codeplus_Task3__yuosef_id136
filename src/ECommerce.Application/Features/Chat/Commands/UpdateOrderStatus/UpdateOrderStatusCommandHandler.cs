using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using MediatR;

namespace ECommerce.Application.Features.Orders.Commands.UpdateOrderStatus;

public sealed class UpdateOrderStatusCommandHandler
    : IRequestHandler<UpdateOrderStatusCommand, OrderStatusUpdateResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderStatusCommandHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderStatusUpdateResponse> Handle(
        UpdateOrderStatusCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        order.UpdateStatus(request.Status);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new OrderStatusUpdateResponse(
            order.Id,
            order.Status,
            DateTime.UtcNow);
    }
}