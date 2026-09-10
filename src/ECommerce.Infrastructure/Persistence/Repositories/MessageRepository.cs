using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Domain.Entities;

namespace ECommerce.Infrastructure.Persistence.Repositories;

public sealed class MessageRepository(AppDbContext context)
    : IMessageRepository
{
    public async Task AddAsync(
        Message message,
        CancellationToken cancellationToken = default)
    {
        await context.Messages.AddAsync(
            message,
            cancellationToken);
    }
}