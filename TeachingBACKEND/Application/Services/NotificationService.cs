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
        var verificationUrl = $"https://yourdomain.com/api/user/verify-email?token={token}";

        var fromAddress = new MailAddress(_configuration["EmailSettings:From"], "Your App");
        var toAddress = new MailAddress(email);
        const string subject = "Email Verification";
        string body = $"Click the link to verify your email: {verificationUrl}";

        // Configure SMTP client
        using (var smtp = new SmtpClient
        {
            Host = _configuration["EmailSettings:SmtpServer"], // e.g., smtp.gmail.com
            Port = 587, // Port for TLS (or 465 for SSL)
            EnableSsl = true, // Enable SSL for secure connection
            Credentials = new NetworkCredential(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
            )
        })
        {
            using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
            {
                try
                {
                    await smtp.SendMailAsync(message);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
        }
    }
}
