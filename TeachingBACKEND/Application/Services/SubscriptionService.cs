using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Text.Json;
using TeachingBACKEND.Application.Interfaces;
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
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            ApplicationDbContext context,
            IConfiguration configuration,
            IPasswordService passwordService,
            INotificationService notificationService,
            ILogger<SubscriptionService> logger)
        {
            _context = context;
            _configuration = configuration;
            _passwordService = passwordService;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<string> CreateSubscriptionAsync(SubscriptionRequestDTO dto)
        {
            using var activity = _logger.BeginScope("CreateSubscriptionAsync");
            
            try
            {
                _logger.LogInformation("Starting subscription creation for email: {Email}, RegistrationType: {RegistrationType}, PackageId: {PackageId}", 
                    dto.Email, dto.RegistrationType, dto.SubscriptionPackageId);

                // Get the subscription package
                _logger.LogInformation("Looking up subscription package with ID: {PackageId}", dto.SubscriptionPackageId);
                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == dto.SubscriptionPackageId);

                if (package == null)
                {
                    _logger.LogError("Subscription package not found with ID: {PackageId}", dto.SubscriptionPackageId);
                    throw new ArgumentException("Subscription package not found");
                }

                _logger.LogInformation("Found subscription package: {PackageName}, UserType: {UserType}, TrialDays: {TrialDays}", 
                    package.Name, package.UserType, package.TrialDays);

                // Determine the Stripe Price ID based on billing interval
                string stripePriceId = dto.BillingInterval switch
                {
                    BillingInterval.Month => package.StripeMonthlyPriceId,
                    BillingInterval.Year => package.StripeYearlyPriceId,
                    _ => package.StripeMonthlyPriceId
                };

                _logger.LogInformation("Using Stripe Price ID: {StripePriceId} for billing interval: {BillingInterval}", 
                    stripePriceId, dto.BillingInterval);

                // Create Stripe checkout session for subscription
                _logger.LogInformation("Configuring Stripe API key");
                StripeConfiguration.ApiKey = _configuration["STRIPE_SECRET_KEY"];

                var subscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "registration_type", dto.RegistrationType },
                        { "registration_data", dto.RegistrationData },
                        { "subscription_package_id", dto.SubscriptionPackageId.ToString() },
                        { "billing_interval", dto.BillingInterval.ToString() }
                    }
                };

                // Only set trial period if TrialDays > 0
                if (package.TrialDays > 0)
                {
                    subscriptionData.TrialPeriodDays = package.TrialDays;
                    _logger.LogInformation("Setting trial period to {TrialDays} days", package.TrialDays);
                }

                _logger.LogInformation("Creating Stripe checkout session with SuccessUrl: {SuccessUrl}, CancelUrl: {CancelUrl}", 
                    _configuration["STRIPE_SUCCESS_URL"], _configuration["STRIPE_CANCEL_URL"]);

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    Mode = "subscription",
                    SuccessUrl = _configuration["STRIPE_SUCCESS_URL"],
                    CancelUrl = _configuration["STRIPE_CANCEL_URL"],
                    CustomerEmail = dto.Email,
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new()
                        {
                            Price = stripePriceId,
                            Quantity = 1
                        }
                    },
                    SubscriptionData = subscriptionData
                };

                _logger.LogInformation("Calling Stripe SessionService.CreateAsync");
                var service = new SessionService();
                var session = await service.CreateAsync(options);

                _logger.LogInformation("Stripe session created successfully with ID: {SessionId}", session.Id);

                // Create Payment record for tracking
                _logger.LogInformation("Creating Payment record for tracking");
                var payment = new Payment
                {
                    Email = dto.Email,
                    RegistrationType = dto.RegistrationType,
                    RegistrationData = dto.RegistrationData,
                    StripeSessionId = session.Id,
                    Amount = 0, // Will be set by subscription
                    Currency = "eur",
                    Status = "pending",
                    SubscriptionPackageId = dto.SubscriptionPackageId
                };

                _logger.LogInformation("Adding Payment record to context and saving changes");
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully created Stripe subscription session: {SessionId} and Payment record", session.Id);

                return session.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription session for email: {Email}, RegistrationType: {RegistrationType}, PackageId: {PackageId}. " +
                    "Exception: {ExceptionMessage}. Inner Exception: {InnerExceptionMessage}. Stack Trace: {StackTrace}", 
                    dto.Email, dto.RegistrationType, dto.SubscriptionPackageId, ex.Message, 
                    ex.InnerException?.Message ?? "None", ex.StackTrace);
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
                .Include(s => s.User)
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

                if (subscription == null)
                {
                    return false;
                }

                // Cancel in Stripe
                StripeConfiguration.ApiKey = _configuration["STRIPE_SECRET_KEY"];
                var stripeService = new Stripe.SubscriptionService();
                
                var cancelOptions = new SubscriptionCancelOptions
                {
                    Prorate = !dto.Immediately,
                    InvoiceNow = dto.Immediately
                };

                await stripeService.CancelAsync(subscription.StripeSubscriptionId, cancelOptions);

                // Update local subscription
                subscription.Status = SubscriptionStatus.Canceled;
                subscription.CanceledAt = DateTime.UtcNow;
                subscription.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Canceled subscription: {SubscriptionId}", subscriptionId);
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

                if (subscription == null)
                {
                    return false;
                }

                // Pause in Stripe
                StripeConfiguration.ApiKey = _configuration["STRIPE_SECRET_KEY"];
                var stripeService = new Stripe.SubscriptionService();
                
                var updateOptions = new SubscriptionUpdateOptions
                {
                    PauseCollection = new SubscriptionPauseCollectionOptions
                    {
                        Behavior = "void"
                    }
                };

                await stripeService.UpdateAsync(subscription.StripeSubscriptionId, updateOptions);

                // Update local subscription
                subscription.Status = SubscriptionStatus.Paused;
                subscription.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Paused subscription: {SubscriptionId}", subscriptionId);
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

                if (subscription == null)
                {
                    return false;
                }

                // Resume in Stripe
                StripeConfiguration.ApiKey = _configuration["STRIPE_SECRET_KEY"];
                var stripeService = new Stripe.SubscriptionService();
                
                var updateOptions = new SubscriptionUpdateOptions
                {
                    PauseCollection = null
                };

                await stripeService.UpdateAsync(subscription.StripeSubscriptionId, updateOptions);

                // Update local subscription
                subscription.Status = SubscriptionStatus.Active;
                subscription.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Resumed subscription: {SubscriptionId}", subscriptionId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resuming subscription: {SubscriptionId}", subscriptionId);
                return false;
            }
        }

        public async Task<bool> UpdateSubscriptionPlanAsync(Guid subscriptionId, ChangePlanDTO dto)
        {
            try
            {
                var subscription = await _context.Subscriptions
                    .Include(s => s.SubscriptionPackage)
                    .FirstOrDefaultAsync(s => s.Id == subscriptionId);

                if (subscription == null)
                {
                    return false;
                }

                var newPackage = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == dto.NewPlanId);

                if (newPackage == null)
                {
                    return false;
                }

                // Determine the new Stripe Price ID
                string newStripePriceId = dto.BillingInterval switch
                {
                    BillingInterval.Month => newPackage.StripeMonthlyPriceId,
                    BillingInterval.Year => newPackage.StripeYearlyPriceId,
                    _ => newPackage.StripeMonthlyPriceId
                };

                // Update in Stripe
                StripeConfiguration.ApiKey = _configuration["STRIPE_SECRET_KEY"];
                var stripeService = new Stripe.SubscriptionService();
                
                var updateOptions = new SubscriptionUpdateOptions
                {
                    Items = new List<SubscriptionItemOptions>
                    {
                        new()
                        {
                            Id = subscription.StripeSubscriptionId,
                            Price = newStripePriceId
                        }
                    },
                    ProrationBehavior = dto.Prorate ? "create_prorations" : "none"
                };

                await stripeService.UpdateAsync(subscription.StripeSubscriptionId, updateOptions);

                // Update local subscription
                subscription.SubscriptionPackageId = dto.NewPlanId;
                subscription.StripePriceId = newStripePriceId;
                subscription.Interval = dto.BillingInterval;
                subscription.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Updated subscription plan: {SubscriptionId}", subscriptionId);
                return true;
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
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

            return subscription != null && subscription.CurrentPeriodEnd > DateTime.UtcNow;
        }

        public async Task<DateTime?> GetUserSubscriptionExpiryAsync(Guid userId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.Status == SubscriptionStatus.Active);

            return subscription?.CurrentPeriodEnd;
        }

        public async Task<List<SubscriptionResponseDTO>> GetUserSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .Include(s => s.User)
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            return subscriptions.Select(MapToResponseDTO).ToList();
        }

        private async Task HandleSubscriptionCreatedAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Stripe.Subscription;
            if (subscription == null) return;

            try
            {
                _logger.LogInformation("Processing subscription created webhook for subscription: {SubscriptionId}", subscription.Id);
                
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
                user.SubscriptionExpiresAt = subscription.EndedAt;

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
                    subscriptionEntity.Status = MapStripeStatus(subscription.Status);
                    subscriptionEntity.EndDate = subscription.EndedAt;
                    subscriptionEntity.CurrentPeriodStart = subscription.Items.Data[0].CurrentPeriodStart;
                    subscriptionEntity.CurrentPeriodEnd = subscription.Items.Data[0].CurrentPeriodEnd;
                    subscriptionEntity.TrialEnd = subscription.TrialEnd;
                    subscriptionEntity.UpdatedAt = DateTime.UtcNow;

                    // Update user's subscription expiry
                    var user = await _context.Users.FindAsync(subscriptionEntity.UserId);
                    if (user != null)
                    {
                        user.SubscriptionExpiresAt = subscription.StartDate;
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
                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == invoice.Parent.SubscriptionDetails.SubscriptionId);

                if (subscriptionEntity != null)
                {
                    // Create subscription payment record
                    var payment = new SubscriptionPayment
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionId = subscriptionEntity.Id,
                        StripePaymentIntentId = invoice.Payments.Data.FirstOrDefault(p => p.Payment.PaymentIntentId != null).Payment.PaymentIntentId,
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
                var subscriptionEntity = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.StripeSubscriptionId == invoice.Parent.SubscriptionDetails.SubscriptionId);

                if (subscriptionEntity != null)
                {
                    // Create subscription payment record
                    var payment = new SubscriptionPayment
                    {
                        Id = Guid.NewGuid(),
                        SubscriptionId = subscriptionEntity.Id,
                        StripePaymentIntentId = invoice.Payments.Data.FirstOrDefault(p => p.Payment.PaymentIntentId != null).Payment.PaymentIntentId,
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

                    _logger.LogInformation("Recorded failed payment for subscription: {SubscriptionId}", subscriptionEntity.Id);
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
                CreateAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Send verification email
            if (user.EmailVerificationToken.HasValue)
            {
                await _notificationService.SendEmailVerification(user.Email, user.EmailVerificationToken.Value, "email");
            }

            return user;
        }

        private async Task<User> CreateSchoolFromSubscriptionAsync(string registrationData, Guid planId)
        {
            var dto = JsonSerializer.Deserialize<SchoolRegistrationDTO>(registrationData);
            
            // Generate a temporary password for school users
            var tempPassword = Guid.NewGuid().ToString("N")[..8];
            
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
                CreateAt = DateTime.UtcNow
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
            
            // Check if main user email already exists
            var existingMainUser = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existingMainUser != null)
            {
                throw new InvalidOperationException($"User with email {dto.Email} already exists");
            }
            
            // Create the main family user (parent/guardian)
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                Role = UserRole.Student, // Main family user is also a student
                ApprovalStatus = ApprovalStatus.Approved,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                IsEmailVerified = false,
                EmailVerificationToken = Guid.NewGuid(),
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddDays(7),
                CreateAt = DateTime.UtcNow
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
                payment.StripePaymentIntentId = session.PaymentIntentId;
                await _context.SaveChangesAsync();
                _logger.LogInformation("Updated payment status to succeeded for session: {SessionId}", session.Id);
            }
            else
            {
                _logger.LogWarning("No matching payment found for subscription session: {SessionId}", session.Id);
            }
        }

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
