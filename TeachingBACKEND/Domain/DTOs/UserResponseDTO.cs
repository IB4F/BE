using System.Text.Json.Serialization;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string School { get; set; }
        public string SessionId { get; set; }
        public string CurrentClass { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsEmailVerified { get; set; }
        public string VerificationType { get; set; }
    }


}
