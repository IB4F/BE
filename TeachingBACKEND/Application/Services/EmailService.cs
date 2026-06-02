using System.Net;
using System.Net.Mail;
using TeachingBACKEND.Application.Interfaces;

namespace TeachingBACKEND.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        private static readonly string _tplPaymentInstructions;
        private static readonly string _tplPaymentConfirmed;
        private static readonly string _tplPaymentReminder;

        static EmailService()
        {
            var sys = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "system");
            _tplPaymentInstructions = File.ReadAllText(Path.Combine(sys, "payment-instructions.html"));
            _tplPaymentConfirmed    = File.ReadAllText(Path.Combine(sys, "payment-confirmed.html"));
            _tplPaymentReminder     = File.ReadAllText(Path.Combine(sys, "payment-reminder.html"));
        }

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
                var fromName = _configuration["EMAIL_FROM_NAME"] ?? "Brain Gain Albania";

                using var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUser, smtpPass),
                    EnableSsl   = true
                };

                using var message = new MailMessage
                {
                    From       = new MailAddress(smtpUser, fromName),
                    Subject    = subject,
                    Body       = htmlBody,
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
            var html = _tplPaymentInstructions
                .Replace("{{BANK_NAME}}",          bankName)
                .Replace("{{IBAN}}",               iban)
                .Replace("{{ACCOUNT_HOLDER}}",     accountHolder)
                .Replace("{{PAYMENT_REFERENCE}}",  reference)
                .Replace("{{PAYMENT_AMOUNT}}",     $"{amount} {currency.ToUpper()}");
            await SendAsync(to, "Instruksionet e Pagesës · Payment Instructions", ApplyGlobalTokens(html));
        }

        public async Task SendManualPaymentConfirmedAsync(string to, string packageName)
        {
            var html = _tplPaymentConfirmed.Replace("{{PACKAGE_NAME}}", packageName);
            await SendAsync(to, "Pagesa u konfirmua · Payment Confirmed", ApplyGlobalTokens(html));
        }

        public async Task SendManualPaymentReminderAsync(string to, string reference, long amountCents, string currency)
        {
            var amount = (amountCents / 100.0m).ToString("F2");
            var html = _tplPaymentReminder
                .Replace("{{PAYMENT_REFERENCE}}", reference)
                .Replace("{{PAYMENT_AMOUNT}}",    $"{amount} {currency.ToUpper()}");
            await SendAsync(to, "Kujtesë Pagese · Payment Reminder", ApplyGlobalTokens(html));
        }

        private string ApplyGlobalTokens(string html) =>
            html
                .Replace("{{LOGO_WHITE_URL}}", _configuration["AppSettings:LogoWhiteUrl"] ?? "")
                .Replace("{{LOGO_COLOR_URL}}", _configuration["AppSettings:LogoColorUrl"] ?? "")
                .Replace("{{HELP_URL}}",        _configuration["AppSettings:HelpUrl"] ?? "https://braingainalbania.al/ndihma")
                .Replace("{{PRIVACY_URL}}",     _configuration["AppSettings:PrivacyUrl"] ?? "https://braingainalbania.al/privatesia")
                .Replace("{{YEAR}}",            DateTime.UtcNow.Year.ToString());
    }
}
