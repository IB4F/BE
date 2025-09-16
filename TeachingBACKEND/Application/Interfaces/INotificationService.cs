namespace TeachingBACKEND.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailVerification(string email, Guid token, string verificationType, string? password = null);
        Task SendPasswordResetEmail(string email, Guid resetToken);
        Task SendChildPasswordResetEmail(string parentEmail, string childFirstName, string childLastName, Guid resetToken);
        Task SendEmail(string email, string subject, string body);
        Task SendFamilyEmailVerification(string email, Guid token, List<string> familyMemberNames, string verificationType, List<(string Name, string Email, string Password)>? familyMemberCredentials = null);
        Task SendStudentCreatedBySchoolEmail(string email, Guid token, string password, string firstName, string lastName, string verificationType);
        
        // Supervisor FlowF Methods
        Task SendSupervisorApplicationNotification(string supervisorEmail, string supervisorName, string schoolName);
        Task SendSupervisorApprovalEmail(string supervisorEmail, string supervisorName, string packageSelectionLink, string temporaryPassword);
        Task SendSupervisorRejectionEmail(string supervisorEmail, string supervisorName, string reason);
        Task SendNewPasswordSetEmail(string studentEmail, string studentName, string newPassword);
        Task SendStudentPasswordResetRequestToSupervisor(string supervisorEmail, string supervisorName, string studentName, string studentEmail, Guid resetToken);
        Task SendNewPasswordToSupervisor(string supervisorEmail, string supervisorName, string studentName, string studentEmail, string newPassword);
    }
}
