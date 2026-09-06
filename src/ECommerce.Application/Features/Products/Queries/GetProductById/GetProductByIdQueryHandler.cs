using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces.Services;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductService _productService;
    private readonly IProductViewTracker _productViewTracker;

    public GetProductByIdQueryHandler(
        IProductService productService,
        IProductViewTracker productViewTracker)
    {
        _productService = productService;
        _productViewTracker = productViewTracker;
    }

    public async Task<ProductResponse> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _productService.GetProductByIdAsync(
            request.Id,
            cancellationToken);

        await _productViewTracker.IncrementAsync(request.Id);

        return product;
    }
}