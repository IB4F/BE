using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class SubscriptionPackage
    {
        public Guid Id { get; set; }
        
        // Core Properties
        public string Name { get; set; }                    // "Student Basic Monthly"
        public string Description { get; set; }             // Package description
        
        // Classification
        public UserType UserType { get; set; }              // Student, Family, Supervisor
        public PackageTier Tier { get; set; }               // Basic, Standard, Premium
        public BillingInterval BillingInterval { get; set; } // Month, Year
        
        // Pricing (Fixed for Student/Supervisor, Base for Family)
        public long MonthlyPrice { get; set; }              // Price in cents
        public long YearlyPrice { get; set; }               // Price in cents
        
        // Family Dynamic Pricing (Family packages only)
        public long? BasePrice { get; set; }                // Base price for family
        public long? PricePerAdditionalMember { get; set; } // €10/month or €20/year
        public int? MinFamilyMembers { get; set; }          // Usually 1
        public int? MaxFamilyMembers { get; set; }          // Package limit
        
        // Stripe Integration
        public string StripeMonthlyPriceId { get; set; }
        public string StripeYearlyPriceId { get; set; }
        
        // Package Settings
        public int MaxUsers { get; set; }                   // User limit
        public int TrialDays { get; set; }                  // Trial period
        public bool IsActive { get; set; } = true;          // Package availability
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}
