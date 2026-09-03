using ECommerce.Application.DTOs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(int Id, UpdateProductRequest Request) : IRequest<Unit>
    {
    }
}
