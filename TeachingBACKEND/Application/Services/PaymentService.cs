﻿using Stripe;
using Stripe.Checkout;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TeachingBACKEND.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;


        public PaymentService(ApplicationDbContext context, IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> CreateCheckoutSessionAsync(PaymentSessionRequestDTO dto, Guid userId)
        {
            try
            {
                _logger.LogInformation("Creating checkout session for email: {Email}, type: {Type}", dto.Email, dto.RegistrationType);

                var stripeSecretKey = _configuration["STRIPE_SECRET_KEY"];
                var stripePublishableKey = _configuration["STRIPE_PUBLISHABLE_KEY"];

                StripeConfiguration.ApiKey = stripeSecretKey;

                var plan = await _context.RegistrationPlans.FindAsync(dto.PlanId);
                if (plan == null)
                    throw new Exception("Invalid registraton plan selected.");

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "payment",
                    SuccessUrl = _configuration["STRIPE_SUCCESS_URL"],
                    CancelUrl = _configuration["STRIPE_CANCEL_URL"],
                    CustomerEmail = dto.Email,
                    LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "eur",
                            UnitAmount = plan.Price,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = plan.StripeProductName
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
                    UserId = userId,
                    Email = dto.Email,
                    RegistrationType = dto.RegistrationType,
                    StripeSessionId = session.Id,
                    Amount = plan.Price,
                    Currency = "eur",
                    Status = "pending",
                    PlanId = plan.Id,
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return session.Id;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Failed to create checkout session for email: {Email}", dto.Email);
                throw new Exception("Unable to create Stripe session. Please try again later");
            }
          
        }

        public async Task HandleStripeWebhookAsync(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            var secret = _configuration["STRIPE_WEBHOOK_SECRET"];

            _logger.LogInformation("Received Stripe webhook");

            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], secret);
                _logger.LogDebug("Stripe event type: {Type}", stripeEvent.Type);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stripe webhook error: {Message}", ex.Message);
                throw;
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                if (session == null)
                {
                    _logger.LogWarning("Stripe session object is null in webhook.");
                    return;
                }

                var payment = _context.Payments.FirstOrDefault(p => p.StripeSessionId == session.Id);
                if (payment != null)
                {
                    payment.Status = "succeeded";
                    payment.StripePaymentIntentId = session.PaymentIntentId;
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Payment succeeded for session: {SessionId}, email: {Email}", session.Id, payment.Email);
                }
                else
                {
                    _logger.LogWarning("No matching payment found for session: {SessionId}", session.Id);
                }
            }
        }
    }
}
