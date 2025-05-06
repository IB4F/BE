using Stripe;
using Stripe.Checkout;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public PaymentService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> CreateCheckoutSessionAsync(PaymentSessionRequestDTO dto)
        {
            var price = dto.RegistrationType.ToLower() switch
            {
                "school" => 9900,   //$99
                "student" => 4900,  //$49
                _ => throw new Exception("Invalid registration type")
            };


            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                SuccessUrl = "https://your-frontend.com/success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://your-frontend.com/cancel",
                CustomerEmail = dto.Email,
                LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        UnitAmount = price,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{dto.RegistrationType} Registration"
                        }
                    },
                    Quantity = 1
                }
            }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            var payment = new Payment
            {
                Email = dto.Email,
                RegistrationType = dto.RegistrationType,
                StripeSessionId = session.Id,
                Amount = price,
                Currency = "eur",
                Status = "pending"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return session.Id;
        }

        public async Task HandleStripeWebhookAsync(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            var secret = _configuration["Stripe:WebhookSecret"];

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], secret);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Webhook error: " + ex.Message);
                throw;
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                var payment = _context.Payments.FirstOrDefault(p => p.StripeSessionId == session.Id);
                if (payment != null)
                {
                    payment.Status = "succeeded";
                    payment.StripePaymentIntentId = session.PaymentIntentId;
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
