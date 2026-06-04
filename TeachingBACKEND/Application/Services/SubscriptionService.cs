using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Application.Services.Providers;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly PaymentProviderFactory _providerFactory;
        private readonly NovalnetPaymentProvider _novalnetProvider;
        private readonly PaddlePaymentProvider _paddleProvider;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IPasswordService passwordService,
            INotificationService notificationService,
            IEmailService emailService,
            PaymentProviderFactory providerFactory,
            NovalnetPaymentProvider novalnetProvider,
            PaddlePaymentProvider paddleProvider,
            ILogger<SubscriptionService> logger)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
            _notificationService = notificationService;
            _emailService = emailService;
            _providerFactory = providerFactory;
            _novalnetProvider = novalnetProvider;
            _paddleProvider = paddleProvider;
            _logger = logger;
        }

        public async Task<PaymentSessionResult> CreateSubscriptionAsync(SubscriptionRequestDTO dto)
        {
            using var activity = _logger.BeginScope("CreateSubscriptionAsync");

            try
            {
                _logger.LogInformation("Creating subscription: Email={Email}, Provider={Provider}, Package={PackageId}",
                    dto.Email, dto.Provider, dto.SubscriptionPackageId);

                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == dto.SubscriptionPackageId)
                    ?? throw new ArgumentException("Subscription package not found");

                var sessionRequest = new PaymentSessionRequest
                {
                    Email = dto.Email,
                    Package = package,
                    BillingInterval = dto.BillingInterval,
                    FamilyMemberCount = dto.FamilyMemberCount ?? 1,
                    RegistrationType = dto.RegistrationType,
                    RegistrationData = dto.RegistrationData,
                    SuccessUrl = _configuration["STRIPE_SUCCESS_URL"] ?? "",
                    CancelUrl = _configuration["STRIPE_CANCEL_URL"] ?? ""
                };

                var provider = _providerFactory.GetProvider(dto.Provider);
                var result = await provider.CreateCheckoutSessionAsync(sessionRequest);

                var payment = new Payment
                {
                    Email = dto.Email,
                    RegistrationType = dto.RegistrationType,
                    RegistrationData = dto.RegistrationData,
                    Provider = dto.Provider,
                    StripeSessionId = dto.Provider == PaymentProvider.Stripe ? result.SessionId : null,
                    ExternalSessionId = dto.Provider != PaymentProvider.Stripe ? result.SessionId : null,
                    Amount = 0,
                    Currency = "eur",
                    Status = result.IsManual ? "awaiting_transfer" : "pending",
                    SubscriptionPackageId = dto.SubscriptionPackageId
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Payment record created: {PaymentId}, Provider={Provider}, SessionId={SessionId}",
                    payment.Id, dto.Provider, result.SessionId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription for {Email} with provider {Provider}", dto.Email, dto.Provider);
                throw;
            }
        }

        public async Task<PaymentSessionResult> CreateSupervisorSubscriptionAsync(SupervisorSubscriptionRequestDTO dto)
        {
            using var activity = _logger.BeginScope("CreateSupervisorSubscriptionAsync");

            try
            {
                var supervisorApplication = await _context.SupervisorApplications
                    .FirstOrDefaultAsync(sa => sa.Id == dto.SupervisorApplicationId && sa.ApprovalStatus == ApprovalStatus.Approved)
                    ?? throw new ArgumentException("Approved supervisor application not found");

                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == dto.SubscriptionPackageId)
                    ?? throw new ArgumentException("Subscription package not found");

                var registrationData = JsonSerializer.Serialize(new SupervisorRegistrationDTO
                {
                    SupervisorApplicationId = dto.SupervisorApplicationId
                });

                var sessionRequest = new PaymentSessionRequest
                {
                    Email = supervisorApplication.ContactPersonEmail,
                    Package = package,
                    BillingInterval = dto.BillingInterval,
                    RegistrationType = "supervisor",
                    RegistrationData = registrationData,
                    SuccessUrl = _configuration["STRIPE_SUCCESS_URL"] ?? "",
                    CancelUrl = _configuration["STRIPE_CANCEL_URL"] ?? ""
                };

                var provider = _providerFactory.GetProvider(dto.Provider);
                var result = await provider.CreateCheckoutSessionAsync(sessionRequest);

                var payment = new Payment
                {
                    Email = supervisorApplication.ContactPersonEmail,
                    RegistrationType = "supervisor",
                    RegistrationData = registrationData,
                    Provider = dto.Provider,
                    StripeSessionId = dto.Provider == PaymentProvider.Stripe ? result.SessionId : null,
                    ExternalSessionId = dto.Provider != PaymentProvider.Stripe ? result.SessionId : null,
                    Amount = 0,
                    Currency = "eur",
                    Status = result.IsManual ? "awaiting_transfer" : "pending",
                    SubscriptionPackageId = dto.SubscriptionPackageId
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Supervisor payment record created: Provider={Provider}, SessionId={SessionId}",
                    dto.Provider, result.SessionId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supervisor subscription for application {ApplicationId}", dto.SupervisorApplicationId);
                throw;
            }
        }

        public async Task<SubscriptionResponseDTO> GetSubscriptionAsync(Guid subscriptionId)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == subscriptionId);

            if (subscription == null)
            {
                throw new ArgumentException("Subscription not found");
            }

            return MapToResponseDTO(subscription);
        }

        public async Task<SubscriptionResponseDTO> GetUserActiveSubscriptionAsync(Guid userId)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .AsNoTracking() // Performance optimization
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

            if (subscription == null)
            {
                return null;
            }

            return MapToResponseDTO(subscription);
        }

        public async Task<bool> CancelSubscriptionAsync(Guid subscriptionId, CancelSubscriptionDTO dto)
        {
            try
            {
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.Id == subscriptionId);

                if (subscription == null) return false;

                var externalId = GetExternalSubscriptionId(subscription);
                if (!string.IsNullOrEmpty(externalId))
                {
                    var provider = _providerFactory.GetProvider(subscription.Provider);
                    await provider.CancelSubscriptionAsync(externalId);
                }

                subscription.Status = SubscriptionStatus.Active;
                subscription.CancelAtPeriodEnd = true;
                subscription.CanceledAt = null;
                subscription.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Canceled subscription: {SubscriptionId}, Provider: {Provider}", subscriptionId, subscription.Provider);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error canceling subscription: {SubscriptionId}", subscriptionId);
                return false;
            }
        }

        public async Task<bool> PauseSubscriptionAsync(Guid subscriptionId)
        {
            try
            {
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.Id == subscriptionId);

                if (subscription == null) return false;

                var externalId = GetExternalSubscriptionId(subscription);
                if (!string.IsNullOrEmpty(externalId))
                {
                    var provider = _providerFactory.GetProvider(subscription.Provider);
                    await provider.PauseSubscriptionAsync(externalId);
                }

                subscription.Status = SubscriptionStatus.Paused;
                subscription.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Paused subscription: {SubscriptionId}, Provider: {Provider}", subscriptionId, subscription.Provider);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error pausing subscription: {SubscriptionId}", subscriptionId);
                return false;
            }
        }

        public async Task<bool> ResumeSubscriptionAsync(Guid subscriptionId)
        {
            try
            {
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.Id == subscriptionId);

                if (subscription == null) return false;

                var externalId = GetExternalSubscriptionId(subscription);
                if (!string.IsNullOrEmpty(externalId))
                {
                    var provider = _providerFactory.GetProvider(subscription.Provider);
                    await provider.ResumeSubscriptionAsync(externalId);
                }

                subscription.Status = SubscriptionStatus.Active;
                subscription.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                _logger.LogInformation("Resumed subscription: {SubscriptionId}, Provider: {Provider}", subscriptionId, subscription.Provider);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resuming subscription: {SubscriptionId}", subscriptionId);
                return false;
            }
        }

        private static string? GetExternalSubscriptionId(Domain.Entities.Subscription subscription) =>
            subscription.Provider == PaymentProvider.Stripe
                ? subscription.StripeSubscriptionId
                : subscription.ExternalSubscriptionId;

        public async Task<bool> UpdateSubscriptionPlanAsync(Guid subscriptionId, ChangePlanDTO dto)
        {
            try
            {
                _logger.LogInformation("Starting subscription plan update for subscription: {SubscriptionId}, newPlanId: {NewPlanId}", 
                    subscriptionId, dto.NewPlanId);

                // Get the subscription with validation
                var subscription = await _context.Subscriptions
                    .Include(s => s.SubscriptionPackage)
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == subscriptionId);

                if (subscription == null)
                {
                    _logger.LogWarning("Subscription not found: {SubscriptionId}", subscriptionId);
                    return false;
                }

                // Validate subscription can be updated
                if (subscription.Status != SubscriptionStatus.Active && subscription.Status != SubscriptionStatus.Trialing)
                {
                    _logger.LogWarning("Cannot update subscription with status: {Status}", subscription.Status);
                    return false;
                }

                // Validate new package exists and is active
                var newPackage = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == dto.NewPlanId && p.IsActive);

                if (newPackage == null)
                {
                    _logger.LogWarning("New subscription package not found or inactive: {PackageId}", dto.NewPlanId);
                    return false;
                }

                // Require confirmation for plan changes
                if (!dto.ConfirmChange)
                {
                    _logger.LogWarning("Plan change not confirmed: {SubscriptionId}", subscriptionId);
                    return false;
                }

                // Check if user is changing to same plan (prevent unnecessary updates)
                if (subscription.SubscriptionPackageId == dto.NewPlanId && 
                    subscription.Interval == dto.BillingInterval)
                {
                    _logger.LogInformation("No plan change needed - same plan and interval");
                    return true;
                }

                // Determine the new Stripe Price ID
                string newStripePriceId = dto.BillingInterval switch
                {
                    BillingInterval.Month => newPackage.StripeMonthlyPriceId,
                    BillingInterval.Year => newPackage.StripeYearlyPriceId,
                    _ => newPackage.StripeMonthlyPriceId
                };

                if (string.IsNullOrEmpty(newStripePriceId))
                {
                    _logger.LogError("Invalid Stripe Price ID for package: {PackageId}, billing interval: {BillingInterval}", 
                        dto.NewPlanId, dto.BillingInterval);
                    return false;
                }

                // Get current Stripe subscription to find correct subscription item ID

                var stripeService = new Stripe.SubscriptionService();
                
                var currentSubscription = await stripeService.GetAsync(subscription.StripeSubscriptionId);
                if (currentSubscription == null)
                {
                    _logger.LogError("Stripe subscription not found: {StripeSubscriptionId}", subscription.StripeSubscriptionId);
                    return false;
                }

                var subscriptionItemId = currentSubscription.Items.Data.FirstOrDefault()?.Id;
                if (string.IsNullOrEmpty(subscriptionItemId))
                {
                    _logger.LogError("No subscription items found in Stripe subscription: {StripeSubscriptionId}", 
                        subscription.StripeSubscriptionId);
                    return false;
                }

                // Update in Stripe with correct subscription item ID
                var updateOptions = new SubscriptionUpdateOptions
                {
                    Items = new List<SubscriptionItemOptions>
                    {
                        new()
                        {
                            Id = subscriptionItemId,
                            Price = newStripePriceId
                        }
                    },
                    ProrationBehavior = dto.Prorate ? "create_prorations" : "none"
                };

                _logger.LogInformation("Updating Stripe subscription {StripeSubscriptionId} to new plan {NewPlanId}", 
                    subscription.StripeSubscriptionId, dto.NewPlanId);

                await stripeService.UpdateAsync(subscription.StripeSubscriptionId, updateOptions);

                // Update local subscription only after successful Stripe update
                subscription.SubscriptionPackageId = dto.NewPlanId;
                subscription.StripePriceId = newStripePriceId;
                subscription.Interval = dto.BillingInterval;
                subscription.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully updated subscription plan: {SubscriptionId} from {OldPackage} to {NewPackage}", 
                    subscriptionId, subscription.SubscriptionPackage?.Name ?? "Unknown", newPackage.Name);
                return true;
            }
            catch (StripeException stripeEx)
            {
                _logger.LogError(stripeEx, "Stripe error updating subscription plan: {SubscriptionId}. Stripe error: {StripeError}", 
                    subscriptionId, stripeEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription plan: {SubscriptionId}", subscriptionId);
                return false;
            }
        }

        public async Task HandleStripeSubscriptionWebhookAsync(HttpRequest request)
        {
            try
            {
                var json = await new StreamReader(request.Body).ReadToEndAsync();
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    _configuration["STRIPE_WEBHOOK_SECRET"]
                );

                _logger.LogInformation("Received Stripe webhook: {EventType}", stripeEvent.Type);

                switch (stripeEvent.Type)
                {
                    case "checkout.session.completed":
                        await HandleCheckoutSessionCompletedAsync(stripeEvent);
                        break;
                    case "customer.subscription.created":
                        await HandleSubscriptionCreatedAsync(stripeEvent);
                        break;
                    case "customer.subscription.updated":
                        await HandleSubscriptionUpdatedAsync(stripeEvent);
                        break;
                    case "customer.subscription.deleted":
                        await HandleSubscriptionDeletedAsync(stripeEvent);
                        break;
                    case "invoice.payment_succeeded":
                        await HandleInvoicePaymentSucceededAsync(stripeEvent);
                        break;
                    case "invoice.payment_failed":
                        await HandleInvoicePaymentFailedAsync(stripeEvent);
                        break;
                    default:
                        _logger.LogInformation("Unhandled webhook event type: {EventType}", stripeEvent.Type);
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Stripe webhook");
                throw;
            }
        }

        public async Task<bool> IsUserSubscriptionActiveAsync(Guid userId)
        {
            var subscription = await _context.Subscriptions
                .AsNoTracking() // Performance optimization for read-only checks
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

            return subscription != null && subscription.CurrentPeriodEnd > DateTime.UtcNow;
        }

        public async Task<DateTime?> GetUserSubscriptionExpiryAsync(Guid userId)
        {
            var subscription = await _context.Subscriptions
                .AsNoTracking() // Performance optimization for read-only checks
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

            return subscription?.CurrentPeriodEnd;
        }

        public async Task<List<SubscriptionResponseDTO>> GetUserSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .AsNoTracking() // Performance optimization
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return subscriptions.Select(MapToResponseDTO).ToList();
        }

        public async Task<List<SubscriptionPaymentResponseDTO>> GetUserPaymentHistoryAsync(Guid userId)
        {
            var payments = await _context.SubscriptionPayments
                .Include(sp => sp.Subscription)
                    .ThenInclude(s => s.SubscriptionPackage)
                .AsNoTracking() // Performance optimization for read-only data
                .Where(sp => sp.Subscription.UserId == userId)
                .OrderByDescending(sp => sp.PaidAt)
                .ToListAsync();

            return payments.Select(payment => new SubscriptionPaymentResponseDTO
            {
                Id = payment.Id,
                SubscriptionId = payment.SubscriptionId,
                StripePaymentIntentId = payment.StripePaymentIntentId,
                StripeInvoiceId = payment.StripeInvoiceId,
                Amount = payment.Amount,
                Currency = payment.Currency,
                Status = payment.Status,
                PaidAt = payment.PaidAt,
                CreatedAt = payment.CreatedAt,
                PeriodStart = payment.PeriodStart,
                PeriodEnd = payment.PeriodEnd,
                PlanName = payment.Subscription?.SubscriptionPackage?.Name ?? "Unknown Plan",
                SubscriptionStatus = payment.Subscription?.Status.ToString() ?? "Unknown",
                Interval = payment.Subscription?.Interval ?? BillingInterval.Month
            }).ToList();
        }

        private async Task HandleSubscriptionCreatedAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Stripe.Subscription;
            if (subscription == null) return;

            try
            {
                _logger.LogInformation("Processing subscription created webhook for subscription: {SubscriptionId}", subscription.Id);

                // Idempotency check — Stripe can retry webhooks, avoid duplicate user/subscription creation
                var alreadyExists = await _context.Subscriptions
                    .AnyAsync(s => s.StripeSubscriptionId == subscription.Id);
                if (alreadyExists)
                {
                    _logger.LogInformation("Subscription already processed, skipping: {SubscriptionId}", subscription.Id);
                    return;
                }

                // Get metadata
                var registrationType = subscription.Metadata.GetValueOrDefault("registration_type");
                var registrationData = subscription.Metadata.GetValueOrDefault("registration_data");
                var planIdString = subscription.Metadata.GetValueOrDefault("subscription_package_id");
                
                _logger.LogInformation("Metadata - RegistrationType: {RegistrationType}, RegistrationData: {RegistrationData}, PlanId: {PlanId}", 
                    registrationType, registrationData, planIdString);
                
                if (string.IsNullOrEmpty(registrationType) || string.IsNullOrEmpty(registrationData) || string.IsNullOrEmpty(planIdString))
                {
                    _logger.LogError("Missing required metadata for subscription: {SubscriptionId}", subscription.Id);
                    return;
                }
                
                var planId = Guid.Parse(planIdString);

                // Create user from registration data
                var user = await CreateUserFromSubscriptionAsync(subscription, registrationType, registrationData, planId);

                // Create subscription record
                var subscriptionEntity = new Domain.Entities.Subscription
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    StripeSubscriptionId = subscription.Id,
                    StripeCustomerId = subscription.CustomerId,
                    StripePriceId = subscription.Items.Data[0].Price.Id,
                    SubscriptionPackageId = planId,
                    Status = MapStripeStatus(subscription.Status),
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndedAt,
                    CurrentPeriodStart = subscription.Items.Data[0].CurrentPeriodStart,
                    CurrentPeriodEnd = subscription.Items.Data[0].CurrentPeriodEnd,
                    TrialEnd = subscription.TrialEnd,
                    Currency = subscription.Currency,
                    Amount = subscription.Items.Data[0].Price.UnitAmount ?? 0,
                    Interval = MapStripeInterval(subscription.Items.Data[0].Price.Recurring.Interval),
                    IntervalCount = (int)subscription.Items.Data[0].Price.Recurring.IntervalCount,
                    RegistrationType = registrationType,
                    RegistrationData = registrationData,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Subscriptions.Add(subscriptionEntity);

                // Update user's active subscription
                user.ActiveSubscriptionId = subscriptionEntity.Id;
                user.SubscriptionExpiresAt = subscriptionEntity.CurrentPeriodEnd;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Created subscription for user: {UserId}", user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling subscription created webhook");
            }
        }

        private async Task HandleSubscriptionUpdatedAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Stripe.Subscription;
            if (subscription == null) return;

            try
            {
                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == subscription.Id);

                if (subscriptionEntity != null)
                {
                    var oldStatus = subscriptionEntity.Status;
                    subscriptionEntity.Status = MapStripeStatus(subscription.Status);
                    subscriptionEntity.EndDate = subscription.EndedAt;
                    subscriptionEntity.CurrentPeriodStart = subscription.Items.Data[0].CurrentPeriodStart;
                    subscriptionEntity.CurrentPeriodEnd = subscription.Items.Data[0].CurrentPeriodEnd;
                    subscriptionEntity.TrialEnd = subscription.TrialEnd;
                    subscriptionEntity.UpdatedAt = DateTime.UtcNow;

                    // Handle period-end cancellation
                    if (subscription.CancelAtPeriodEnd && 
                        oldStatus == SubscriptionStatus.Active && 
                        subscriptionEntity.Status == SubscriptionStatus.Canceled)
                    {
                        subscriptionEntity.CanceledAt = DateTime.UtcNow;
                        subscriptionEntity.CancelAtPeriodEnd = false; // Mark as fulfilled
                        
                        _logger.LogInformation("Subscription completed period-end cancellation: {SubscriptionId}", subscriptionEntity.Id);
                    }

                    // Update user's subscription expiry
                    var user = await _context.Users.FindAsync(subscriptionEntity.UserId);
                    if (user != null)
                    {
                        user.SubscriptionExpiresAt = subscriptionEntity.CurrentPeriodEnd;
                    }

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Updated subscription: {SubscriptionId}", subscriptionEntity.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling subscription updated webhook");
            }
        }

        private async Task HandleSubscriptionDeletedAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Stripe.Subscription;
            if (subscription == null) return;

            try
            {
                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == subscription.Id);

                if (subscriptionEntity != null)
                {
                    subscriptionEntity.Status = SubscriptionStatus.Canceled;
                    subscriptionEntity.EndDate = subscription.EndedAt;
                    subscriptionEntity.CanceledAt = DateTime.UtcNow;
                    subscriptionEntity.UpdatedAt = DateTime.UtcNow;

                    // Clear user's active subscription
                    var user = await _context.Users.FindAsync(subscriptionEntity.UserId);
                    if (user != null)
                    {
                        user.ActiveSubscriptionId = null;
                        user.SubscriptionExpiresAt = null;
                    }

                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Deleted subscription: {SubscriptionId}", subscriptionEntity.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling subscription deleted webhook");
            }
        }

        private async Task HandleInvoicePaymentSucceededAsync(Event stripeEvent)
        {
            var invoice = stripeEvent.Data.Object as Invoice;
            if (invoice == null) return;

            try
            {
                var stripeSubscriptionId = invoice.Parent?.SubscriptionDetails?.SubscriptionId;

                if (string.IsNullOrEmpty(stripeSubscriptionId)) return;

                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == stripeSubscriptionId);

                if (subscriptionEntity != null)
                {
                    // Create subscription payment record
                    var payment = new SubscriptionPayment
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionId = subscriptionEntity.Id,
                        StripePaymentIntentId = invoice.Payments?.Data?.FirstOrDefault(p => p.Payment.PaymentIntentId != null)?.Payment.PaymentIntentId,
                        StripeInvoiceId = invoice.Id,
                        Amount = invoice.AmountPaid,
                        Currency = invoice.Currency,
                        Status = PaymentStatus.Succeeded,
                        PaidAt = invoice.StatusTransitions.PaidAt ?? DateTime.UtcNow,
                        PeriodStart = invoice.PeriodStart,
                        PeriodEnd = invoice.PeriodEnd,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.SubscriptionPayments.Add(payment);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Recorded successful payment for subscription: {SubscriptionId}", subscriptionEntity.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling invoice payment succeeded webhook");
            }
        }

        private async Task HandleInvoicePaymentFailedAsync(Event stripeEvent)
        {
            var invoice = stripeEvent.Data.Object as Invoice;
            if (invoice == null) return;

            try
            {
                var stripeSubscriptionId = invoice.Parent?.SubscriptionDetails?.SubscriptionId;

                if (string.IsNullOrEmpty(stripeSubscriptionId)) return;

                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == stripeSubscriptionId);

                if (subscriptionEntity != null)
                {
                    // Mark subscription as past due — Stripe will retry payment automatically
                    subscriptionEntity.Status = SubscriptionStatus.PastDue;
                    subscriptionEntity.UpdatedAt = DateTime.UtcNow;

                    // Create subscription payment record
                    var payment = new SubscriptionPayment
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionId = subscriptionEntity.Id,
                        StripePaymentIntentId = invoice.Payments?.Data?.FirstOrDefault(p => p.Payment.PaymentIntentId != null)?.Payment.PaymentIntentId,
                        StripeInvoiceId = invoice.Id,
                        Amount = invoice.AmountDue,
                        Currency = invoice.Currency,
                        Status = PaymentStatus.Failed,
                        PaidAt = DateTime.UtcNow,
                        PeriodStart = invoice.PeriodStart,
                        PeriodEnd = invoice.PeriodEnd,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.SubscriptionPayments.Add(payment);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Recorded failed payment and set PastDue for subscription: {SubscriptionId}", subscriptionEntity.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling invoice payment failed webhook");
            }
        }

        private async Task<User> CreateUserFromSubscriptionAsync(Stripe.Subscription subscription, string registrationType, string registrationData, Guid planId)
        {
            _logger.LogInformation("Creating user from subscription with type: {RegistrationType}", registrationType);
            
            try
            {
                // Deserialize registration data
                var registrationDataObj = JsonSerializer.Deserialize<object>(registrationData);
                _logger.LogInformation("Successfully deserialized registration data");

                // Create user based on registration type
                var user = registrationType switch
                {
                    "student" => await CreateStudentFromSubscriptionAsync(registrationData, planId),
                    "school" => await CreateSchoolFromSubscriptionAsync(registrationData, planId),
                    "family" => await CreateFamilyFromSubscriptionAsync(registrationData, planId),
                    "supervisor" => await CreateSupervisorFromSubscriptionAsync(registrationData, planId),
                    _ => throw new ArgumentException($"Unknown registration type: {registrationType}")
                };
                
                _logger.LogInformation("Successfully created user: {UserId} with email: {Email}", user.Id, user.Email);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user from subscription data. RegistrationType: {RegistrationType}, RegistrationData: {RegistrationData}", 
                    registrationType, registrationData);
                throw;
            }
        }

        private async Task<User> CreateStudentFromSubscriptionAsync(string registrationData, Guid planId)
        {
            var dto = JsonSerializer.Deserialize<StudentRegistrationDTO>(registrationData);

            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existing != null)
                return existing;

            var currentTermsVersion = _configuration["AppSettings:CurrentTermsVersion"] ?? "2025-06-01";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = UserRole.Student,
                ApprovalStatus = ApprovalStatus.Approved,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                CurrentClass = dto.CurrentClass,
                School = dto.School,
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid(),
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                CreateAt = DateTime.UtcNow,
                TermsAcceptedAt = DateTime.UtcNow,
                TermsVersion = currentTermsVersion
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (user.EmailVerificationToken.HasValue)
            {
                await _notificationService.SendEmailVerification(user.Email, user.EmailVerificationToken.Value, "email");
            }

            return user;
        }

        private async Task<User> CreateSchoolFromSubscriptionAsync(string registrationData, Guid planId)
        {
            var dto = JsonSerializer.Deserialize<SchoolRegistrationDTO>(registrationData);

            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existing != null)
                return existing;

            // Generate a temporary password for school users
            var tempPassword = Guid.NewGuid().ToString("N")[..8];

            var currentTermsVersion = _configuration["AppSettings:CurrentTermsVersion"] ?? "2025-06-01";
            var schoolUser = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(tempPassword),
                Role = UserRole.Supervisor,
                ApprovalStatus = ApprovalStatus.Approved,
                FirstName = "School", // Default values since SchoolRegistrationDTO doesn't have these
                LastName = "User",
                School = dto.SchoolName,
                PhoneNumber = dto.PhoneNumber,
                Profession = dto.Profession,
                City = dto.City,
                PostalCode = dto.PostalCode,
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid(),
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                CreateAt = DateTime.UtcNow,
                TermsAcceptedAt = DateTime.UtcNow,
                TermsVersion = currentTermsVersion
            };

            _context.Users.Add(schoolUser);
            await _context.SaveChangesAsync();

            // Register all students associated with the school
            if (dto.Students != null && dto.Students.Any())
            {
                foreach (var studentDto in dto.Students)
                {
                    // Check if student email already exists
                    var existingStudent = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == studentDto.Email.ToLower());
                    if (existingStudent != null)
                    {
                        // Skip existing students and continue
                        continue;
                    }

                    // Generate random password for student
                    var studentPassword = _passwordService.GenerateRandomPassword();
                    var studentVerificationToken = _passwordService.GenerateVerificationToken();
                    
                    var student = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = studentDto.Email,
                        PasswordHash = _passwordService.HashPassword(studentPassword),
                        Role = UserRole.Student,
                        ApprovalStatus = ApprovalStatus.Approved, // Auto-approved by school
                        FirstName = studentDto.FirstName,
                        LastName = studentDto.LastName,
                        DateOfBirth = studentDto.DateOfBirth,
                        CurrentClass = studentDto.CurrentClass,
                        School = studentDto.School,
                        IsEmailVerified = false,
                        EmailVerificationToken = studentVerificationToken,
                        EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                        CreateAt = DateTime.UtcNow
                    };

                    _context.Users.Add(student);
                    
                    // Send verification email to student with their generated password
                    await _notificationService.SendEmailVerification(studentDto.Email, studentVerificationToken, "student", studentPassword);
                }
                
                await _context.SaveChangesAsync();
            }

            // Send verification email to school
            if (schoolUser.EmailVerificationToken.HasValue)
            {
                await _notificationService.SendEmailVerification(schoolUser.Email, schoolUser.EmailVerificationToken.Value, "email");
            }

            return schoolUser;
        }

        private async Task<User> CreateFamilyFromSubscriptionAsync(string registrationData, Guid planId)
        {
            var dto = JsonSerializer.Deserialize<FamilyRegistrationDTO>(registrationData);
            
            var existingMainUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existingMainUser != null)
                return existingMainUser;
            
            // Create the main family user (parent/guardian)
            var currentTermsVersion = _configuration["AppSettings:CurrentTermsVersion"] ?? "2025-06-01";
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = UserRole.Family,
                ApprovalStatus = ApprovalStatus.Approved,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid(),
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                CreateAt = DateTime.UtcNow,
                TermsAcceptedAt = DateTime.UtcNow,
                TermsVersion = currentTermsVersion
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Create family members and collect their passwords
            var familyMemberCredentials = new List<(string Name, string Email, string Password)>();
            
            if (dto.FamilyMembers != null && dto.FamilyMembers.Any())
            {
                foreach (var familyMember in dto.FamilyMembers)
                {
                    // Generate a unique email for each family member using the new format
                    var familyMemberEmail = await GenerateUniqueFamilyMemberEmail(familyMember.FirstName, familyMember.LastName);
                    
                    // Validate email length (max 256 characters)
                    if (familyMemberEmail.Length > 256)
                    {
                        throw new InvalidOperationException($"Generated email for {familyMember.FirstName} {familyMember.LastName} is too long: {familyMemberEmail}");
                    }
                    
                    // Validate CurrentClass is a valid GUID
                    if (!string.IsNullOrEmpty(familyMember.CurrentClass) && !Guid.TryParse(familyMember.CurrentClass, out _))
                    {
                        throw new InvalidOperationException($"Invalid CurrentClass format for {familyMember.FirstName} {familyMember.LastName}: {familyMember.CurrentClass}");
                    }
                    
                    // Generate a random password for family member
                    var familyMemberPassword = _passwordService.GenerateRandomPassword();
                    var familyMemberVerificationToken = _passwordService.GenerateVerificationToken();
                    
                    var familyMemberUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = familyMemberEmail,
                        PasswordHash = _passwordService.HashPassword(familyMemberPassword),
                        Role = UserRole.Student,
                        ApprovalStatus = ApprovalStatus.Approved,
                        FirstName = familyMember.FirstName,
                        LastName = familyMember.LastName,
                        CurrentClass = familyMember.CurrentClass,
                        ParentUserId = user.Id, // Link to main family user
                        IsEmailVerified = false,
                        EmailVerificationToken = familyMemberVerificationToken,
                        EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                        CreateAt = DateTime.UtcNow
                    };

                    _context.Users.Add(familyMemberUser);
                    
                    // Store family member credentials to send to main user
                    familyMemberCredentials.Add(($"{familyMember.FirstName} {familyMember.LastName}", familyMemberEmail, familyMemberPassword));
                }
                
                await _context.SaveChangesAsync();
            }

            // Send verification email to main family user using the family-specific method
            if (user.EmailVerificationToken.HasValue)
            {
                var familyMemberNames = dto.FamilyMembers?.Select(fm => $"{fm.FirstName} {fm.LastName}").ToList() ?? new List<string>();
                await _notificationService.SendFamilyEmailVerification(user.Email, user.EmailVerificationToken.Value, familyMemberNames, "family", familyMemberCredentials);
            }

            return user;
        }

        private async Task<User> CreateSupervisorFromSubscriptionAsync(string registrationData, Guid planId)
        {
            var dto = JsonSerializer.Deserialize<SupervisorRegistrationDTO>(registrationData);
            
            // Get the approved supervisor application
            var supervisorApplication = await _context.SupervisorApplications
                .FirstOrDefaultAsync(sa => sa.Id == dto.SupervisorApplicationId && sa.ApprovalStatus == ApprovalStatus.Approved);

            if (supervisorApplication == null)
            {
                throw new ArgumentException("Approved supervisor application not found");
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == supervisorApplication.ContactPersonEmail);
            if (existingUser != null)
                return existingUser;

            // Create the supervisor user
            var tempPassword = supervisorApplication.TemporaryPassword ?? Guid.NewGuid().ToString("N")[..12];
            var tempPasswordHash = _passwordService.HashPassword(tempPassword);

            var supervisor = new User
            {
                Id = Guid.NewGuid(),
                Email = supervisorApplication.ContactPersonEmail,
                FirstName = supervisorApplication.ContactPersonFirstName,
                LastName = supervisorApplication.ContactPersonLastName,
                School = supervisorApplication.SchoolName,
                City = supervisorApplication.City,
                PhoneNumber = supervisorApplication.ContactPersonPhone,
                Role = UserRole.Supervisor,
                ApprovalStatus = ApprovalStatus.Approved,
                IsEmailVerified = true,
                PasswordHash = tempPasswordHash,
                CreateAt = DateTime.UtcNow
            };

            _context.Users.Add(supervisor);
            await _context.SaveChangesAsync();

            // Update the application with the created user ID
            supervisorApplication.ApprovedUserId = supervisor.Id;
            supervisorApplication.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            // Send credentials email
            await _notificationService.SendSupervisorCredentialsEmail(
                supervisor.Email,
                $"{supervisor.FirstName} {supervisor.LastName}",
                tempPassword);

            _logger.LogInformation("Created supervisor user {UserId} from approved application {ApplicationId}", 
                supervisor.Id, supervisorApplication.Id);

            return supervisor;
        }

        private async Task<string> GenerateUniqueFamilyMemberEmail(string firstName, string lastName)
        {
            var baseEmail = $"{firstName.ToLower()}.{lastName.ToLower()}@bga.al";
            
            // Check if the base email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == baseEmail.ToLower());
            if (existingUser == null)
            {
                return baseEmail;
            }
            
            // If it exists, try with numbers
            for (int i = 1; i <= 999; i++)
            {
                var numberedEmail = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@bga.al";
                var existingNumberedUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == numberedEmail.ToLower());
                if (existingNumberedUser == null)
                {
                    return numberedEmail;
                }
            }
            
            throw new InvalidOperationException($"Unable to generate unique email for {firstName} {lastName} after trying 999 variations");
        }

        private SubscriptionStatus MapStripeStatus(string stripeStatus)
        {
            return stripeStatus switch
            {
                "incomplete" => SubscriptionStatus.Incomplete,
                "incomplete_expired" => SubscriptionStatus.IncompleteExpired,
                "trialing" => SubscriptionStatus.Trialing,
                "active" => SubscriptionStatus.Active,
                "past_due" => SubscriptionStatus.PastDue,
                "canceled" => SubscriptionStatus.Canceled,
                "unpaid" => SubscriptionStatus.Unpaid,
                "paused" => SubscriptionStatus.Paused,
                _ => SubscriptionStatus.Incomplete
            };
        }

        private BillingInterval MapStripeInterval(string stripeInterval)
        {
            return stripeInterval switch
            {
                "day" => BillingInterval.Day,
                "week" => BillingInterval.Week,
                "month" => BillingInterval.Month,
                "year" => BillingInterval.Year,
                _ => BillingInterval.Month
            };
        }

        private async Task HandleCheckoutSessionCompletedAsync(Event stripeEvent)
        {
            var session = stripeEvent.Data.Object as Session;

            if (session == null)
            {
                _logger.LogWarning("Stripe session object is null in webhook.");
                return;
            }

            // Only handle subscription checkout sessions
            if (session.Mode != "subscription")
            {
                _logger.LogInformation("Non-subscription checkout session completed: {SessionId}", session.Id);
                return;
            }

            _logger.LogInformation("Subscription checkout session completed: {SessionId}", session.Id);

            // Update the payment status if it exists
            var payment = await _context.Payments.FirstOrDefaultAsync(p => p.StripeSessionId == session.Id);
            if (payment != null)
            {
                payment.Status = "succeeded";
                // For subscription checkout sessions, PaymentIntentId is null on the session —
                // the actual payment intent is on the invoice (recorded via invoice.payment_succeeded).
                if (!string.IsNullOrEmpty(session.PaymentIntentId))
                    payment.StripePaymentIntentId = session.PaymentIntentId;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated payment status to succeeded for session: {SessionId}", session.Id);
            }
            else
            {
                _logger.LogWarning("No matching payment found for subscription session: {SessionId}", session.Id);
            }
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Webhook handlers for Novalnet and Paddle
        // ──────────────────────────────────────────────────────────────────────────

        public async Task HandleNovalnetWebhookAsync(HttpRequest request)
        {
            var body = await new StreamReader(request.Body).ReadToEndAsync();

            if (!_novalnetProvider.VerifyWebhookSignature(request, body))
                throw new UnauthorizedAccessException("Invalid Novalnet webhook signature");

            using var doc = System.Text.Json.JsonDocument.Parse(body);
            var root = doc.RootElement;

            var eventType = root.GetProperty("event").GetProperty("type").GetString();
            var txnStatus = root.GetProperty("transaction").GetProperty("status").GetString();
            var txnId = root.GetProperty("transaction").GetProperty("tid").GetString();

            _logger.LogInformation("Novalnet webhook: EventType={EventType}, Status={Status}, TxnId={TxnId}",
                eventType, txnStatus, txnId);

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == txnId);

            if (subscription == null)
            {
                _logger.LogWarning("No subscription found for Novalnet TxnId: {TxnId}", txnId);
                return;
            }

            switch (eventType)
            {
                case "PAYMENT":
                    if (txnStatus == "CONFIRMED")
                    {
                        subscription.Status = SubscriptionStatus.Active;
                        subscription.UpdatedAt = DateTime.UtcNow;

                        var amountCents = root.GetProperty("transaction").GetProperty("amount").GetInt64();
                        _context.SubscriptionPayments.Add(new SubscriptionPayment
                        {
                            Id = Guid.NewGuid(),
                            SubscriptionId = subscription.Id,
                            Amount = amountCents,
                            Currency = root.GetProperty("transaction").GetProperty("currency").GetString() ?? "EUR",
                            Status = PaymentStatus.Succeeded,
                            PaidAt = DateTime.UtcNow,
                            PeriodStart = DateTime.UtcNow,
                            PeriodEnd = subscription.Interval == BillingInterval.Year
                                ? DateTime.UtcNow.AddYears(1)
                                : DateTime.UtcNow.AddMonths(1),
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                    break;

                case "SUBSCRIPTION_SUSPEND":
                    subscription.Status = SubscriptionStatus.Paused;
                    subscription.UpdatedAt = DateTime.UtcNow;
                    break;

                case "SUBSCRIPTION_REACTIVATE":
                    subscription.Status = SubscriptionStatus.Active;
                    subscription.UpdatedAt = DateTime.UtcNow;
                    break;

                case "SUBSCRIPTION_CANCEL":
                    subscription.Status = SubscriptionStatus.Canceled;
                    subscription.CanceledAt = DateTime.UtcNow;
                    subscription.UpdatedAt = DateTime.UtcNow;
                    break;
            }

            await _context.SaveChangesAsync();
        }

        public async Task HandlePaddleWebhookAsync(HttpRequest request)
        {
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            var paddleSignature = request.Headers["Paddle-Signature"].ToString();

            if (!_paddleProvider.VerifyWebhookSignature(body, paddleSignature))
                throw new UnauthorizedAccessException("Invalid Paddle webhook signature");

            using var doc = System.Text.Json.JsonDocument.Parse(body);
            var root = doc.RootElement;

            var eventType = root.GetProperty("event_type").GetString();
            _logger.LogInformation("Paddle webhook: EventType={EventType}", eventType);

            switch (eventType)
            {
                case "subscription.activated":
                case "subscription.updated":
                    await HandlePaddleSubscriptionActivatedAsync(root);
                    break;

                case "subscription.canceled":
                    await HandlePaddleSubscriptionCanceledAsync(root);
                    break;

                case "subscription.paused":
                    await HandlePaddleSubscriptionPausedAsync(root);
                    break;

                case "subscription.resumed":
                    await HandlePaddleSubscriptionResumedAsync(root);
                    break;

                case "transaction.completed":
                    await HandlePaddleTransactionCompletedAsync(root);
                    break;
            }
        }

        private async Task HandlePaddleSubscriptionActivatedAsync(System.Text.Json.JsonElement root)
        {
            var data = root.GetProperty("data");
            var paddleSubId = data.GetProperty("id").GetString();

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paddleSubId);

            if (subscription == null)
            {
                // First activation — create user and subscription record from custom_data
                if (!data.TryGetProperty("custom_data", out var customData) || customData.ValueKind == System.Text.Json.JsonValueKind.Null)
                {
                    _logger.LogWarning("Paddle subscription.activated: no custom_data for subscription {SubId}", paddleSubId);
                    return;
                }

                var registrationType = customData.TryGetProperty("registration_type", out var rt) ? rt.GetString() : null;
                var registrationData = customData.TryGetProperty("registration_data", out var rd) ? rd.GetString() : null;
                var planIdString = customData.TryGetProperty("subscription_package_id", out var pi) ? pi.GetString() : null;
                var billingIntervalStr = customData.TryGetProperty("billing_interval", out var bi) ? bi.GetString() : null;

                if (string.IsNullOrEmpty(registrationType) || string.IsNullOrEmpty(registrationData) || string.IsNullOrEmpty(planIdString))
                {
                    _logger.LogError("Paddle subscription.activated: missing custom_data fields for subscription {SubId}", paddleSubId);
                    return;
                }

                if (!Guid.TryParse(planIdString, out var planId))
                {
                    _logger.LogError("Paddle subscription.activated: invalid plan ID {PlanId}", planIdString);
                    return;
                }

                User user;
                try
                {
                    user = registrationType switch
                    {
                        "student" => await CreateStudentFromSubscriptionAsync(registrationData, planId),
                        "school" => await CreateSchoolFromSubscriptionAsync(registrationData, planId),
                        "family" => await CreateFamilyFromSubscriptionAsync(registrationData, planId),
                        _ => throw new ArgumentException($"Unknown registration type: {registrationType}")
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Paddle: failed to create user for subscription {SubId}", paddleSubId);
                    return;
                }

                var billingInterval = string.Equals(billingIntervalStr, "Year", StringComparison.OrdinalIgnoreCase)
                    ? BillingInterval.Year
                    : BillingInterval.Month;

                long subscriptionAmount = 0;
                var subscriptionCurrency = data.TryGetProperty("currency_code", out var subCur) ? subCur.GetString() ?? "eur" : "eur";
                if (data.TryGetProperty("items", out var subItems) && subItems.GetArrayLength() > 0)
                {
                    var firstItem = subItems[0];
                    if (firstItem.TryGetProperty("price", out var subPrice) &&
                        subPrice.TryGetProperty("unit_price", out var subUnitPrice) &&
                        subUnitPrice.TryGetProperty("amount", out var subAmountEl))
                    {
                        long.TryParse(subAmountEl.GetString(), out subscriptionAmount);
                    }
                }

                var newSubscription = new Domain.Entities.Subscription
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ExternalSubscriptionId = paddleSubId,
                    Provider = PaymentProvider.Paddle,
                    SubscriptionPackageId = planId,
                    Status = SubscriptionStatus.Active,
                    StartDate = DateTime.UtcNow,
                    Interval = billingInterval,
                    IntervalCount = 1,
                    Amount = subscriptionAmount,
                    Currency = subscriptionCurrency,
                    RegistrationType = registrationType,
                    RegistrationData = registrationData,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                if (data.TryGetProperty("current_billing_period", out var bp))
                {
                    if (DateTime.TryParse(bp.GetProperty("starts_at").GetString(), out var start))
                        newSubscription.CurrentPeriodStart = start;
                    if (DateTime.TryParse(bp.GetProperty("ends_at").GetString(), out var end))
                        newSubscription.CurrentPeriodEnd = end;
                }

                try
                {
                    _context.Subscriptions.Add(newSubscription);
                    user.ActiveSubscriptionId = newSubscription.Id;
                    user.SubscriptionExpiresAt = newSubscription.CurrentPeriodEnd;

                    var pendingPayment = await _context.Payments
                        .FirstOrDefaultAsync(p => p.Provider == PaymentProvider.Paddle
                                               && p.Email.ToLower() == user.Email.ToLower()
                                               && p.Status == "pending");
                    if (pendingPayment != null)
                        pendingPayment.Status = "succeeded";

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Paddle: created user {UserId} and subscription {SubId}", user.Id, paddleSubId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Paddle: failed to save subscription for user {UserId}, sub {SubId}", user.Id, paddleSubId);
                    return;
                }

                // Save payment record separately so a parsing failure never blocks subscription creation
                try
                {
                    long amountCents = 0;
                    var currency = data.TryGetProperty("currency_code", out var cur) ? cur.GetString() ?? "EUR" : "EUR";
                    if (data.TryGetProperty("items", out var items) && items.GetArrayLength() > 0)
                    {
                        var firstItem = items[0];
                        if (firstItem.TryGetProperty("price", out var price) &&
                            price.TryGetProperty("unit_price", out var unitPrice) &&
                            unitPrice.TryGetProperty("amount", out var amountEl))
                        {
                            long.TryParse(amountEl.GetString(), out amountCents);
                        }
                    }

                    _context.SubscriptionPayments.Add(new SubscriptionPayment
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionId = newSubscription.Id,
                        Amount = amountCents,
                        Currency = currency,
                        Status = PaymentStatus.Succeeded,
                        PaidAt = DateTime.UtcNow,
                        PeriodStart = newSubscription.CurrentPeriodStart ?? DateTime.UtcNow,
                        PeriodEnd = newSubscription.CurrentPeriodEnd ?? DateTime.UtcNow.AddMonths(1),
                        CreatedAt = DateTime.UtcNow
                    });
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Paddle: failed to save payment record for subscription {SubId} — subscription itself was saved", newSubscription.Id);
                }
                return;
            }

            subscription.Status = SubscriptionStatus.Active;
            subscription.UpdatedAt = DateTime.UtcNow;

            if (data.TryGetProperty("current_billing_period", out var period))
            {
                if (DateTime.TryParse(period.GetProperty("starts_at").GetString(), out var start))
                    subscription.CurrentPeriodStart = start;
                if (DateTime.TryParse(period.GetProperty("ends_at").GetString(), out var end))
                    subscription.CurrentPeriodEnd = end;
            }

            await _context.SaveChangesAsync();
        }

        private async Task HandlePaddleSubscriptionCanceledAsync(System.Text.Json.JsonElement root)
        {
            var paddleSubId = root.GetProperty("data").GetProperty("id").GetString();
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paddleSubId);

            if (subscription == null) return;

            subscription.Status = SubscriptionStatus.Canceled;
            subscription.CanceledAt = DateTime.UtcNow;
            subscription.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task HandlePaddleSubscriptionPausedAsync(System.Text.Json.JsonElement root)
        {
            var paddleSubId = root.GetProperty("data").GetProperty("id").GetString();
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paddleSubId);

            if (subscription == null) return;

            subscription.Status = SubscriptionStatus.Paused;
            subscription.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task HandlePaddleSubscriptionResumedAsync(System.Text.Json.JsonElement root)
        {
            var paddleSubId = root.GetProperty("data").GetProperty("id").GetString();
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paddleSubId);

            if (subscription == null) return;

            subscription.Status = SubscriptionStatus.Active;
            subscription.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        private async Task HandlePaddleTransactionCompletedAsync(System.Text.Json.JsonElement root)
        {
            var data = root.GetProperty("data");
            var paddleSubId = data.TryGetProperty("subscription_id", out var subIdEl) ? subIdEl.GetString() : null;

            if (string.IsNullOrEmpty(paddleSubId)) return;

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paddleSubId);

            if (subscription == null)
            {
                _logger.LogWarning("Paddle transaction.completed: subscription not found for {PaddleSubId} — likely race condition, payment will be recorded via subscription.activated", paddleSubId);
                return;
            }

            var detailsEl = data.GetProperty("details");
            var totalsEl = detailsEl.GetProperty("totals");
            var amountCents = long.TryParse(totalsEl.GetProperty("grand_total").GetString(), out var total) ? total : 0;
            var currency = data.GetProperty("currency_code").GetString() ?? "EUR";

            _context.SubscriptionPayments.Add(new SubscriptionPayment
            {
                Id = Guid.NewGuid(),
                SubscriptionId = subscription.Id,
                Amount = amountCents,
                Currency = currency,
                Status = PaymentStatus.Succeeded,
                PaidAt = DateTime.UtcNow,
                PeriodStart = subscription.CurrentPeriodStart ?? DateTime.UtcNow,
                PeriodEnd = subscription.CurrentPeriodEnd ?? DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        // ──────────────────────────────────────────────────────────────────────────
        // Manual payment (BKT / Raiffeisen)
        // ──────────────────────────────────────────────────────────────────────────

        public async Task<bool> ConfirmManualPaymentAsync(string paymentReference, Guid confirmedByAdminId)
        {
            try
            {
                var payment = await _context.Payments
                    .Include(p => p.SubscriptionPackage)
                    .FirstOrDefaultAsync(p => p.ExternalSessionId == paymentReference &&
                                             (p.Provider == PaymentProvider.BKT || p.Provider == PaymentProvider.Raiffeisen));

                if (payment == null)
                {
                    _logger.LogWarning("Manual payment not found for reference: {Reference}", paymentReference);
                    return false;
                }

                if (payment.Status == "succeeded")
                {
                    _logger.LogWarning("Manual payment already confirmed: {Reference}", paymentReference);
                    return false;
                }

                payment.Status = "succeeded";

                // Find associated subscription (created on checkout initiation) or create one
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.ExternalSubscriptionId == paymentReference);

                if (subscription != null)
                {
                    subscription.Status = SubscriptionStatus.Active;
                    subscription.StartDate = DateTime.UtcNow;
                    subscription.CurrentPeriodStart = DateTime.UtcNow;
                    subscription.CurrentPeriodEnd = subscription.Interval == BillingInterval.Year
                        ? DateTime.UtcNow.AddYears(1)
                        : DateTime.UtcNow.AddMonths(1);
                    subscription.UpdatedAt = DateTime.UtcNow;

                    var user = await _context.Users.FindAsync(subscription.UserId);
                    if (user != null)
                    {
                        user.ActiveSubscriptionId = subscription.Id;
                        user.SubscriptionExpiresAt = subscription.CurrentPeriodEnd;
                    }
                }

                await _context.SaveChangesAsync();

                // Send confirmation email
                var packageName = payment.SubscriptionPackage?.Name ?? "abbonamento";
                await _emailService.SendManualPaymentConfirmedAsync(payment.Email, packageName);

                _logger.LogInformation("Manual payment confirmed: {Reference} by admin {AdminId}", paymentReference, confirmedByAdminId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming manual payment: {Reference}", paymentReference);
                return false;
            }
        }

        public async Task SendManualPaymentReminderAsync(string paymentReference)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.ExternalSessionId == paymentReference &&
                                         (p.Provider == PaymentProvider.BKT || p.Provider == PaymentProvider.Raiffeisen));

            if (payment == null)
            {
                _logger.LogWarning("Manual payment not found for reminder: {Reference}", paymentReference);
                return;
            }

            var package = await _context.SubscriptionPackages.FindAsync(payment.SubscriptionPackageId);
            var amountCents = package?.MonthlyPrice ?? 0;

            await _emailService.SendManualPaymentReminderAsync(payment.Email, paymentReference, amountCents, "ALL");

            _logger.LogInformation("Manual payment reminder sent for reference: {Reference}", paymentReference);
        }

        // ──────────────────────────────────────────────────────────────────────────

        private SubscriptionResponseDTO MapToResponseDTO(Domain.Entities.Subscription subscription)
        {
            return new SubscriptionResponseDTO
            {
                Id = subscription.Id,
                UserId = subscription.UserId,
                StripeSubscriptionId = subscription.StripeSubscriptionId,
                StripeCustomerId = subscription.StripeCustomerId,
                PlanId = subscription.SubscriptionPackageId,
                PlanName = subscription.SubscriptionPackage?.Name ?? "",
                Status = subscription.Status,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                CurrentPeriodStart = subscription.CurrentPeriodStart,
                CurrentPeriodEnd = subscription.CurrentPeriodEnd,
                TrialEnd = subscription.TrialEnd,
                Amount = subscription.Amount,
                Currency = subscription.Currency,
                Interval = subscription.Interval,
                IntervalCount = subscription.IntervalCount,
                RegistrationType = subscription.RegistrationType,
                CreatedAt = subscription.CreatedAt,
                UpdatedAt = subscription.UpdatedAt
            };
        }
    }
}
