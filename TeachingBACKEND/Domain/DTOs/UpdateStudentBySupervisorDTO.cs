using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class UpdateStudentBySupervisorDTO
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string CurrentClass { get; set; } = string.Empty;

        [Required]
        public string DateOfBirth { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Notes { get; set; }
    }

    public class UpdatedStudentDTO
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CurrentClass { get; set; }
        public string School { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Notes { get; set; }
    }
}
