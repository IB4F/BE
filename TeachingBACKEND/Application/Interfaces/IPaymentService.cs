using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(PaymentSessionRequestDTO dto, Guid userId);
        Task HandleStripeWebhookAsync(HttpRequest request);
    }
}
