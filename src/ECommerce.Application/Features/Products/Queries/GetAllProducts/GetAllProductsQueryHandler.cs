using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductResponse>>
    {
        private readonly IProductService _productService;
        public GetAllProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IReadOnlyList<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
           return await _productService.GetAllProductsAsync(cancellationToken);
        }
    }
}
