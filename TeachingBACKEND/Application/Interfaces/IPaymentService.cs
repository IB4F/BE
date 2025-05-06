using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(PaymentSessionRequestDTO dto);
        Task HandleStripeWebhookAsync(HttpRequest request);
    }
}
