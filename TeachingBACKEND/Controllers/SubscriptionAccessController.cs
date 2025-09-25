using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionAccessController : ControllerBase
    {
        private readonly ISubscriptionAccessService _subscriptionAccessService;

        public SubscriptionAccessController(ISubscriptionAccessService subscriptionAccessService)
        {
            _subscriptionAccessService = subscriptionAccessService;
        }

        /// <summary>
        /// Check if the current user can access a specific LearnHub
        /// </summary>
        /// <param name="learnHubId">The LearnHub ID to check access for</param>
        /// <returns>Access information including whether access is granted and upgrade requirements</returns>
        [HttpGet("check-access/{learnHubId}")]
        public async Task<IActionResult> CheckLearnHubAccess(Guid learnHubId)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized();

                var canAccess = await _subscriptionAccessService.CanUserAccessLearnHubAsync(userId.Value, learnHubId);
                var needsUpgrade = await _subscriptionAccessService.NeedsUpgradeForLearnHubAsync(userId.Value, learnHubId);
                var requiredTier = await _subscriptionAccessService.GetLearnHubRequiredTierAsync(learnHubId);
                var userTier = await _subscriptionAccessService.GetUserAccessTierAsync(userId.Value);

                var response = new
                {
                    CanAccess = canAccess,
                    NeedsUpgrade = needsUpgrade,
                    UserTier = userTier?.ToString(),
                    RequiredTier = requiredTier?.ToString(),
                    Message = canAccess 
                        ? "Access granted" 
                        : needsUpgrade 
                            ? $"You need to upgrade to {requiredTier} subscription to access this LearnHub"
                            : "Access denied"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while checking access", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all LearnHubs the current user can access
        /// </summary>
        /// <returns>List of accessible LearnHubs</returns>
        [HttpGet("accessible-learnhubs")]
        public async Task<IActionResult> GetAccessibleLearnHubs()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized();

                var accessibleLearnHubs = await _subscriptionAccessService.GetAccessibleLearnHubsAsync(userId.Value);
                
                return Ok(new
                {
                    LearnHubs = accessibleLearnHubs,
                    Count = accessibleLearnHubs.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching accessible LearnHubs", error = ex.Message });
            }
        }

        /// <summary>
        /// Get the current user's subscription tier
        /// </summary>
        /// <returns>User's current subscription tier</returns>
        [HttpGet("user-tier")]
        public async Task<IActionResult> GetUserTier()
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized();

                var userTier = await _subscriptionAccessService.GetUserAccessTierAsync(userId.Value);
                
                return Ok(new
                {
                    Tier = userTier?.ToString(),
                    HasActiveSubscription = userTier != null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching user tier", error = ex.Message });
            }
        }


        private Guid? GetCurrentUserId()
        {
            // Try the full claim type name first (what your JWT actually uses)
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            
            // Try alternative claim names
            var alternativeClaims = new[] { 
                "nameid", 
                "UserId", 
                "sub", 
                "id",
                "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
            };
            
            foreach (var claimType in alternativeClaims)
            {
                var claim = User.FindFirst(claimType);
                if (claim != null && Guid.TryParse(claim.Value, out var altUserId))
                {
                    return altUserId;
                }
            }
            
            return null;
        }
    }
}
