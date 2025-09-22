using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
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
        public int? ChildrenCount { get; set; } // For family members: number of children
        public int? StudentsCount { get; set; } // For supervisors: number of supervised students
    }
}
