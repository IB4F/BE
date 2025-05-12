using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    public async Task SendEmailVerification(string email, Guid token)
    {
        string verificationUrl = $"https://yourdomain.com/api/user/verify-email?token={token}";
        string subject = "Email Verification";
        string body = $"Click the link to verify your email: {verificationUrl}";

        await SendEmail(email, subject, body);
    }

    public async Task SendPasswordResetEmail(string email, Guid resetToken)
    {
        string resetLink = $"https://frontend.com/reset-password?token={resetToken}";
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
                    await smtp.SendMailAsync(message);
                    Console.WriteLine($"Email sent to {email} successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Email sending failed: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
