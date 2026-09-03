using ECommerce.Application.Contracts.Services;
using ECommerce.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ECommerce.Infrastructure.Email;

internal sealed class SmtpEmailService(
    IOptions<EmailSettings> options)
    : IEmailService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(
     string to,
     string subject,
     string body,
     byte[]? attachment = null,
     string? attachmentName = null,
     CancellationToken cancellationToken = default)
    {
        using var message = new MailMessage
        {
            From = new MailAddress(
                _settings.FromEmail,
                _settings.FromName),

            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);

        if (attachment != null && !string.IsNullOrWhiteSpace(attachmentName))
        {
            var stream = new MemoryStream(attachment);

            var mailAttachment = new Attachment(
                stream,
                attachmentName,
                "application/pdf");

            message.Attachments.Add(mailAttachment);
        }

        using var client = new SmtpClient(
            _settings.Host,
            _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(
                _settings.Username,
                _settings.Password)
        };

        cancellationToken.ThrowIfCancellationRequested();

        await client.SendMailAsync(message);
    }
}