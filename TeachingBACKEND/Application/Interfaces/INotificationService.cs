namespace TeachingBACKEND.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailVerification(string email, Guid token, string verificationType, string? password = null);
        Task SendPasswordResetEmail(string email, Guid resetToken);
        Task SendEmail(string email, string subject, string body);
        Task SendFamilyEmailVerification(string email, Guid token, List<string> familyMemberNames, string verificationType);
        Task SendStudentCreatedBySchoolEmail(string email, Guid token, string password, string firstName, string lastName, string verificationType);
    }
}
