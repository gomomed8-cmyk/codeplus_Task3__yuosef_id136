using ECommerce.Application.DTOs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries.GetMostViewedProducts
{
    public sealed record GetMostViewedProductsQuery(int Count = 10)
       : IRequest<IReadOnlyList<ProductResponse>>;
}
