using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class FamilyRegistrationDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Range(1, 10)]
        public int NumberOfFamilyMembers { get; set; }

        [Required]
        public string PlanId { get; set; }
    }
}
