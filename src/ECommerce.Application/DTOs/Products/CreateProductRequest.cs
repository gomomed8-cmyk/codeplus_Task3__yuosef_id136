namespace ECommerce.Application.DTOs.Products;

public record UpdateProduct(string Name, string SKU, decimal Price, int StockQuantity);
