using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(
        string to,
        string subject,
        string body,
        byte[]? attachment = null,
        string? attachmentName = null,
        CancellationToken cancellationToken = default);
    }
}
