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
        
        public string? Reason { get; set; }
    }
}
