using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(
            ISubscriptionService subscriptionService,
            ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        private (Guid LoggedUserId, bool IsAdmin) GetCallerInfo()
        {
            var loggedUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isAdmin = User.IsInRole("Admin");
            return (loggedUserId, isAdmin);
        }

        /// <summary>
        /// Create a new subscription. Set "provider" to: Stripe, Novalnet, Paddle, BKT, or Raiffeisen.
        /// </summary>
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionRequestDTO dto)
        {
            try
            {
                var result = await _subscriptionService.CreateSubscriptionAsync(dto);
                return Ok(BuildCheckoutResponse(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription");
                return StatusCode(500, new { message = "An error occurred while creating the subscription." });
            }
        }

        /// <summary>
        /// Create a subscription for an approved supervisor application.
        /// </summary>
        [Authorize]
        [HttpPost("create-supervisor")]
        public async Task<IActionResult> CreateSupervisorSubscription([FromBody] SupervisorSubscriptionRequestDTO dto)
        {
            try
            {
                var result = await _subscriptionService.CreateSupervisorSubscriptionAsync(dto);
                return Ok(BuildCheckoutResponse(result));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supervisor subscription");
                return StatusCode(500, new { message = "An error occurred while creating the subscription." });
            }
        }

        private static object BuildCheckoutResponse(PaymentSessionResult result)
        {
            if (result.IsManual)
            {
                return new
                {
                    message = "Subscription initiated. Please complete the bank transfer to activate your account.",
                    provider = result.Provider.ToString(),
                    isManual = true,
                    sessionId = result.SessionId,
                    manualDetails = result.ManualDetails
                };
            }

            return new
            {
                message = "Subscription initiated. Please complete payment to start your subscription.",
                provider = result.Provider.ToString(),
                isManual = false,
                sessionId = result.SessionId,
                checkoutUrl = result.CheckoutUrl
            };
        }

        /// <summary>
        /// Get subscription by ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionAsync(id);

                var (loggedUserId, isAdmin) = GetCallerInfo();
                if (subscription.UserId != loggedUserId && !isAdmin)
                    return Forbid();

                return Ok(subscription);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription: {SubscriptionId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the subscription." });
            }
        }

        /// <summary>
        /// Get user's active subscription
        /// </summary>
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserSubscription(Guid userId)
        {
            var (loggedUserId, isAdmin) = GetCallerInfo();
            if (userId != loggedUserId && !isAdmin)
                return Forbid();

            try
            {
                var subscription = await _subscriptionService.GetUserActiveSubscriptionAsync(userId);
                if (subscription == null)
                {
                    return NotFound(new { message = "No active subscription found for this user." });
                }
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user subscription: {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred while retrieving the user subscription." });
            }
        }

        /// <summary>
        /// Get all user's subscriptions
        /// </summary>
        [HttpGet("user/{userId}/all")]
        [Authorize]
        public async Task<IActionResult> GetUserSubscriptions(Guid userId)
        {
            var (loggedUserId, isAdmin) = GetCallerInfo();
            if (userId != loggedUserId && !isAdmin)
                return Forbid();

            try
            {
                var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
                return Ok(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user subscriptions: {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred while retrieving the user subscriptions." });
            }
        }

        /// <summary>
        /// Get user's payment/billing history
        /// </summary>
        [HttpGet("user/{userId}/payment-history")]
        [Authorize]
        public async Task<IActionResult> GetUserPaymentHistory(Guid userId)
        {
            var (loggedUserId, isAdmin) = GetCallerInfo();
            if (userId != loggedUserId && !isAdmin)
                return Forbid();

            try
            {
                var payments = await _subscriptionService.GetUserPaymentHistoryAsync(userId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user payment history: {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred while retrieving the payment history." });
            }
        }

        /// <summary>
        /// Cancel subscription
        /// </summary>
        [HttpPost("{id}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelSubscription(Guid id, [FromBody] CancelSubscriptionDTO dto)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionAsync(id);
                var (loggedUserId, isAdmin) = GetCallerInfo();
                if (subscription.UserId != loggedUserId && !isAdmin)
                    return Forbid();

                var success = await _subscriptionService.CancelSubscriptionAsync(id, dto);
                if (success)
                {
                    return Ok(new { message = "Subscription canceled successfully." });
                }
                return BadRequest(new { message = "Failed to cancel subscription." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error canceling subscription: {SubscriptionId}", id);
                return StatusCode(500, new { message = "An error occurred while canceling the subscription." });
            }
        }

        /// <summary>
        /// Pause subscription
        /// </summary>
        [HttpPost("{id}/pause")]
        [Authorize]
        public async Task<IActionResult> PauseSubscription(Guid id)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionAsync(id);
                var (loggedUserId, isAdmin) = GetCallerInfo();
                if (subscription.UserId != loggedUserId && !isAdmin)
                    return Forbid();

                var success = await _subscriptionService.PauseSubscriptionAsync(id);
                if (success)
                {
                    return Ok(new { message = "Subscription paused successfully." });
                }
                return BadRequest(new { message = "Failed to pause subscription." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error pausing subscription: {SubscriptionId}", id);
                return StatusCode(500, new { message = "An error occurred while pausing the subscription." });
            }
        }

        /// <summary>
        /// Resume subscription
        /// </summary>
        [HttpPost("{id}/resume")]
        [Authorize]
        public async Task<IActionResult> ResumeSubscription(Guid id)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionAsync(id);
                var (loggedUserId, isAdmin) = GetCallerInfo();
                if (subscription.UserId != loggedUserId && !isAdmin)
                    return Forbid();

                var success = await _subscriptionService.ResumeSubscriptionAsync(id);
                if (success)
                {
                    return Ok(new { message = "Subscription resumed successfully." });
                }
                return BadRequest(new { message = "Failed to resume subscription." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resuming subscription: {SubscriptionId}", id);
                return StatusCode(500, new { message = "An error occurred while resuming the subscription." });
            }
        }

        /// <summary>
        /// Change subscription plan
        /// </summary>
        [HttpPost("{id}/change-plan")]
        [Authorize]
        public async Task<IActionResult> ChangePlan(Guid id, [FromBody] ChangePlanDTO dto)
        {
            try
            {
                var subscription = await _subscriptionService.GetSubscriptionAsync(id);
                var (loggedUserId, isAdmin) = GetCallerInfo();
                if (subscription.UserId != loggedUserId && !isAdmin)
                    return Forbid();

                var success = await _subscriptionService.UpdateSubscriptionPlanAsync(id, dto);
                if (success)
                {
                    return Ok(new { message = "Subscription plan updated successfully." });
                }
                return BadRequest(new { message = "Failed to update subscription plan." });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing subscription plan: {SubscriptionId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the subscription plan." });
            }
        }

        /// <summary>
        /// Get user's active subscription with detailed information
        /// </summary>
        [HttpGet("user/{userId}/active")]
        [Authorize]
        public async Task<IActionResult> GetUserActiveSubscriptionDetails(Guid userId)
        {
            var (loggedUserId, isAdmin) = GetCallerInfo();
            if (userId != loggedUserId && !isAdmin)
                return Forbid();

            try
            {
                var subscription = await _subscriptionService.GetUserActiveSubscriptionAsync(userId);

                if (subscription == null)
                {
                    return Ok(new
                    {
                        isActive = false,
                        expiresAt = (DateTime?)null,
                        subscription = (object)null
                    });
                }

                var isActive = await _subscriptionService.IsUserSubscriptionActiveAsync(userId);

                return Ok(new
                {
                    isActive = isActive,
                    expiresAt = subscription.CurrentPeriodEnd,
                    subscription = new
                    {
                        id = subscription.Id,
                        planId = subscription.PlanId,
                        planName = subscription.PlanName,
                        status = subscription.Status.ToString(),
                        startDate = subscription.StartDate,
                        endDate = subscription.EndDate,
                        currentPeriodStart = subscription.CurrentPeriodStart,
                        currentPeriodEnd = subscription.CurrentPeriodEnd,
                        trialEnd = subscription.TrialEnd,
                        amount = subscription.Amount,
                        currency = subscription.Currency,
                        interval = subscription.Interval.ToString(),
                        intervalCount = subscription.IntervalCount,
                        registrationType = subscription.RegistrationType,
                        createdAt = subscription.CreatedAt,
                        updatedAt = subscription.UpdatedAt
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user active subscription details: {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred while retrieving subscription details." });
            }
        }

        /// <summary>
        /// Handle Stripe webhook events
        /// </summary>
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            try
            {
                await _subscriptionService.HandleStripeSubscriptionWebhookAsync(Request);
                return Ok();
            }
            catch (StripeException ex)
            {
                _logger.LogWarning(ex, "Invalid Stripe webhook signature or event");
                return BadRequest(new { message = "Invalid webhook." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Stripe webhook");
                return StatusCode(500, new { message = "Webhook processing failed." });
            }
        }

        /// <summary>
        /// Handle Novalnet webhook events
        /// </summary>
        [HttpPost("webhook/novalnet")]
        public async Task<IActionResult> HandleNovalnetWebhook()
        {
            try
            {
                await _subscriptionService.HandleNovalnetWebhookAsync(Request);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Invalid Novalnet webhook signature");
                return BadRequest(new { message = "Invalid webhook." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Novalnet webhook");
                return StatusCode(500, new { message = "Webhook processing failed." });
            }
        }

        /// <summary>
        /// Handle Paddle webhook events
        /// </summary>
        [HttpPost("webhook/paddle")]
        public async Task<IActionResult> HandlePaddleWebhook()
        {
            try
            {
                await _subscriptionService.HandlePaddleWebhookAsync(Request);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Invalid Paddle webhook signature");
                return BadRequest(new { message = "Invalid webhook." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Paddle webhook");
                return StatusCode(500, new { message = "Webhook processing failed." });
            }
        }

        /// <summary>
        /// Admin: confirm a manual bank transfer payment (BKT / Raiffeisen).
        /// </summary>
        [HttpPost("manual/confirm/{paymentReference}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmManualPayment(string paymentReference)
        {
            var (adminId, _) = GetCallerInfo();
            try
            {
                var success = await _subscriptionService.ConfirmManualPaymentAsync(paymentReference, adminId);
                if (success)
                    return Ok(new { message = "Manual payment confirmed. Subscription activated." });

                return BadRequest(new { message = "Payment reference not found or already confirmed." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming manual payment: {Reference}", paymentReference);
                return StatusCode(500, new { message = "An error occurred while confirming the payment." });
            }
        }

        /// <summary>
        /// Admin: send payment reminder for a pending manual bank transfer.
        /// </summary>
        [HttpPost("manual/remind/{paymentReference}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SendManualPaymentReminder(string paymentReference)
        {
            try
            {
                await _subscriptionService.SendManualPaymentReminderAsync(paymentReference);
                return Ok(new { message = "Reminder email sent." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending manual payment reminder: {Reference}", paymentReference);
                return StatusCode(500, new { message = "An error occurred while sending the reminder." });
            }
        }

        /// <summary>
        /// Check if user's subscription is active
        /// </summary>
        [HttpGet("is-active/{userId}")]
        [Authorize]
        public async Task<IActionResult> IsSubscriptionActive(Guid userId)
        {
            var (loggedUserId, isAdmin) = GetCallerInfo();
            if (userId != loggedUserId && !isAdmin)
                return Forbid();

            try
            {
                var isActive = await _subscriptionService.IsUserSubscriptionActiveAsync(userId);
                return Ok(new { isActive });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subscription status: {UserId}", userId);
                return StatusCode(500, new { message = "An error occurred." });
            }
        }
    }
}