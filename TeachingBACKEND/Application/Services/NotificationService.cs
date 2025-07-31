using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public  NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }
    
    public async Task SendEmailVerification(string email, Guid token)
    {
        _logger.LogInformation("Starting email-verification workflow for {Email}", email);
        string verificationUrl = $"http://localhost:4200/verify-email?token={token}";
        string subject = "Verifica la tua Email";
        
        string htmlTemplate = File.ReadAllText("EmailVerificationTemplate.html");
        string body = htmlTemplate.Replace("{VERIFICATION_URL}", verificationUrl);

        await SendEmail(email, subject, body);
    }

    public async Task SendPasswordResetEmail(string email, Guid resetToken)
    {
        _logger.LogInformation("Starting password-reset workflow for {Email}", email);
        string resetLink = $"http://localhost:4200/reset-password?token={resetToken}";
        string subject = "Password Reset Request";
        string body = $"Click the link to reset your password: {resetLink}";

        await SendEmail(email, subject, body);
    }

    public async Task SendEmail(string email, string subject, string body)
    {
        var fromAddress = new MailAddress(
            Environment.GetEnvironmentVariable("EMAIL_FROM"),
            "Your App"
        );
        var toAddress = new MailAddress(email);

        using (var smtp = new SmtpClient
        {
            Host = Environment.GetEnvironmentVariable("EMAIL_SMTP_SERVER"),
            Port = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT") ?? "587"),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
                Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
            )
        })
        {
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                try
                {
                    _logger.LogInformation("Sending email to {Email}", email);
                    await smtp.SendMailAsync(message);
                    Console.WriteLine($"Email sent to {email} successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send email to {Email}", email);
                    Console.WriteLine($"Email sending failed: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
