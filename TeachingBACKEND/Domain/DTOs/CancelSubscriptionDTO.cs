using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class CancelSubscriptionDTO
    {
        [Required]
        public string Reason { get; set; }
        
        public bool Immediately { get; set; } = false;
        
        public string? Feedback { get; set; }
    }
}
