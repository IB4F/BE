using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services.Providers
{
    /// <summary>
    /// Manual bank transfer provider for BKT and Raiffeisen Albania.
    /// No external API — generates a unique reference and sends bank transfer instructions via email.
    /// Required env vars (BKT): MANUAL_BKT_IBAN, MANUAL_BKT_ACCOUNT_HOLDER
    /// Required env vars (Raiffeisen): MANUAL_RAIFFEISEN_IBAN, MANUAL_RAIFFEISEN_ACCOUNT_HOLDER
    /// </summary>
    public class ManualPaymentProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ILogger<ManualPaymentProvider> _logger;
        private readonly PaymentProvider _providerType;

        public PaymentProvider ProviderType => _providerType;

        public ManualPaymentProvider(
            IConfiguration configuration,
            IEmailService emailService,
            ILogger<ManualPaymentProvider> logger,
            PaymentProvider providerType)
        {
            _configuration = configuration;
            _emailService = emailService;
            _logger = logger;
            _providerType = providerType;
        }

        public async Task<PaymentSessionResult> CreateCheckoutSessionAsync(PaymentSessionRequest request)
        {
            var package = request.Package;
            var amountCents = CalculateAmount(package, request.BillingInterval, request.FamilyMemberCount);

            // Unique payment reference for tracking in bank statement
            var reference = $"SUB-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

            var (bankName, iban, accountHolder) = GetBankDetails();

            await _emailService.SendManualPaymentInstructionsAsync(
                request.Email, reference, amountCents, "ALL",
                bankName, iban, accountHolder);

            _logger.LogInformation("Manual payment reference generated: {Reference} for {Email}", reference, request.Email);

            return new PaymentSessionResult
            {
                SessionId = reference,
                Provider = _providerType,
                IsManual = true,
                ManualDetails = new ManualPaymentDetails
                {
                    BankName = bankName,
                    Iban = iban,
                    AccountHolder = accountHolder,
                    Reference = reference,
                    AmountCents = amountCents,
                    Currency = "ALL",
                    Instructions = $"Transferoni {amountCents / 100.0m:F2} ALL në llogarinë e mëposhtme me referencën {reference}."
                }
            };
        }

        /// <summary>
        /// Manual provider: cancellation is handled administratively, no external API.
        /// </summary>
        public Task<bool> CancelSubscriptionAsync(string externalSubscriptionId)
        {
            _logger.LogInformation("Manual cancel requested for reference: {Ref}", externalSubscriptionId);
            return Task.FromResult(true);
        }

        public Task<bool> PauseSubscriptionAsync(string externalSubscriptionId)
        {
            _logger.LogInformation("Manual pause requested for reference: {Ref}", externalSubscriptionId);
            return Task.FromResult(true);
        }

        public Task<bool> ResumeSubscriptionAsync(string externalSubscriptionId)
        {
            _logger.LogInformation("Manual resume requested for reference: {Ref}", externalSubscriptionId);
            return Task.FromResult(true);
        }

        private static long CalculateAmount(Domain.Entities.SubscriptionPackage package, BillingInterval interval, int familyMemberCount)
        {
            if (package.UserType == Domain.Enums.UserType.Family &&
                package.BasePrice.HasValue && package.PricePerAdditionalMember.HasValue)
            {
                var minMembers = package.MinFamilyMembers ?? 1;
                var additionalMembers = Math.Max(0, familyMemberCount - minMembers);
                var basePrice = interval == BillingInterval.Year ? package.YearlyPrice : package.MonthlyPrice;
                return basePrice + (additionalMembers * package.PricePerAdditionalMember.Value);
            }

            return interval == BillingInterval.Year ? package.YearlyPrice : package.MonthlyPrice;
        }

        private (string bankName, string iban, string accountHolder) GetBankDetails()
        {
            if (_providerType == PaymentProvider.BKT)
            {
                return (
                    "BKT — Banka Kombëtare Tregtare",
                    _configuration["MANUAL_BKT_IBAN"] ?? "AL00 0000 0000 0000 0000 0000 0000",
                    _configuration["MANUAL_BKT_ACCOUNT_HOLDER"] ?? "Company Name"
                );
            }

            return (
                "Raiffeisen Bank Albania",
                _configuration["MANUAL_RAIFFEISEN_IBAN"] ?? "AL00 0000 0000 0000 0000 0000 0000",
                _configuration["MANUAL_RAIFFEISEN_ACCOUNT_HOLDER"] ?? "Company Name"
            );
        }
    }
}