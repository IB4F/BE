﻿namespace TeachingBACKEND.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailVerification(string email, Guid token);
        Task SendPasswordResetEmail(string email, Guid resetToken);
        Task SendEmail(string email, string subject, string body);

    }
}
