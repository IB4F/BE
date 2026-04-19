using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IPaymentProvider
    {
        PaymentProvider ProviderType { get; }

        Task<PaymentSessionResult> CreateCheckoutSessionAsync(PaymentSessionRequest request);

        /// <param name="externalSubscriptionId">Provider-specific subscription ID.</param>
        Task<bool> CancelSubscriptionAsync(string externalSubscriptionId);

        Task<bool> PauseSubscriptionAsync(string externalSubscriptionId);

        Task<bool> ResumeSubscriptionAsync(string externalSubscriptionId);
    }
}