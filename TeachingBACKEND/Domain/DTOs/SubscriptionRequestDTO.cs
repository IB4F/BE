using System.ComponentModel.DataAnnotations;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SubscriptionRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string RegistrationType { get; set; }

        [Required]
        public Guid SubscriptionPackageId { get; set; }

        [Required]
        public string RegistrationData { get; set; }

        [Required]
        public BillingInterval BillingInterval { get; set; } = BillingInterval.Month;

        /// <summary>
        /// Payment provider to use. Default: Stripe.
        /// Options: Stripe, Novalnet, Paddle, BKT, Raiffeisen
        /// </summary>
        public PaymentProvider Provider { get; set; } = PaymentProvider.Stripe;

        public int? FamilyMemberCount { get; set; } = 1;

        public string? CouponCode { get; set; }
    }
}
