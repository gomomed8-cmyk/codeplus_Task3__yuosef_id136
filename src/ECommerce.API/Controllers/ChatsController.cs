using ECommerce.Application.DTOs.Chat;
using ECommerce.Application.Features.Chat.Commands.CreateConversation;
using ECommerce.Application.Features.Chat.Commands.SendMessage;
using ECommerce.Application.Features.Chat.Queries.GetConversation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public class ChatController : BaseApiController
{
    private readonly ISender _sender;

    public ChatController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("conversations")]
    public async Task<ActionResult<ConversationResponse>> CreateConversation(
        CreateConversationRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateConversationCommand(request);

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("conversations/{id}")]
    public async Task<ActionResult<ConversationResponse>> GetConversation(
        int id,
        CancellationToken cancellationToken)
    {
        var query = new GetConversationQuery(id);

        var result = await _sender.Send(
            query,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost("conversations/{conversationId}/messages")]
    public async Task<ActionResult<MessageResponse>> SendMessage(
        int conversationId,
        SendMessageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SendMessageCommand(
            conversationId,
            request.SenderId,
            request.Content);

        var result = await _sender.Send(
            command,
            cancellationToken);

        return Ok(result);
    }
}