using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IPdfService
    {
        byte[] GenerateBasketExpirationInvoice(
            string customerName,
            string customerEmail,
            string productName,
            string sku,
            int quantity,
            decimal unitPrice,
            DateTime addedAt,
            DateTime expirationDate);
    }
}
