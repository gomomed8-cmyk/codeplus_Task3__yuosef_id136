using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Features.Chat.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace ECommerce.API.Hubs;

public class ChatHub(
    ISender sender) : Hub
{
    public async Task JoinConversation(int conversationId)
    {
        var groupName = $"conversation-{conversationId}";

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            groupName);
    }

    public async Task SendMessage(
        int conversationId,
        int senderId,
        string content)
    {
        var command = new SendMessageCommand(
            conversationId,
            senderId,
            content);

        var message = await sender.Send(command);

        var groupName = $"conversation-{conversationId}";

        await Clients.Group(groupName)
            .SendAsync("ReceiveMessage", message);
    }
}