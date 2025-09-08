using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SubscriptionResponseDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string StripeSubscriptionId { get; set; }
        public string StripeCustomerId { get; set; }
        public Guid PlanId { get; set; }
        public string PlanName { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CurrentPeriodStart { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public DateTime? TrialEnd { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public BillingInterval Interval { get; set; }
        public int IntervalCount { get; set; }
        public string RegistrationType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
