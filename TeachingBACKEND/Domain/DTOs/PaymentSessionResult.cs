using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class PaymentSessionResult
    {
        public string? SessionId { get; set; }
        public string? CheckoutUrl { get; set; }
        public PaymentProvider Provider { get; set; }

        /// <summary>
        /// True for BKT/Raiffeisen manual bank transfers.
        /// </summary>
        public bool IsManual { get; set; }

        /// <summary>
        /// IBAN and bank details shown to user for manual transfers.
        /// </summary>
        public ManualPaymentDetails? ManualDetails { get; set; }
    }

    public class ManualPaymentDetails
    {
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string AccountHolder { get; set; }
        public string Reference { get; set; }
        public long AmountCents { get; set; }
        public string Currency { get; set; } = "ALL";
        public string Instructions { get; set; }
    }
}
