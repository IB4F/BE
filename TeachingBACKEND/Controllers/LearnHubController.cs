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
        private readonly IConfiguration _configuration;

        public LearnHubsController(ILearnHubService learnHubService, IConfiguration configuration)
        {
            _learnHubService = learnHubService;
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
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["JWT_SECRET_KEY"]);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    isAuthenticated = true;
                }
                catch
                {
                    // Token is invalid, user is not authenticated
                    isAuthenticated = false;
                }
            }
            
            var learnHubs = await _learnHubService.GetFilteredLearnHubs(classType, subject, isAuthenticated);
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

    }
}
