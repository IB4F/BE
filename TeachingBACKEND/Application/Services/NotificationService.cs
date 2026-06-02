using System.Net;
using System.Net.Mail;
using TeachingBACKEND.Application.Interfaces;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IConfiguration _configuration;

    // ── Transactional templates (old + new branded) ───────────────────────────
    private static readonly string _emailTemplate;
    private static readonly string _verifyEmailTemplate;
    private static readonly string _welcomeTemplate;
    private static readonly string _resetPasswordTemplate;

    // ── System templates (14 new branded emails) ──────────────────────────────
    private static readonly string _tplFamilyVerification;
    private static readonly string _tplStudentCreated;
    private static readonly string _tplChildrenCredentials;
    private static readonly string _tplChildPasswordReset;
    private static readonly string _tplSupervisorApplication;
    private static readonly string _tplSupervisorApproval;
    private static readonly string _tplSupervisorRejection;
    private static readonly string _tplSupervisorCredentials;
    private static readonly string _tplStudentPasswordSet;
    private static readonly string _tplResetRequestToSupervisor;
    private static readonly string _tplNewPasswordToSupervisor;

    static NotificationService()
    {
        var dir = Path.Combine(Directory.GetCurrentDirectory(), "Templates");
        var sys = Path.Combine(dir, "system");

        _emailTemplate        = File.ReadAllText(Path.Combine(dir, "EmailVerificationTemplate.html"));
        _verifyEmailTemplate  = File.ReadAllText(Path.Combine(dir, "verify-email.html"));
        _welcomeTemplate      = File.ReadAllText(Path.Combine(dir, "welcome.html"));
        _resetPasswordTemplate = File.ReadAllText(Path.Combine(dir, "reset-password.html"));

        _tplFamilyVerification       = File.ReadAllText(Path.Combine(sys, "family-verification.html"));
        _tplStudentCreated           = File.ReadAllText(Path.Combine(sys, "student-created.html"));
        _tplChildrenCredentials      = File.ReadAllText(Path.Combine(sys, "children-credentials.html"));
        _tplChildPasswordReset       = File.ReadAllText(Path.Combine(sys, "child-password-reset.html"));
        _tplSupervisorApplication    = File.ReadAllText(Path.Combine(sys, "supervisor-application.html"));
        _tplSupervisorApproval       = File.ReadAllText(Path.Combine(sys, "supervisor-approval.html"));
        _tplSupervisorRejection      = File.ReadAllText(Path.Combine(sys, "supervisor-rejection.html"));
        _tplSupervisorCredentials    = File.ReadAllText(Path.Combine(sys, "supervisor-credentials.html"));
        _tplStudentPasswordSet       = File.ReadAllText(Path.Combine(sys, "student-password-set.html"));
        _tplResetRequestToSupervisor = File.ReadAllText(Path.Combine(sys, "reset-request-to-supervisor.html"));
        _tplNewPasswordToSupervisor  = File.ReadAllText(Path.Combine(sys, "new-password-to-supervisor.html"));
    }

    public NotificationService(ILogger<NotificationService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // ── System template helpers ───────────────────────────────────────────────

    private string ApplyGlobalTokens(string html) =>
        html
            .Replace("{{LOGO_WHITE_URL}}", _configuration["AppSettings:LogoWhiteUrl"] ?? "")
            .Replace("{{LOGO_COLOR_URL}}", _configuration["AppSettings:LogoColorUrl"] ?? "")
            .Replace("{{HELP_URL}}",       _configuration["AppSettings:HelpUrl"] ?? "https://braingainalbania.al/help")
            .Replace("{{PRIVACY_URL}}",    _configuration["AppSettings:PrivacyUrl"] ?? "https://braingainalbania.al/privacy")
            .Replace("{{YEAR}}",           DateTime.UtcNow.Year.ToString());

    // Strips everything between <!-- OPTIONAL: ... --> and <!-- END OPTIONAL -->
    private static string StripOptionalBlock(string html)
    {
        const string start = "<!-- OPTIONAL:";
        const string end   = "<!-- END OPTIONAL -->";
        var si = html.IndexOf(start, StringComparison.Ordinal);
        if (si < 0) return html;
        var ei = html.IndexOf(end, si, StringComparison.Ordinal);
        if (ei < 0) return html;
        return html.Remove(si, ei - si + end.Length);
    }

    // Replaces <!-- REPEAT ... <!-- END REPEAT --> with one rendered card per member
    private static string ProcessRepeatBlock(string html, IEnumerable<(string Name, string Email, string Password)> members)
    {
        const string startMarker = "<!-- REPEAT the card below once per family member / child -->";
        const string endMarker   = "<!-- END REPEAT -->";
        var si = html.IndexOf(startMarker, StringComparison.Ordinal);
        var ei = html.IndexOf(endMarker, StringComparison.Ordinal);
        if (si < 0 || ei < 0) return html;
        var cardTpl = html[(si + startMarker.Length)..ei];
        var cards = string.Concat(members.Select(m => cardTpl
            .Replace("{{MEMBER_NAME}}",     m.Name)
            .Replace("{{MEMBER_EMAIL}}",    m.Email)
            .Replace("{{MEMBER_PASSWORD}}", m.Password)));
        return html[..si] + cards + html[(ei + endMarker.Length)..];
    }

    // ── Old branded templates (verify / welcome / reset) ─────────────────────

    private string RenderBrandedTemplate(string template, string actionUrl, string? userName = null, string? expiryHours = null)
    {
        var expiry = expiryHours ?? _configuration["AppSettings:EmailVerifyExpiryHours"] ?? "24";
        return template
            .Replace("{{LOGO_WHITE_URL}}", _configuration["AppSettings:LogoWhiteUrl"] ?? "")
            .Replace("{{LOGO_COLOR_URL}}", _configuration["AppSettings:LogoColorUrl"] ?? "")
            .Replace("{{ACTION_URL}}",     actionUrl)
            .Replace("{{USER_NAME}}",      string.IsNullOrWhiteSpace(userName) ? "" : " " + userName.Trim())
            .Replace("{{EXPIRY_HOURS}}",   expiry)
            .Replace("{{HELP_URL}}",       _configuration["AppSettings:HelpUrl"] ?? "https://braingainalbania.al/help")
            .Replace("{{PRIVACY_URL}}",    _configuration["AppSettings:PrivacyUrl"] ?? "https://braingainalbania.al/privacy")
            .Replace("{{YEAR}}",           DateTime.UtcNow.Year.ToString());
    }

    // ── Transactional flows — branded templates ───────────────────────────────

    public async Task SendEmailVerification(string email, Guid token, string verificationType, string? password = null, string? userName = null)
    {
        _logger.LogInformation("Starting email-verification workflow for {Email}", email);
        var verifyUrl     = $"https://braingainalbania.al/verify-email?token={token}&verificationType={verificationType}";
        var expiryHours   = _configuration["AppSettings:EmailVerifyExpiryHours"] ?? "24";

        if (string.IsNullOrEmpty(password))
        {
            var body = RenderBrandedTemplate(_verifyEmailTemplate, verifyUrl, userName, expiryHours);
            await SendEmail(email, "Verifikimi i emailit · Verify your email", body);
        }
        else
        {
            // Student with auto-generated password — use student-created system template
            var html = _tplStudentCreated
                .Replace("{{USER_NAME}}",     userName ?? $"{email.Split('@')[0]}")
                .Replace("{{USER_EMAIL}}",    email)
                .Replace("{{TEMP_PASSWORD}}", password)
                .Replace("{{ACTION_URL}}",    verifyUrl);
            await SendEmail(email, "Llogaria e nxënësit është krijuar · Your student account is ready", ApplyGlobalTokens(html));
        }
    }

    public async Task SendWelcomeEmail(string email, string? userName = null)
    {
        _logger.LogInformation("Sending welcome email to {Email}", email);
        var dashboardUrl = (_configuration["AppSettings:BaseUrl"] ?? "https://braingainalbania.al") + "/login";
        var body = RenderBrandedTemplate(_welcomeTemplate, dashboardUrl, userName);
        await SendEmail(email, "Mirë se vini në Brain Gain · Welcome to Brain Gain", body);
    }

    public async Task SendPasswordResetEmail(string email, Guid resetToken)
    {
        _logger.LogInformation("Starting password-reset workflow for {Email}", email);
        var resetLink   = $"https://braingainalbania.al/reset-password?token={resetToken}";
        var expiryHours = _configuration["AppSettings:PasswordResetExpiryHours"] ?? "2";
        var body = RenderBrandedTemplate(_resetPasswordTemplate, resetLink, null, expiryHours);
        await SendEmail(email, "Rivendosja e fjalëkalimit · Reset your password", body);
    }

    public async Task SendChildPasswordResetEmail(string parentEmail, string childFirstName, string childLastName, Guid resetToken)
    {
        _logger.LogInformation("Starting child password-reset for parent {ParentEmail}", parentEmail);
        var resetLink   = $"https://braingainalbania.al/reset-password?token={resetToken}";
        var expiryHours = _configuration["AppSettings:PasswordResetExpiryHours"] ?? "2";
        var body = RenderBrandedTemplate(_resetPasswordTemplate, resetLink, null, expiryHours);
        await SendEmail(parentEmail, "Rivendosja e fjalëkalimit · Reset your password", body);
    }

    // ── Family verification ───────────────────────────────────────────────────

    public async Task SendFamilyEmailVerification(string email, Guid token, List<string> familyMemberNames,
        string verificationType, List<(string Name, string Email, string Password)>? familyMemberCredentials = null)
    {
        _logger.LogInformation("Starting family email-verification workflow for {Email}", email);
        var verifyUrl    = $"https://braingainalbania.al/verify-email?token={token}&verificationType={verificationType}";
        var memberNames  = string.Join(", ", familyMemberNames);

        var html = _tplFamilyVerification.Replace("{{FAMILY_MEMBER_NAMES}}", memberNames);

        if (familyMemberCredentials == null || !familyMemberCredentials.Any())
        {
            html = StripOptionalBlock(html);
        }
        else
        {
            html = ProcessRepeatBlock(html, familyMemberCredentials);
        }

        html = html.Replace("{{ACTION_URL}}", verifyUrl);
        await SendEmail(email, "Verifikimi i llogarisë familjare", ApplyGlobalTokens(html));
    }

    public async Task SendStudentCreatedBySchoolEmail(string email, Guid token, string password, string firstName,
        string lastName, string verificationType)
    {
        _logger.LogInformation("Starting student-creation email for {Email}", email);
        var verifyUrl = $"https://braingainalbania.al/verify-email?token={token}&verificationType={verificationType}";
        var html = _tplStudentCreated
            .Replace("{{USER_NAME}}",     $"{firstName} {lastName}")
            .Replace("{{USER_EMAIL}}",    email)
            .Replace("{{TEMP_PASSWORD}}", password)
            .Replace("{{ACTION_URL}}",    verifyUrl);
        await SendEmail(email, "Llogaria e nxënësit është krijuar", ApplyGlobalTokens(html));
    }

    // ── Parent / child credential emails ─────────────────────────────────────

    public async Task SendNewChildrenCredentialsToParent(string parentEmail, string parentName,
        List<(string Name, string Email, string Password)> credentials)
    {
        _logger.LogInformation("Sending children credentials to parent {ParentEmail}", parentEmail);
        var html = _tplChildrenCredentials.Replace("{{PARENT_NAME}}", parentName);
        html = ProcessRepeatBlock(html, credentials);
        await SendEmail(parentEmail, "Kredencialet e fëmijëve tuaj", ApplyGlobalTokens(html));
    }

    public async Task SendChildPasswordResetToParent(string parentEmail, string parentName, string childName,
        string childEmail, string newPassword)
    {
        _logger.LogInformation("Sending child password reset to parent {ParentEmail}", parentEmail);
        var html = _tplChildPasswordReset
            .Replace("{{PARENT_NAME}}",  parentName)
            .Replace("{{CHILD_NAME}}",   childName)
            .Replace("{{CHILD_EMAIL}}",  childEmail)
            .Replace("{{NEW_PASSWORD}}", newPassword);
        await SendEmail(parentEmail, "Fjalëkalimi i ri i fëmijës suaj", ApplyGlobalTokens(html));
    }

    // ── Supervisor flow emails ────────────────────────────────────────────────

    public async Task SendSupervisorApplicationNotification(string supervisorEmail, string supervisorName, string schoolName)
    {
        _logger.LogInformation("Supervisor application notification for {SupervisorEmail}", supervisorEmail);
        var adminUrl = _configuration["AppSettings:AdminPanelUrl"] ?? "https://braingainalbania.al/admin/supervisor-applications";
        var html = _tplSupervisorApplication
            .Replace("{{SUPERVISOR_NAME}}",  supervisorName)
            .Replace("{{SUPERVISOR_EMAIL}}", supervisorEmail)
            .Replace("{{SCHOOL_NAME}}",      schoolName)
            .Replace("{{ACTION_URL}}",       adminUrl);
        await SendEmail("fabiano.meoo98@gmail.com", "Aplikim i ri i Supervizorit", ApplyGlobalTokens(html));
    }

    public async Task SendSupervisorApprovalEmail(string supervisorEmail, string supervisorName,
        string packageSelectionLink, string? temporaryPassword = null)
    {
        _logger.LogInformation("Supervisor approval email for {SupervisorEmail}", supervisorEmail);
        var html = _tplSupervisorApproval
            .Replace("{{SUPERVISOR_NAME}}", supervisorName)
            .Replace("{{ACTION_URL}}",      packageSelectionLink);

        if (string.IsNullOrEmpty(temporaryPassword))
        {
            html = StripOptionalBlock(html);
        }
        else
        {
            html = html
                .Replace("{{SUPERVISOR_EMAIL}}", supervisorEmail)
                .Replace("{{TEMP_PASSWORD}}",    temporaryPassword);
        }

        await SendEmail(supervisorEmail, "Aplikimi juaj është pranuar", ApplyGlobalTokens(html));
    }

    public async Task SendSupervisorRejectionEmail(string supervisorEmail, string supervisorName, string reason)
    {
        _logger.LogInformation("Supervisor rejection email for {SupervisorEmail}", supervisorEmail);
        var html = _tplSupervisorRejection
            .Replace("{{SUPERVISOR_NAME}}", supervisorName)
            .Replace("{{REJECT_REASON}}",   reason);
        await SendEmail(supervisorEmail, "Aplikimi juaj nuk është pranuar", ApplyGlobalTokens(html));
    }

    public async Task SendSupervisorCredentialsEmail(string supervisorEmail, string supervisorName, string temporaryPassword)
    {
        _logger.LogInformation("Supervisor credentials email for {SupervisorEmail}", supervisorEmail);
        var loginUrl = (_configuration["AppSettings:BaseUrl"] ?? "https://braingainalbania.al") + "/login";
        var html = _tplSupervisorCredentials
            .Replace("{{SUPERVISOR_NAME}}",  supervisorName)
            .Replace("{{SUPERVISOR_EMAIL}}", supervisorEmail)
            .Replace("{{TEMP_PASSWORD}}",    temporaryPassword)
            .Replace("{{ACTION_URL}}",       loginUrl);
        await SendEmail(supervisorEmail, "Kredencialet tuaja të hyrjes", ApplyGlobalTokens(html));
    }

    // ── Student password emails ───────────────────────────────────────────────

    public async Task SendNewPasswordSetEmail(string studentEmail, string studentName, string newPassword)
    {
        _logger.LogInformation("New password set email for {StudentEmail}", studentEmail);
        var html = _tplStudentPasswordSet
            .Replace("{{STUDENT_NAME}}",  studentName)
            .Replace("{{STUDENT_EMAIL}}", studentEmail)
            .Replace("{{NEW_PASSWORD}}",  newPassword);
        await SendEmail(studentEmail, "Fjalëkalimi juaj është rivendosur", ApplyGlobalTokens(html));
    }

    public async Task SendStudentPasswordResetRequestToSupervisor(string supervisorEmail, string supervisorName,
        string studentName, string studentEmail, Guid resetToken)
    {
        _logger.LogInformation("Password reset request to supervisor {SupervisorEmail} for student {StudentEmail}", supervisorEmail, studentEmail);
        var supervisorPanelUrl = _configuration["AppSettings:SupervisorPanelUrl"] ?? "https://braingainalbania.al/supervisor/password-reset-requests";
        var html = _tplResetRequestToSupervisor
            .Replace("{{SUPERVISOR_NAME}}", supervisorName)
            .Replace("{{STUDENT_NAME}}",    studentName)
            .Replace("{{STUDENT_EMAIL}}",   studentEmail)
            .Replace("{{REQUEST_TIME}}",    DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm"))
            .Replace("{{ACTION_URL}}",      supervisorPanelUrl);
        await SendEmail(supervisorEmail, "Kërkesë për rivendosje të fjalëkalimit të nxënësit", ApplyGlobalTokens(html));
    }

    public async Task SendNewPasswordToSupervisor(string supervisorEmail, string supervisorName,
        string studentName, string studentEmail, string newPassword)
    {
        _logger.LogInformation("New password notification to supervisor {SupervisorEmail} for student {StudentEmail}", supervisorEmail, studentEmail);
        var html = _tplNewPasswordToSupervisor
            .Replace("{{SUPERVISOR_NAME}}", supervisorName)
            .Replace("{{STUDENT_NAME}}",    studentName)
            .Replace("{{STUDENT_EMAIL}}",   studentEmail)
            .Replace("{{NEW_PASSWORD}}",    newPassword);
        await SendEmail(supervisorEmail, "Fjalëkalimi i ri i nxënësit është gjeneruar", ApplyGlobalTokens(html));
    }

    // ── SMTP sender ───────────────────────────────────────────────────────────

    public async Task SendEmail(string email, string subject, string body)
    {
        var fromAddress = new MailAddress(
            Environment.GetEnvironmentVariable("EMAIL_FROM"),
            "Brain Gain Albania"
        );
        var toAddress = new MailAddress(email);

        using var smtp = new SmtpClient
        {
            Host        = Environment.GetEnvironmentVariable("EMAIL_SMTP_SERVER"),
            Port        = int.Parse(Environment.GetEnvironmentVariable("EMAIL_PORT") ?? "587"),
            EnableSsl   = true,
            Credentials = new NetworkCredential(
                Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
                Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
            )
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject    = subject,
            Body       = body,
            IsBodyHtml = true
        };

        try
        {
            _logger.LogInformation("Sending email to {Email}", email);
            await smtp.SendMailAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {Email}", email);
            throw;
        }
    }
}
