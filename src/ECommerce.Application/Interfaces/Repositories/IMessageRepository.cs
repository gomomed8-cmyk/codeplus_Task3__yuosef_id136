using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

public interface IMessageRepository
{
    Task AddAsync(
        Message message,
        CancellationToken cancellationToken = default);
}