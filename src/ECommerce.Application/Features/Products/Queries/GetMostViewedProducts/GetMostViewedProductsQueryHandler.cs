using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces.Repositories;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetMostViewedProducts;

public sealed class GetMostViewedProductsQueryHandler
    : IRequestHandler<
        GetMostViewedProductsQuery,
        IReadOnlyList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetMostViewedProductsQueryHandler(
        IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductResponse>> Handle(
        GetMostViewedProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products =
            await _productRepository.GetAllAsync(cancellationToken);

        return products
            .OrderByDescending(p => p.ViewCount)
            .Take(request.Count)
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.SKU,
                p.Price,
                p.StockQuantity,
                p.ViewCount))
            .ToList();
    }
}