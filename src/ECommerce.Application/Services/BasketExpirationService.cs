using ECommerce.Application.Contracts.Persistence;
using ECommerce.Application.Contracts.Services;
using ECommerce.Application.Interfaces.Services;

namespace ECommerce.Application.Services;

public sealed class BasketExpirationService(
    IBasketRepository basketRepository,
    IEmailService emailService,
    IPdfService pdfService,
    IUnitOfWork unitOfWork)
    : IBasketExpirationService
{
    public async Task ProcessAsync(
    CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var reminderCutoff = now.AddDays(-4);
        var expirationCutoff = now.AddDays(-7);

        // ==========================================
        // 1. Products older than 7 days
        // ==========================================

        var expiredItems =
            await basketRepository.GetExpiredItemsAsync(
                expirationCutoff,
                cancellationToken);

        foreach (var item in expiredItems)
        {
            var basket = item.Basket;
            var customer = basket?.Customer;
            var product = item.Product;

            if (basket == null ||
                customer == null ||
                product == null)
                continue;

            var pdf = pdfService.GenerateBasketExpirationInvoice(
                customer.FullName,
                customer.Email,
                product.Name,
                product.SKU,
                item.Quantity,
                product.Price,
                item.AddedAt,
                now);

            var body = $"""
            <h2>Basket Product Expired</h2>

            <p>Hello {customer.FullName},</p>

            <p>
                The following product has been removed from your basket
                because it remained there for 7 days.
            </p>

            <p>
                <strong>Product:</strong> {product.Name}
            </p>

            <p>
                <strong>Quantity:</strong> {item.Quantity}
            </p>

            <p>
                <strong>Total Price:</strong>
                {(item.Quantity * product.Price):C}
            </p>

            <p>
                We have attached a PDF containing the product details.
            </p>
            """;

            await emailService.SendAsync(
                customer.Email,
                "Basket Product Expired",
                body,
                pdf,
                $"Basket-Invoice-{product.SKU}.pdf",
                cancellationToken);

            basket.RemoveItem(item);
        }

        // ==========================================
        // 2. Products older than 4 days
        //    but younger than 7 days
        // ==========================================

        var reminderItems =
            await basketRepository.GetItemsForReminderAsync(
                reminderCutoff,
                expirationCutoff,
                cancellationToken);

        foreach (var item in reminderItems)
        {
            var basket = item.Basket;
            var customer = basket?.Customer;
            var product = item.Product;

            if (basket == null ||
                customer == null ||
                product == null)
                continue;

            var body = $"""
            <h2>Basket Reminder</h2>

            <p>Hello {customer.FullName},</p>

            <p>
                You still have the following product in your basket:
            </p>

            <p>
                <strong>Product:</strong> {product.Name}
            </p>

            <p>
                <strong>Quantity:</strong> {item.Quantity}
            </p>

            <p>
                <strong>Unit Price:</strong>
                {product.Price:C}
            </p>

            <p>
                Please note that this product will be removed
                from your basket after 7 days.
            </p>
            """;

            await emailService.SendAsync(
                customer.Email,
                "Reminder: Product is still in your basket",
                body,
                cancellationToken: cancellationToken);

            item.MarkReminderAsSent();
        }

        await unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}