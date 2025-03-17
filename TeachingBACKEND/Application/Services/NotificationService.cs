using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    private readonly IConfiguration _configuration;

    public NotificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

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
        var fromAddress = new MailAddress(_configuration["EmailSettings:From"], "Your App");
        var toAddress = new MailAddress(email);

        using (var smtp = new SmtpClient
        {
            Host = _configuration["EmailSettings:SmtpServer"],
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
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
