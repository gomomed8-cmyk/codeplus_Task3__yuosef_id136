using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("https://localhost:49698/hubs/order-tracking")
    .WithAutomaticReconnect()
    .Build();

connection.On<object>(
    "OrderStatusUpdated",
    order =>
    {
        Console.WriteLine();
        Console.WriteLine("=== Order Status Updated ===");
        Console.WriteLine(order);
        Console.WriteLine("============================");
        Console.WriteLine();
    });

try
{
    await connection.StartAsync();

    Console.WriteLine("Connected to OrderTrackingHub.");

    await connection.InvokeAsync(
        "JoinOrder",
        1);

    Console.WriteLine("Joined order 1.");
    Console.WriteLine();
    Console.WriteLine("Waiting for order status updates...");
    Console.WriteLine("Change Order 1 status from Swagger.");
    Console.WriteLine("Press ENTER to exit.");

    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
finally
{
    await connection.DisposeAsync();
}