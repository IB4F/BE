using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

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

        /// <summary>
        /// Create a new subscription
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateSubscription([FromBody] SubscriptionRequestDTO dto)
        {
            try
            {
                var sessionId = await _subscriptionService.CreateSubscriptionAsync(dto);
                
                return Ok(new
                {
                    message = "Subscription initiated. Please complete payment to start your subscription.",
                    sessionId = sessionId,
                    paymentUrl = $"https://checkout.stripe.com/pay/{sessionId}"
                });
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
        /// Create a subscription for an approved supervisor application
        /// </summary>
        [HttpPost("create-supervisor")]
        public async Task<IActionResult> CreateSupervisorSubscription([FromBody] SupervisorSubscriptionRequestDTO dto)
        {
            try
            {
                var sessionId = await _subscriptionService.CreateSupervisorSubscriptionAsync(dto);
                
                return Ok(new
                {
                    message = "Subscription initiated. Please complete payment to start your subscription.",
                    sessionId = sessionId,
                    paymentUrl = $"https://checkout.stripe.com/pay/{sessionId}"
                });
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
                var success = await _subscriptionService.CancelSubscriptionAsync(id, dto);
                if (success)
                {
                    return Ok(new { message = "Subscription canceled successfully." });
                }
                return BadRequest(new { message = "Failed to cancel subscription." });
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
                var success = await _subscriptionService.PauseSubscriptionAsync(id);
                if (success)
                {
                    return Ok(new { message = "Subscription paused successfully." });
                }
                return BadRequest(new { message = "Failed to pause subscription." });
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
                var success = await _subscriptionService.ResumeSubscriptionAsync(id);
                if (success)
                {
                    return Ok(new { message = "Subscription resumed successfully." });
                }
                return BadRequest(new { message = "Failed to resume subscription." });
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
                var success = await _subscriptionService.UpdateSubscriptionPlanAsync(id, dto);
                if (success)
                {
                    return Ok(new { message = "Subscription plan updated successfully." });
                }
                return BadRequest(new { message = "Failed to update subscription plan." });
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling Stripe webhook");
                return StatusCode(500, new { message = "Webhook processing failed." });
            }
        }
    }
}
