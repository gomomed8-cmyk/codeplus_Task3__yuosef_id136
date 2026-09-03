using ECommerce.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ECommerce.Infrastructure.Pdf;

internal sealed class QuestPdfService : IPdfService
{
    public byte[] GenerateBasketExpirationInvoice(
        string customerName,
        string customerEmail,
        string productName,
        string sku,
        int quantity,
        decimal unitPrice,
        DateTime addedAt,
        DateTime expirationDate)
    {
        var totalPrice = quantity * unitPrice;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header()
                    .Text("E-Commerce Invoice")
                    .FontSize(24)
                    .Bold();

                page.Content()
                    .PaddingVertical(20)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item()
                            .Text($"Customer: {customerName}");

                        column.Item()
                            .Text($"Email: {customerEmail}");

                        column.Item()
                            .LineHorizontal(1);

                        column.Item()
                            .Text("Product Information")
                            .FontSize(16)
                            .Bold();

                        column.Item()
                            .Text($"Product: {productName}");

                        column.Item()
                            .Text($"SKU: {sku}");

                        column.Item()
                            .Text($"Quantity: {quantity}");

                        column.Item()
                            .Text($"Unit Price: {unitPrice:C}");

                        column.Item()
                            .Text($"Total Price: {totalPrice:C}")
                            .Bold();

                        column.Item()
                            .LineHorizontal(1);

                        column.Item()
                            .Text($"Added At: {addedAt:yyyy-MM-dd HH:mm}");

                        column.Item()
                            .Text($"Expiration Date: {expirationDate:yyyy-MM-dd HH:mm}");

                        column.Item()
                            .PaddingTop(20)
                            .Text(
                                "This product was removed from your basket after remaining there for 7 days.");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text("E-Commerce System");
            });
        });

        return document.GeneratePdf();
    }
}