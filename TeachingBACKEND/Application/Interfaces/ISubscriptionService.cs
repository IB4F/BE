using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<string> CreateSubscriptionAsync(SubscriptionRequestDTO dto);
        Task<SubscriptionResponseDTO> GetSubscriptionAsync(Guid subscriptionId);
        Task<SubscriptionResponseDTO> GetUserActiveSubscriptionAsync(Guid userId);
        Task<bool> CancelSubscriptionAsync(Guid subscriptionId, CancelSubscriptionDTO dto);
        Task<bool> PauseSubscriptionAsync(Guid subscriptionId);
        Task<bool> ResumeSubscriptionAsync(Guid subscriptionId);
        Task<bool> UpdateSubscriptionPlanAsync(Guid subscriptionId, ChangePlanDTO dto);
        Task HandleStripeSubscriptionWebhookAsync(HttpRequest request);
        Task<bool> IsUserSubscriptionActiveAsync(Guid userId);
        Task<DateTime?> GetUserSubscriptionExpiryAsync(Guid userId);
        Task<List<SubscriptionResponseDTO>> GetUserSubscriptionsAsync(Guid userId);
    }
}
