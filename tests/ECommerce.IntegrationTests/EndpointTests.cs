using System.Net;
using System.Net.Http.Json;
using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.DTOs.Products;
using Xunit;

namespace ECommerce.IntegrationTests;

public class EndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public EndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllProducts_ReturnsSuccessAndProductList()
    {
        var response = await _client.GetAsync("/api/products");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var products = await response.Content.ReadFromJsonAsync<List<ProductResponse>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }

    [Fact]
    public async Task CheckoutOrder_ValidPayload_ReturnsPaidOrder()
    {
        var request = new CreateOrderRequest(
            CustomerId: 1,
            Items: new List<OrderItemRequest> { new(ProductId: 1, Quantity: 1) },
            CouponCode: "WELCOME10"
        );

        var response = await _client.PostAsJsonAsync("/api/orders/checkout", request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<CheckoutOrderResponse>();
        Assert.NotNull(result);
        Assert.Equal("Paid", result.Status);
    }
}
