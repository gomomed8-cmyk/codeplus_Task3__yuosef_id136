using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;
using Xunit;

namespace ECommerce.UnitTests.Domain;

public class OrderTests
{
    [Fact]
    public void CalculateTotals_WithVipAndCoupon_CalculatesCorrectDiscountsTaxAndShipping()
    {
        var order = new Order(1);
        order.AddItem(1, 2, 100m);

        var coupon = new Coupon("DISCOUNT10", 10m, true);
        order.CalculateTotals(isCustomerVip: true, coupon: coupon);

        Assert.Equal(200m, order.Subtotal);
        Assert.Equal(50m, order.DiscountAmount);
        Assert.Equal(21m, order.TaxAmount);
        Assert.Equal(75m, order.ShippingFee);
        Assert.Equal(246m, order.TotalAmount);
    }

    [Fact]
    public void MarkAsPaid_WhenAlreadyPaid_ThrowsInvalidOrderStateException()
    {
        var order = new Order(1);
        order.AddItem(1, 1, 100m);
        order.CalculateTotals(false);
        order.MarkAsPaid("TX-12345");

        Assert.Throws<InvalidOrderStateException>(() => order.MarkAsPaid("TX-67890"));
    }
}
