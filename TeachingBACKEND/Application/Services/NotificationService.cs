using System.Net;
using System.Net.Mail;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IConfiguration _configuration;
    private static readonly string _emailTemplate;

    static NotificationService()
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", "EmailVerificationTemplate.html");
        _emailTemplate = File.ReadAllText(templatePath);
    }

    public NotificationService(ILogger<NotificationService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendEmailVerification(string email, Guid token, string verificationType, string? password = null)
    {
        _logger.LogInformation("Starting email-verification workflow for {Email}", email);
        var verificationUrl = $"https://braingainalbania.al/verify-email?token={token}&verificationType={verificationType}";
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
        string verificationType, List<(string Name, string Email, string Password)>? familyMemberCredentials = null)
    {
        _logger.LogInformation("Starting family email-verification workflow for {Email}", email);
        var verificationUrl = $"https://braingainalbania.al//verify-email?token={token}&verificationType={verificationType}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var familyNamesFormatted = string.Join(", ", familyMemberNames);

        var title = "Verifikimi i llogarisë familjare";
        var content = $@"
        <p>Përshëndetje,</p>
        <p>Ju lutemi verifikoni llogarinë tuaj familjare duke klikuar butonin më poshtë. Kjo do të verifikojë ju dhe anëtarët e familjes në vijim: <strong>{familyNamesFormatted}</strong>.</p>";

        // Add family member credentials if provided
        if (familyMemberCredentials != null && familyMemberCredentials.Any())
        {
            content += @"
            <p><strong>Kredencialet e anëtarëve të familjes:</strong></p>
            <ul>";
            
            foreach (var (name, memberEmail, password) in familyMemberCredentials)
            {
                content += $@"
                <li><strong>{name}:</strong><br/>
                    Email: {memberEmail}<br/>
                    Fjalëkalimi: {password}</li>";
            }
            
            content += @"
            </ul>
            <p><em>Ju lutemi ruani këto kredenciale dhe ndryshoni fjalëkalimet pas verifikimit të emailit.</em></p>";
        }

        var ctaText = "Verifiko Llogarinë Familjare";

        var body = GenerateHtml(title, content, ctaText, verificationUrl, footerText);
        await SendEmail(email, "Verifikimi i llogarisë familjare", body);
    }

    public async Task SendPasswordResetEmail(string email, Guid resetToken)
    {
        _logger.LogInformation("Starting password-reset workflow for {Email}", email);
        var resetLink = $"https://braingainalbania.al//reset-password?token={resetToken}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Kërkesë për rivendosje të fjalëkalimit";
        var content = @"
        <p>Përshëndetje,</p>
        <p>Ne morëm një kërkesë për të rivendosur fjalëkalimin tuaj. Për të vazhduar, ju lutemi klikoni butonin më poshtë.</p>";
        var ctaText = "Rivendos Fjalëkalimin";

        var body = GenerateHtml(title, content, ctaText, resetLink, footerText);
        await SendEmail(email, "Rivendosja e Fjalëkalimit", body);
    }

    public async Task SendChildPasswordResetEmail(string parentEmail, string childFirstName, string childLastName, Guid resetToken)
    {
        _logger.LogInformation("Starting child password-reset workflow for parent {ParentEmail}, child {ChildFirstName} {ChildLastName}", parentEmail, childFirstName, childLastName);
        var resetLink = $"https://braingainalbania.al//reset-password?token={resetToken}";
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Kërkesë për rivendosje të fjalëkalimit të fëmijës";
        var content = $@"
        <p>Përshëndetje,</p>
        <p>Ne morëm një kërkesë për të rivendosur fjalëkalimin e fëmijës tuaj <strong>{childFirstName} {childLastName}</strong>. Për të vazhduar, ju lutemi klikoni butonin më poshtë.</p>
        <p><em>Shënim: Kjo kërkesë është dërguar tek ju sepse fëmija juaj ka një adresë email të krijuar automatikisht që nuk mund të marrë email-e.</em></p>";
        var ctaText = "Rivendos Fjalëkalimin e Fëmijës";

        var body = GenerateHtml(title, content, ctaText, resetLink, footerText);
        await SendEmail(parentEmail, "Rivendosja e Fjalëkalimit të Fëmijës", body);
    }

    public async Task SendStudentCreatedBySchoolEmail(string email, Guid token, string password, string firstName,
        string lastName, string verificationType)
    {
        _logger.LogInformation("Starting student-creation email workflow for {Email}", email);
        var verificationUrl = $"https://braingainalbania.al//verify-email?token={token}&verificationType={verificationType}";
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

    // Supervisor Flow Methods
    public async Task SendSupervisorApplicationNotification(string supervisorEmail, string supervisorName, string schoolName)
    {
        _logger.LogInformation("Starting supervisor application notification for {SupervisorEmail}", supervisorEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        // Get admin panel URL from configuration
        var adminPanelUrl = _configuration["AppSettings:AdminPanelUrl"] ?? "https://braingainalbania.al/admin/supervisor-applications";
        
        var title = "Aplikim i ri i Supervizorit";
        var content = $@"
        <p>Përshëndetje Admin,</p>
        <p>Një supervizor i ri ka aplikuar për të hyrë në platformë:</p>
        <ul>
            <li><strong>Emri:</strong> {supervisorName}</li>
            <li><strong>Email:</strong> {supervisorEmail}</li>
            <li><strong>Shkolla:</strong> {schoolName}</li>
        </ul>
        <p>Ju lutemi shqyrtoni aplikimin dhe merrni vendimin përkatës në panelin e administrimit.</p>";

        var ctaText = "Shiko Aplikimet e Supervizorëve";
        var body = GenerateHtml(title, content, ctaText, adminPanelUrl, footerText);
        await SendEmail("fabiano.meoo98@gmail.com", "Aplikim i ri i Supervizorit", body);
    }

    public async Task SendSupervisorApprovalEmail(string supervisorEmail, string supervisorName, string packageSelectionLink, string? temporaryPassword = null)
    {
        _logger.LogInformation("Starting supervisor approval email for {SupervisorEmail}", supervisorEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Aplikimi juaj është pranuar!";
        var content = $@"
        <p>Përshëndetje {supervisorName},</p>
        <p>Urime! Aplikimi juaj për të u bërë supervizor është pranuar nga administrata.</p>
        <p>Tani mund të zgjidhni paketën e subscriptionit tuaj dhe të filloni të menaxhoni nxënësit tuaj.</p>";
        
        // Only include credentials if provided (for post-payment email)
        if (!string.IsNullOrEmpty(temporaryPassword))
        {
            content += $@"
            <p><strong>Kredencialet tuaja të hyrjes:</strong></p>
            <ul>
                <li><strong>Email:</strong> {supervisorEmail}</li>
                <li><strong>Fjalëkalimi i përkohshëm:</strong> {temporaryPassword}</li>
            </ul>
            <p><em>Ju lutemi ruani këto kredenciale dhe ndryshoni fjalëkalimin pas hyrjes së parë për siguri.</em></p>";
        }
        
        var ctaText = "Zgjidh Paketën e Subscriptionit";

        var body = GenerateHtml(title, content, ctaText, packageSelectionLink, footerText);
        await SendEmail(supervisorEmail, "Aplikimi juaj është pranuar", body);
    }

    public async Task SendSupervisorCredentialsEmail(string supervisorEmail, string supervisorName, string temporaryPassword)
    {
        _logger.LogInformation("Starting supervisor credentials email for {SupervisorEmail}", supervisorEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Kredencialet tuaja të hyrjes";
        var content = $@"
        <p>Përshëndetje {supervisorName},</p>
        <p>Pagesa juaj u konfirmua me sukses! Tani mund të kyçeni në platformë duke përdorur kredencialet e mëposhtme:</p>
        <p><strong>Kredencialet tuaja të hyrjes:</strong></p>
        <ul>
            <li><strong>Email:</strong> {supervisorEmail}</li>
            <li><strong>Fjalëkalimi i përkohshëm:</strong> {temporaryPassword}</li>
        </ul>
        <p><em>Ju lutemi ruani këto kredenciale dhe ndryshoni fjalëkalimin pas hyrjes së parë për siguri.</em></p>
        <p>Tani mund të filloni të menaxhoni nxënësit tuaj.</p>";
        var ctaText = "Kyçu në Platformë";
        var loginUrl = "https://braingainalbania.al/login";

        var body = GenerateHtml(title, content, ctaText, loginUrl, footerText);
        await SendEmail(supervisorEmail, "Kredencialet tuaja të hyrjes", body);
    }

    public async Task SendSupervisorRejectionEmail(string supervisorEmail, string supervisorName, string reason)
    {
        _logger.LogInformation("Starting supervisor rejection email for {SupervisorEmail}", supervisorEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Aplikimi juaj nuk është pranuar";
        var content = $@"
        <p>Përshëndetje {supervisorName},</p>
        <p>Faleminderit për interesimin tuaj për të u bërë supervizor në platformën tonë.</p>
        <p>Fatkeqësisht, aplikimi juaj nuk është pranuar për arsyen e mëposhtme:</p>
        <p><strong>{reason}</strong></p>
        <p>Nëse keni pyetje ose dëshironi të aplikoni përsëri në të ardhmen, ju lutemi na kontaktoni.</p>";

        var body = GenerateHtml(title, content, "", "", footerText);
        await SendEmail(supervisorEmail, "Aplikimi juaj nuk është pranuar", body);
    }

    public async Task SendNewPasswordSetEmail(string studentEmail, string studentName, string newPassword)
    {
        _logger.LogInformation("Starting new password set email for {StudentEmail}", studentEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Fjalëkalimi juaj është rivendosur";
        var content = $@"
        <p>Përshëndetje {studentName},</p>
        <p>Supervizori juaj ka rivendosur fjalëkalimin tuaj. Këtu janë kredencialet tuaja të reja:</p>
        <p><strong>Email:</strong> {studentEmail}</p>
        <p><strong>Fjalëkalimi i ri:</strong> {newPassword}</p>
        <p>Ju lutemi kyçuni në platformë dhe ndryshoni fjalëkalimin për siguri më të mirë.</p>";

        var body = GenerateHtml(title, content, "", "", footerText);
        await SendEmail(studentEmail, "Fjalëkalimi juaj është rivendosur", body);
    }

    public async Task SendStudentPasswordResetRequestToSupervisor(string supervisorEmail, string supervisorName, string studentName, string studentEmail, Guid resetToken)
    {
        _logger.LogInformation("Starting student password reset request notification for supervisor {SupervisorEmail}, student {StudentEmail}", supervisorEmail, studentEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        // Get supervisor panel URL from configuration
        var supervisorPanelUrl = _configuration["AppSettings:SupervisorPanelUrl"] ?? "https://braingainalbania.al/supervisor/password-reset-requests";
        
        var title = "Kërkesë për rivendosje të fjalëkalimit të nxënësit";
        var content = $@"
        <p>Përshëndetje {supervisorName},</p>
        <p>Një nxënës i juaj ka kërkuar rivendosje të fjalëkalimit:</p>
        <ul>
            <li><strong>Emri i nxënësit:</strong> {studentName}</li>
            <li><strong>Email i nxënësit:</strong> {studentEmail}</li>
            <li><strong>Data e kërkesës:</strong> {DateTime.UtcNow:dd/MM/yyyy HH:mm}</li>
        </ul>
        <p>Ju lutemi shqyrtoni kërkesën dhe merrni vendimin përkatës në panelin e supervizorit.</p>
        <p><em>Shënim: Kjo kërkesë është dërguar tek ju sepse nxënësi ka një adresë email të krijuar automatikisht (@bga.al) që nuk mund të marrë email-e.</em></p>";

        var ctaText = "Shiko Kërkesat për Rivendosje të Fjalëkalimit";
        var body = GenerateHtml(title, content, ctaText, supervisorPanelUrl, footerText);
        await SendEmail(supervisorEmail, "Kërkesë për rivendosje të fjalëkalimit të nxënësit", body);
    }

    public async Task SendNewPasswordToSupervisor(string supervisorEmail, string supervisorName, string studentName, string studentEmail, string newPassword)
    {
        _logger.LogInformation("Starting new password notification to supervisor {SupervisorEmail} for student {StudentEmail}", supervisorEmail, studentEmail);
        var footerText = "&copy; 2025 Brain Gain. Të gjitha të drejtat e rezervuara.";

        var title = "Fjalëkalimi i ri i nxënësit është gjeneruar";
        var content = $@"
        <p>Përshëndetje {supervisorName},</p>
        <p>Ju keni miratuar kërkesën për rivendosje të fjalëkalimit për nxënësin tuaj. Këtu janë kredencialet e reja:</p>
        <ul>
            <li><strong>Emri i nxënësit:</strong> {studentName}</li>
            <li><strong>Email i nxënësit:</strong> {studentEmail}</li>
            <li><strong>Fjalëkalimi i ri:</strong> {newPassword}</li>
        </ul>
        <p><em>Ju lutemi jepni këto kredenciale nxënësit dhe këshillojeni që të ndryshojë fjalëkalimin pas hyrjes së parë për siguri.</em></p>
        <p><strong>Shënim:</strong> Fjalëkalimi u dërgua tek ju sepse nxënësi ka një adresë email të krijuar automatikisht (@bga.al) që nuk mund të marrë email-e.</p>";

        var body = GenerateHtml(title, content, "", "", footerText);
        await SendEmail(supervisorEmail, "Fjalëkalimi i ri i nxënësit është gjeneruar", body);
    }
}