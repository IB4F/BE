using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class PaymentSessionRequest
    {
        public string Email { get; set; }
        public SubscriptionPackage Package { get; set; }
        public BillingInterval BillingInterval { get; set; }
        public int FamilyMemberCount { get; set; } = 1;
        public string RegistrationType { get; set; }
        public string RegistrationData { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
