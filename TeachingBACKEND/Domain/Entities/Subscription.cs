using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        
        // Stripe Fields
        public string StripeSubscriptionId { get; set; }
        public string StripeCustomerId { get; set; }
        public string StripePriceId { get; set; }
        
        // Subscription Details
        public Guid SubscriptionPackageId { get; set; }
        public SubscriptionPackage SubscriptionPackage { get; set; }
        
        // Status & Dates
        public SubscriptionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public DateTime? TrialEnd { get; set; }
        
        // Billing
        public string Currency { get; set; } = "eur";
        public long Amount { get; set; }
        public BillingInterval Interval { get; set; } // monthly, yearly
        public int IntervalCount { get; set; } = 1;
        
        // Metadata
        public string RegistrationType { get; set; } // student, school, family
        public string RegistrationData { get; set; } // JSON for webhook processing
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation
        public ICollection<SubscriptionPayment> Payments { get; set; } = new List<SubscriptionPayment>();
    }
}
