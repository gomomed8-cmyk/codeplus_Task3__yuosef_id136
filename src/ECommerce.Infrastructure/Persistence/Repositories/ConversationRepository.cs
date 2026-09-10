using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public sealed class ConversationRepository(AppDbContext context)
    : IConversationRepository
{
    public async Task<Conversation?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Conversations
            .Include(x => x.Messages)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AddAsync(
        Conversation conversation,
        CancellationToken cancellationToken = default)
    {
        await context.Conversations.AddAsync(
            conversation,
            cancellationToken);
    }

    public void Update(Conversation conversation)
    {
        context.Conversations.Update(conversation);
    }
}