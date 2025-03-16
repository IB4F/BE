namespace TeachingBACKEND.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailVerification(string email, Guid token);
            
    }
}
