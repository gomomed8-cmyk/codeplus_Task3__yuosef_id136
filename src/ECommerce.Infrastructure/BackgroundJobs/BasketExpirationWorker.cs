using ECommerce.Application.Contracts.Services;
using ECommerce.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.BackgroundJobs;

public sealed class BasketExpirationWorker(
    IServiceScopeFactory scopeFactory,
    ILogger<BasketExpirationWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var expirationService =
                    scope.ServiceProvider
                        .GetRequiredService<IBasketExpirationService>();

                await expirationService.ProcessAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error while processing basket expiration.");
            }

            await Task.Delay(
                TimeSpan.FromHours(1),
                stoppingToken);
        }
    }
}