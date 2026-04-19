using Stripe;
using Stripe.Checkout;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services.Providers
{
    public class StripePaymentProvider : IPaymentProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StripePaymentProvider> _logger;

        public PaymentProvider ProviderType => PaymentProvider.Stripe;

        public StripePaymentProvider(IConfiguration configuration, ILogger<StripePaymentProvider> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<PaymentSessionResult> CreateCheckoutSessionAsync(PaymentSessionRequest request)
        {
            var package = request.Package;

            string stripePriceId = request.BillingInterval switch
            {
                BillingInterval.Month => package.StripeMonthlyPriceId,
                BillingInterval.Year => package.StripeYearlyPriceId,
                _ => package.StripeMonthlyPriceId
            };

            if (string.IsNullOrEmpty(stripePriceId))
                throw new InvalidOperationException($"Stripe price ID not configured for package {package.Name} with interval {request.BillingInterval}");

            var subscriptionData = new SessionSubscriptionDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    { "registration_type", request.RegistrationType },
                    { "registration_data", request.RegistrationData },
                    { "subscription_package_id", package.Id.ToString() },
                    { "billing_interval", request.BillingInterval.ToString() }
                }
            };

            if (package.TrialDays > 0)
                subscriptionData.TrialPeriodDays = package.TrialDays;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "subscription",
                SuccessUrl = request.SuccessUrl,
                CancelUrl = request.CancelUrl,
                CustomerEmail = request.Email,
                LineItems = new List<SessionLineItemOptions>
                {
                    new() { Price = stripePriceId, Quantity = 1 }
                },
                SubscriptionData = subscriptionData
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            _logger.LogInformation("Stripe checkout session created: {SessionId}", session.Id);

            return new PaymentSessionResult
            {
                SessionId = session.Id,
                CheckoutUrl = session.Url,
                Provider = PaymentProvider.Stripe
            };
        }

        public async Task<bool> CancelSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var service = new Stripe.SubscriptionService();
                await service.UpdateAsync(externalSubscriptionId, new SubscriptionUpdateOptions
                {
                    CancelAtPeriodEnd = true
                });
                return true;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error canceling subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> PauseSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var service = new Stripe.SubscriptionService();
                await service.UpdateAsync(externalSubscriptionId, new SubscriptionUpdateOptions
                {
                    PauseCollection = new SubscriptionPauseCollectionOptions { Behavior = "void" }
                });
                return true;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error pausing subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }

        public async Task<bool> ResumeSubscriptionAsync(string externalSubscriptionId)
        {
            try
            {
                var service = new Stripe.SubscriptionService();
                await service.UpdateAsync(externalSubscriptionId, new SubscriptionUpdateOptions
                {
                    PauseCollection = null
                });
                return true;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error resuming subscription: {Id}", externalSubscriptionId);
                return false;
            }
        }
    }
}