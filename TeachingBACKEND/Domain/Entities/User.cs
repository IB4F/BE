using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        // Authentication
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Role & Approval Status
        public UserRole Role { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;

        public bool IsEmailVerified { get; set; } = false;
        public Guid? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiry { get; set; }

        // Common Fields
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        // Student-Specific Fields 
        public DateTime? DateOfBirth { get; set; }
        public string? CurrentClass { get; set; }

        // School-Specific Fields 
        public string? School { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Profession { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }


        //Reset Password
        public Guid? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
    }
}
