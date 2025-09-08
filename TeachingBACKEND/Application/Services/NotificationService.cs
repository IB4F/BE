using System.Net;
using System.Net.Mail;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private static readonly string _emailTemplate;

    static NotificationService()
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailVerificationTemplate.html");
        _emailTemplate = File.ReadAllText(templatePath);
    }

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public async Task SendEmailVerification(string email, Guid token, string verificationType, string? password = null)
    {
        _logger.LogInformation("Starting email-verification workflow for {Email}", email);
        var verificationUrl = $"http://localhost:4200/verify-email?token={token}&verificationType={verificationType}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Verifikoni adresën tuaj të emailit";
        var content = @"
        <p>Përshëndetje,</p>
        <p>Faleminderit që u regjistruat! Për të përfunduar procesin, ju lutemi klikoni butonin më poshtë për të verifikuar adresën tuaj të emailit.</p>";
        
        // Add password information if provided (for school-registered students)
        if (!string.IsNullOrEmpty(password))
        {
            content += $@"
            <p><strong>Fjalëkalimi juaj i gjeneruar automatikisht:</strong> {password}</p>
            <p><em>Ju lutemi ruani këtë fjalëkalim dhe ndryshojeni pas verifikimit të emailit.</em></p>";
        }
        
        var ctaText = "Verifiko Emailin Tani";

        var body = GenerateHtml(title, content, ctaText, verificationUrl, footerText);
        await SendEmail(email, "Verifikimi i emailit", body);
    }

    public async Task SendFamilyEmailVerification(string email, Guid token, List<string> familyMemberNames,
        string verificationType)
    {
        _logger.LogInformation("Starting family email-verification workflow for {Email}", email);
        var verificationUrl = $"http://localhost:4200/verify-email?token={token}&verificationType={verificationType}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var familyNamesFormatted = string.Join(", ", familyMemberNames);

        var title = "Verifikimi i llogarisë familjare";
        var content = $@"
        <p>Përshëndetje,</p>
        <p>Ju lutemi verifikoni llogarinë tuaj familjare duke klikuar butonin më poshtë. Kjo do të verifikojë ju dhe anëtarët e familjes në vijim: <strong>{familyNamesFormatted}</strong>.</p>";
        var ctaText = "Verifiko Llogarinë Familjare";

        var body = GenerateHtml(title, content, ctaText, verificationUrl, footerText);
        await SendEmail(email, "Verifikimi i llogarisë familjare", body);
    }

    public async Task SendPasswordResetEmail(string email, Guid resetToken)
    {
        _logger.LogInformation("Starting password-reset workflow for {Email}", email);
        var resetLink = $"http://localhost:4200/reset-password?token={resetToken}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Kërkesë për rivendosje të fjalëkalimit";
        var content = @"
        <p>Përshëndetje,</p>
        <p>Ne morëm një kërkesë për të rivendosur fjalëkalimin tuaj. Për të vazhduar, ju lutemi klikoni butonin më poshtë.</p>";
        var ctaText = "Rivendos Fjalëkalimin";

        var body = GenerateHtml(title, content, ctaText, resetLink, footerText);
        await SendEmail(email, "Rivendosja e Fjalëkalimit", body);
    }

    public async Task SendStudentCreatedBySchoolEmail(string email, Guid token, string password, string firstName,
        string lastName, string verificationType)
    {
        _logger.LogInformation("Starting student-creation email workflow for {Email}", email);
        var verificationUrl = $"http://localhost:4200/verify-email?token={token}&verificationType={verificationType}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Llogaria e nxënësit është krijuar nga shkolla";
        var content = $@"
        <p>Përshëndetje {firstName} {lastName},</p>
        <p>Llogaria juaj e nxënësit është krijuar nga shkolla juaj. Këtu janë kredencialet tuaja të hyrjes:</p>
        <p><strong>Email:</strong> {email}</p>
        <p><strong>Fjalëkalimi i përkohshëm:</strong> {password}</p>
        <p>Ju lutemi klikoni butonin më poshtë për të verifikuar adresën tuaj të emailit dhe më pas ndërroni fjalëkalimin:</p>";
        var ctaText = "Verifiko Adresën e Emailit";

        var body = GenerateHtml(title, content, ctaText, verificationUrl, footerText);
        await SendEmail(email, "Llogaria e nxënësit është krijuar", body);
    }

    private string GenerateHtml(string title, string content, string ctaText, string ctaUrl, string footerText)
    {
        if (string.IsNullOrEmpty(_emailTemplate))
            throw new InvalidOperationException(
                "Template-i i emailit nuk është ngarkuar. Kontrolloni che il file HTML esista nel percorso specificato.");

        var html = _emailTemplate
            .Replace("{TEMPLATE_TITLE}", title)
            .Replace("{TEMPLATE_CONTENT}", content)
            .Replace("{TEMPLATE_CTA_TEXT}", ctaText)
            .Replace("{TEMPLATE_CTA_URL}", ctaUrl)
            .Replace("{TEMPLATE_FOOTER}", footerText); // Added footer replacement

        return html;
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