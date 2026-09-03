using ECommerce.Application.Interfaces.Services;
using MediatR;


namespace ECommerce.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductService _productService;
        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
          await _productService.UpdateProductAsync(request.Id, request.Request, cancellationToken);
            return Unit.Value;
        }
    }
}
