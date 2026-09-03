using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.Orders;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Xunit;

namespace ECommerce.UnitTests.Application;

public class OrderServiceTests
{
    private sealed class FakeProductRepository : IProductRepository
    {
        public List<Product> Products { get; } = new();
        public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct) => Task.FromResult<IReadOnlyList<Product>>(Products);
        public Task<Product?> GetByIdAsync(int id, CancellationToken ct) => Task.FromResult(Products.FirstOrDefault(p => p.Id == id));
        public Task<IReadOnlyList<Product>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct) =>
            Task.FromResult<IReadOnlyList<Product>>(Products.Where(p => ids.Contains(p.Id)).ToList());
        public Task<bool> ExistsBySkuAsync(string sku, CancellationToken ct) => Task.FromResult(Products.Any(p => p.SKU == sku));
        public Task AddAsync(Product product, CancellationToken ct) { Products.Add(product); return Task.CompletedTask; }
        public void Update(Product product) { }
        public void Delete(Product product) { Products.Remove(product); }
    }

    private sealed class FakeCustomerRepository : ICustomerRepository
    {
        public List<Customer> Customers { get; } = new();
        public Task<Customer?> GetByIdAsync(int id, CancellationToken ct) => Task.FromResult(Customers.FirstOrDefault(c => c.Id == id));
        public Task<Customer?> GetByEmailAsync(string email, CancellationToken ct) => Task.FromResult(Customers.FirstOrDefault(c => c.Email == email));
        public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct) => Task.FromResult(Customers.Any(c => c.Email == email));
        public Task AddAsync(Customer customer, CancellationToken ct) { Customers.Add(customer); return Task.CompletedTask; }
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        public List<Order> Orders { get; } = new();
        public List<Coupon> Coupons { get; } = new();
        public Task<Order?> GetByIdAsync(int id, CancellationToken ct) => Task.FromResult(Orders.FirstOrDefault(o => o.Id == id));
        public Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken ct) =>
            Task.FromResult<IReadOnlyList<Order>>(Orders.Where(o => o.CustomerId == customerId).ToList());
        public Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken ct) => Task.FromResult(Coupons.FirstOrDefault(c => c.Code == code));
        public Task AddAsync(Order order, CancellationToken ct) { Orders.Add(order); return Task.CompletedTask; }
    }

    private sealed class FakePaymentGateway : IPaymentGateway
    {
        public Task<PaymentResult> ChargeAsync(string customerEmail, decimal amount, CancellationToken ct)
        {
            return Task.FromResult(new PaymentResult(true, "TX-UNITTEST-123"));
        }
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken ct) => Task.FromResult(1);
    }

    [Fact]
    public async Task Checkout_ValidRequest_ExecutesAndPersistsSuccessfully()
    {
        var productRepo = new FakeProductRepository();
        var customerRepo = new FakeCustomerRepository();
        var orderRepo = new FakeOrderRepository();
        var paymentGateway = new FakePaymentGateway();
        var uow = new FakeUnitOfWork();

        var product = new Product("Desk Mat", "MAT-01", 50m, 10);
        productRepo.Products.Add(product);

        var customer = new Customer("Alice", "alice@example.com", isVip: false);
        customerRepo.Customers.Add(customer);

        var service = new OrderService(orderRepo, productRepo, customerRepo, paymentGateway, uow);

        var request = new CreateOrderRequest(customer.Id, new List<OrderItemRequest> { new(product.Id, 2) }, null);
        var response = await service.CheckoutAsync(request);

        Assert.Equal("Paid", response.Status);
        Assert.Equal(8, product.StockQuantity);
        Assert.Single(orderRepo.Orders);
    }
}
