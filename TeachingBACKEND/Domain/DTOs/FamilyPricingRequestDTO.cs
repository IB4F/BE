using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class FamilyPricingRequestDTO
    {
        [Required]
        [Range(1, 10, ErrorMessage = "Family members must be between 1 and 10")]
        public int FamilyMembers { get; set; }
    }
}
