using System.ComponentModel.DataAnnotations;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class ChangePlanDTO
    {
        [Required]
        public Guid NewPlanId { get; set; }
        
        [Required]
        public BillingInterval BillingInterval { get; set; }
        
        public bool Prorate { get; set; } = true;
        
        [MaxLength(500)]
        public string? Reason { get; set; }
        
        // Add validation attributes for better input validation
        [Range(typeof(bool), "false", "true")]
        public bool ConfirmChange { get; set; } = true; // Safety flag to prevent accidental changes
    }
}
