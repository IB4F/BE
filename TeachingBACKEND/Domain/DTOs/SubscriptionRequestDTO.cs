using System.ComponentModel.DataAnnotations;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SubscriptionRequestDTO
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string RegistrationType { get; set; } // student, school, family
        
        [Required]
        public Guid PlanId { get; set; }
        
        [Required]
        public string RegistrationData { get; set; } // JSON string of registration data
        
        [Required]
        public BillingInterval BillingInterval { get; set; } = BillingInterval.Month;
        
        public int? FamilyMemberCount { get; set; } = 1;
        
        public string? CouponCode { get; set; }
    }
}
