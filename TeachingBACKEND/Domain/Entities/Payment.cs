namespace TeachingBACKEND.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; } // Set after user creation via webhook
        public User User { get; set; }
        public string Email { get; set; }
        public string RegistrationType { get; set; }
        public string RegistrationData { get; set; } // JSON string containing registration data
        public string StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; } = "eur";
        public string Status { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? SubscriptionPackageId { get; set; }
        public SubscriptionPackage? SubscriptionPackage { get; set; }
    }
}
