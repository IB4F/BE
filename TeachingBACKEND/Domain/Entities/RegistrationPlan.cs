namespace TeachingBACKEND.Domain.Entities
{
    public class RegistrationPlan
    {
        public Guid Id { get; set; }
        public string RegistrationPlanName { get; set; }
        public string Type { get; set; }
        
        // Pricing (for backward compatibility)
        public long Price { get; set; }
        
        // Subscription Pricing
        public long MonthlyPrice { get; set; }
        public long YearlyPrice { get; set; }
        public string StripeMonthlyPriceId { get; set; }
        public string StripeYearlyPriceId { get; set; }
        public string StripeProductId { get; set; }
        
        // Plan Details
        public string StripeProductName { get; set; }
        public bool IsFamilyPlan { get; set; }
        public string UserType { get; set; }
        public int MaxUsers { get; set; }
        
        // Subscription Settings
        public bool IsSubscription { get; set; } = true; // New field
        public int TrialDays { get; set; } = 0;
        public bool AllowCancellation { get; set; } = true;
        
        // Navigation
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
