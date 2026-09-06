using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.BackgroundJobs;

public sealed class ProductViewWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ProductViewWorker> _logger;

    public ProductViewWorker(
        IServiceScopeFactory scopeFactory,
        ILogger<ProductViewWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await FlushViewsAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An error occurred while flushing product views.");
            }
        }
    }

    private async Task FlushViewsAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();

        var productRepository =
            scope.ServiceProvider.GetRequiredService<IProductRepository>();

        var viewTracker =
            scope.ServiceProvider.GetRequiredService<IProductViewTracker>();

        var products = await productRepository.GetAllAsync(cancellationToken);

        foreach (var product in products)
        {
            var views = await viewTracker.GetAndResetAsync(product.Id);

            if (views <= 0)
                continue;

            product.UpdateViewCount(product.ViewCount + (int)views);

            productRepository.Update(product);
        }

        await scope.ServiceProvider
            .GetRequiredService<IUnitOfWork>()
            .SaveChangesAsync(cancellationToken);
    }
}