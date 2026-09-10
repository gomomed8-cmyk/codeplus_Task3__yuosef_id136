using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.Hubs;

public class OrderTrackingHub : Hub
{
    public async Task JoinOrder(int orderId)
    {
        var groupName = $"order-{orderId}";

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            groupName);
    }
}