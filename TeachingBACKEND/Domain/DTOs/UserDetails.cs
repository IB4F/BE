using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class ChildSummaryDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? CurrentClass { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; }
    }

    public class UserDetails
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CurrentClass { get; set; }
        public string? School { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Profession { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

        // Count fields for family members and supervisors
        public int? ChildrenCount { get; set; }
        public int? StudentsCount { get; set; }

        // For Family role: full list of children
        public List<ChildSummaryDto>? Children { get; set; }
    }
}
