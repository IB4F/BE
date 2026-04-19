using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public string Email { get; set; }
        public string RegistrationType { get; set; }
        public string RegistrationData { get; set; }

        // Provider
        public PaymentProvider Provider { get; set; } = PaymentProvider.Stripe;

        // Stripe fields (kept for backward compatibility)
        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        // Generic external provider session/transaction ID
        public string? ExternalSessionId { get; set; }

        public long Amount { get; set; }
        public string Currency { get; set; } = "eur";
        public string Status { get; set; } = "pending";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid? SubscriptionPackageId { get; set; }
        public SubscriptionPackage? SubscriptionPackage { get; set; }
    }
}
