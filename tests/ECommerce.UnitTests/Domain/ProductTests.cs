using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Xunit;

namespace ECommerce.UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void DeductStock_ValidQuantity_DecreasesStock()
    {
        var product = new Product("Test Product", "SKU-01", 100m, 10);
        product.DeductStock(3);
        Assert.Equal(7, product.StockQuantity);
    }

    [Fact]
    public void DeductStock_ExceedingAvailableStock_ThrowsInsufficientStockException()
    {
        var product = new Product("Test Product", "SKU-01", 100m, 5);
        Assert.Throws<InsufficientStockException>(() => product.DeductStock(10));
    }
}
