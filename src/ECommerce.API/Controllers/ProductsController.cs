using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Features.Products.Commands.CreateProduct;
using ECommerce.Application.Features.Products.Commands.DeleteProduct;
using ECommerce.Application.Features.Products.Commands.UpdateProduct;
using ECommerce.Application.Features.Products.Queries.GetAllProducts;
using ECommerce.Application.Features.Products.Queries.GetMostViewedProducts;
using ECommerce.Application.Features.Products.Queries.GetProductById;
using ECommerce.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

public sealed class ProductsController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly ISender _sender;

    public ProductsController(IProductService productService,ISender sender)
    {
        _productService = productService;
        _sender = sender;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _sender.Send
            (new GetAllProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _sender.Send(
            new GetProductByIdQuery(id), cancellationToken);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create([FromBody] UpdateProduct request, CancellationToken cancellationToken)
    {
        var created = await _sender.Send(
            new CreateProductCommand(request), cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(new UpdateProductCommand(id, request), cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _sender.Send(
            new DeleteProductCommand(id), cancellationToken);
        return NoContent();
    }
    [HttpGet("most-viewed")]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetMostViewed(
    [FromQuery] int count = 10,
    CancellationToken cancellationToken = default)
    {
        if (count <= 0 || count > 100)
            return BadRequest("Count must be between 1 and 100.");

        var products = await _sender.Send(
            new GetMostViewedProductsQuery(count),
            cancellationToken);

        return Ok(products);
    }
}
