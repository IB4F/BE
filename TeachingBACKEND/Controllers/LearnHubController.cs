using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class LearnHubsController : ControllerBase
    {
        private readonly ILearnHubService _learnHubService;

        public LearnHubsController(ILearnHubService learnHubService)
        {
            _learnHubService = learnHubService;
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

            var learnHubs = await _learnHubService.GetFilteredLearnHubs(classType, subject);
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
