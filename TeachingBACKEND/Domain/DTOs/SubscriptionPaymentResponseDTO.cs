using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SubscriptionPaymentResponseDTO
    {
        public Guid Id { get; set; }
        public Guid SubscriptionId { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string StripeInvoiceId { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        
        // Additional subscription info for context
        public string PlanName { get; set; }
        public string SubscriptionStatus { get; set; }
        public BillingInterval Interval { get; set; }
    }
}
