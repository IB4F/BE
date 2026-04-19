using System.Net;
using System.Net.Mail;
using TeachingBACKEND.Application.Interfaces;

namespace TeachingBACKEND.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendAsync(string to, string subject, string htmlBody)
        {
            try
            {
                var smtpHost = _configuration["EMAIL_SMTP_HOST"] ?? "smtp.gmail.com";
                var smtpPort = int.Parse(_configuration["EMAIL_SMTP_PORT"] ?? "587");
                var smtpUser = _configuration["EMAIL_SMTP_USER"] ?? throw new InvalidOperationException("EMAIL_SMTP_USER not configured");
                var smtpPass = _configuration["EMAIL_SMTP_PASS"] ?? throw new InvalidOperationException("EMAIL_SMTP_PASS not configured");
                var fromName = _configuration["EMAIL_FROM_NAME"] ?? "Platform";

                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl = true
                };

                using var message = new MailMessage
                {
                    From = new MailAddress(smtpUser, fromName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };

                message.To.Add(to);
                await client.SendMailAsync(message);

                _logger.LogInformation("Email sent to {To}, subject: {Subject}", to, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To}", to);
                throw;
            }
        }

        public async Task SendManualPaymentInstructionsAsync(
            string to, string reference, long amountCents, string currency,
            string bankName, string iban, string accountHolder)
        {
            var amount = (amountCents / 100.0m).ToString("F2");
            var subject = "Instruksionet e Pagesës / Payment Instructions";
            var html = $@"
<div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto'>
  <h2>Instruksionet e Pagesës</h2>
  <p>Ju lutemi kryeni pagesën bankare me të dhënat e mëposhtme:</p>
  <table style='border-collapse:collapse;width:100%'>
    <tr><td style='padding:8px;border:1px solid #ddd;font-weight:bold'>Banka</td><td style='padding:8px;border:1px solid #ddd'>{bankName}</td></tr>
    <tr><td style='padding:8px;border:1px solid #ddd;font-weight:bold'>IBAN</td><td style='padding:8px;border:1px solid #ddd'>{iban}</td></tr>
    <tr><td style='padding:8px;border:1px solid #ddd;font-weight:bold'>Përfituesi</td><td style='padding:8px;border:1px solid #ddd'>{accountHolder}</td></tr>
    <tr><td style='padding:8px;border:1px solid #ddd;font-weight:bold'>Referenca</td><td style='padding:8px;border:1px solid #ddd'><strong>{reference}</strong></td></tr>
    <tr><td style='padding:8px;border:1px solid #ddd;font-weight:bold'>Shuma</td><td style='padding:8px;border:1px solid #ddd'>{amount} {currency.ToUpper()}</td></tr>
  </table>
  <p style='margin-top:16px;color:#e74c3c'><strong>IMPORTANT:</strong> Vendosni referencën <strong>{reference}</strong> në fushën e pagesës.</p>
  <p>Pas konfirmimit të pagesës, llogaria juaj do të aktivizohet brenda 1-2 ditëve pune.</p>
</div>";

            await SendAsync(to, subject, html);
        }

        public async Task SendManualPaymentConfirmedAsync(string to, string packageName)
        {
            var subject = "Pagesa u konfirmua / Payment Confirmed";
            var html = $@"
<div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto'>
  <h2 style='color:#27ae60'>Pagesa u konfirmua!</h2>
  <p>Pagesa juaj për <strong>{packageName}</strong> u konfirmua me sukses.</p>
  <p>Abonimi juaj është tani aktiv. Mund të hyni në platformë dhe të filloni menjëherë.</p>
</div>";

            await SendAsync(to, subject, html);
        }

        public async Task SendManualPaymentReminderAsync(string to, string reference, long amountCents, string currency)
        {
            var amount = (amountCents / 100.0m).ToString("F2");
            var subject = "Kujtesë Pagese / Payment Reminder";
            var html = $@"
<div style='font-family:Arial,sans-serif;max-width:600px;margin:0 auto'>
  <h2 style='color:#e67e22'>Kujtesë Pagese</h2>
  <p>Pagesa juaj bankare me referencë <strong>{reference}</strong> ende nuk është konfirmuar.</p>
  <p>Shuma: <strong>{amount} {currency.ToUpper()}</strong></p>
  <p>Nëse keni kryer pagesën, ju lutemi na kontaktoni me referencën e mësipërme.</p>
  <p>Nëse nuk kryeni pagesën, llogaria juaj mund të pezullohet.</p>
</div>";

            await SendAsync(to, subject, html);
        }
    }
}