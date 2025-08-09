using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateStudentBySchoolDTO
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(20)]
        public string CurrentClass { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required, StringLength(100)]
        public string School { get; set; }

        [Required, StringLength(20)]
        public Guid PlanId { get; set; }
    }
} 