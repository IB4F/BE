using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TeachingBACKEND.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class LearnHubsController : ControllerBase
    {
        private readonly ILearnHubService _learnHubService;
        private readonly ISubscriptionAccessService _subscriptionAccessService;
        private readonly IConfiguration _configuration;

        public LearnHubsController(ILearnHubService learnHubService, ISubscriptionAccessService subscriptionAccessService, IConfiguration configuration)
        {
            _learnHubService = learnHubService;
            _subscriptionAccessService = subscriptionAccessService;
            _configuration = configuration;
        }

        [HttpPost("Post-Learnhub")]
        public async Task<IActionResult> PostLearnHub(LearnHubCreateDTO learnHub)
        {
            var newLearnHub = await _learnHubService.PostLearnHub(learnHub);
            return Ok(newLearnHub);
        }

        [HttpGet("Get-List-Learnhubs")]
        public async Task<IActionResult> GetAllLearnHubs()
        {
            var learnHubs = await _learnHubService.GetLearnHubs();
            return Ok(learnHubs);
        }

        [HttpGet("Get-Single-Learnhub")]
        public async Task<IActionResult> GetLearnHub(Guid id)
        {
            var learnHubs = await _learnHubService.GetSingleLearnHub(id);
            return Ok(learnHubs);
        }

        [HttpPut("Update-Learnhub")]
        public async Task<IActionResult> UpdateLearnHub(Guid id, LearnHubCreateDTO dto)
        {
            var learnHubs = await _learnHubService.UpdateLearnHub(id, dto);
            return Ok(learnHubs);
        }

        [HttpDelete("Delete-Learnhub")]
        public async Task<IActionResult> DeleteLearnHub(Guid id)
        {
            await _learnHubService.DeleteLearnHub(id);
            return Ok(new { message = "LearnHub u fshi" });    
        }

        [HttpPost("Get-Paginated-Learnhubs")]
        public async Task<IActionResult> GetPaginatedLearnHubs([FromBody] PaginationRequestDTO dto)
        {
            if (dto.PageNumber < 0 || dto.PageSize <= 0)
                return BadRequest(new { error = "PageSize must be greater than zero." });

            var result = await _learnHubService.GetPaginatedLearnHubs(dto);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("Filter-Learnhubs")]
        public async Task<IActionResult> GetFilteredLearnHubs([FromQuery] string classType, [FromQuery] string subject)
        {
            if (string.IsNullOrWhiteSpace(classType) || string.IsNullOrWhiteSpace(subject))
                return BadRequest(new { error = "ClassType and Subject are required." });

            // Check if user is authenticated by validating JWT token
            var isAuthenticated = false;
            var isAdmin = false;
            Guid? userId = null;
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    // Extract user ID and role from the token
                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid");
                    if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid extractedUserId))
                    {
                        userId = extractedUserId;
                    }

                    // Check if user is admin
                    var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "role" || x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
                    if (roleClaim != null && roleClaim.Value == "Admin")
                    {
                        isAdmin = true;
                    }

                    isAuthenticated = true;
                }
                catch
                {
                    // Token is invalid, user is not authenticated
                    isAuthenticated = false;
                }
            }
            
            var learnHubs = await _learnHubService.GetFilteredLearnHubs(classType, subject, isAuthenticated, userId);
            
            // If user is authenticated and NOT admin, filter based on subscription access
            if (isAuthenticated && userId.HasValue && !isAdmin)
            {
                var accessibleLearnHubs = await _subscriptionAccessService.GetAccessibleLearnHubsAsync(userId.Value);
                var accessibleIds = accessibleLearnHubs.Select(lh => lh.Id).ToHashSet();
                
                // Filter the results to only include accessible LearnHubs
                learnHubs = learnHubs.Where(lh => accessibleIds.Contains(lh.Id)).ToList();
            }
            
            return Ok(learnHubs);
        }
        
        [HttpPost("migrate-class-types")]
        public async Task<IActionResult> MigrateClassTypes()
        {
            try
            {
                await _learnHubService.MigrateLearnHubClassTypes();
                return Ok(new { message = "LearnHub class types migrated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("check-access/{learnHubId}")]
        public async Task<IActionResult> CheckLearnHubAccess(Guid learnHubId)
        {
            try
            {
                // Check if user is authenticated
                var isAuthenticated = false;
                Guid? userId = null;
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    try
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
                        tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);

                        var jwtToken = (JwtSecurityToken)validatedToken;
                        var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "nameid");
                        if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid extractedUserId))
                        {
                            userId = extractedUserId;
                        }

                        isAuthenticated = true;
                    }
                    catch
                    {
                        isAuthenticated = false;
                    }
                }

                if (!isAuthenticated || !userId.HasValue)
                {
                    // For unauthenticated users, only allow access to free LearnHubs
                    var learnHub = await _learnHubService.GetSingleLearnHub(learnHubId);
                    if (learnHub == null)
                        return NotFound(new { message = "LearnHub not found" });

                    var canAccess = learnHub.IsFree;
                    return Ok(new
                    {
                        CanAccess = canAccess,
                        NeedsUpgrade = !canAccess,
                        UserTier = (string)null,
                        RequiredTier = learnHub.RequiredTier?.ToString(),
                        Message = canAccess ? "Access granted (free content)" : "Please log in to access this content"
                    });
                }

                // For authenticated users, check subscription access
                var userCanAccess = await _subscriptionAccessService.CanUserAccessLearnHubAsync(userId.Value, learnHubId);
                var needsUpgrade = await _subscriptionAccessService.NeedsUpgradeForLearnHubAsync(userId.Value, learnHubId);
                var requiredTier = await _subscriptionAccessService.GetLearnHubRequiredTierAsync(learnHubId);
                var userTier = await _subscriptionAccessService.GetUserAccessTierAsync(userId.Value);

                var response = new
                {
                    CanAccess = userCanAccess,
                    NeedsUpgrade = needsUpgrade,
                    UserTier = userTier?.ToString(),
                    RequiredTier = requiredTier?.ToString(),
                    Message = userCanAccess 
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

    }
}
