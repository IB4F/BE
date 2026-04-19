using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<PaymentSessionResult> CreateSubscriptionAsync(SubscriptionRequestDTO dto);
        Task<PaymentSessionResult> CreateSupervisorSubscriptionAsync(SupervisorSubscriptionRequestDTO dto);
        Task<SubscriptionResponseDTO> GetSubscriptionAsync(Guid subscriptionId);
        Task<SubscriptionResponseDTO> GetUserActiveSubscriptionAsync(Guid userId);
        Task<bool> CancelSubscriptionAsync(Guid subscriptionId, CancelSubscriptionDTO dto);
        Task<bool> PauseSubscriptionAsync(Guid subscriptionId);
        Task<bool> ResumeSubscriptionAsync(Guid subscriptionId);
        Task<bool> UpdateSubscriptionPlanAsync(Guid subscriptionId, ChangePlanDTO dto);
        Task<bool> IsUserSubscriptionActiveAsync(Guid userId);
        Task<DateTime?> GetUserSubscriptionExpiryAsync(Guid userId);
        Task<List<SubscriptionResponseDTO>> GetUserSubscriptionsAsync(Guid userId);
        Task<List<SubscriptionPaymentResponseDTO>> GetUserPaymentHistoryAsync(Guid userId);

        // Webhook handlers per provider
        Task HandleStripeSubscriptionWebhookAsync(HttpRequest request);
        Task HandleNovalnetWebhookAsync(HttpRequest request);
        Task HandlePaddleWebhookAsync(HttpRequest request);

        // Manual payment (BKT / Raiffeisen)
        Task<bool> ConfirmManualPaymentAsync(string paymentReference, Guid confirmedByAdminId);
        Task SendManualPaymentReminderAsync(string paymentReference);
    }
}
