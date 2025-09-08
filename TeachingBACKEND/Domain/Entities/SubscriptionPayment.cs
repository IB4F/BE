using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class SubscriptionPayment
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        
        // Stripe Fields
        public string StripePaymentIntentId { get; set; }
        public string StripeInvoiceId { get; set; }
        
        // Payment Details
        public long Amount { get; set; }
        public string Currency { get; set; } = "eur";
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Billing Period
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
    }
}
