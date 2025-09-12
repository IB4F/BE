using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get student dashboard data including progress stats and latest LearnHubs
        /// </summary>
        /// <returns>Dashboard data with stats and latest LearnHubs progress</returns>
        [HttpGet("student")]
        public async Task<ActionResult<DashboardDTO>> GetStudentDashboard()
        {
            try
            {
                // Get student ID from JWT token claims
                var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
                {
                    return Unauthorized("Invalid user token");
                }

                var dashboard = await _dashboardService.GetStudentDashboardAsync(studentId);
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get dashboard data for a specific student (admin/teacher access)
        /// </summary>
        /// <param name="studentId">The ID of the student</param>
        /// <returns>Dashboard data for the specified student</returns>
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<DashboardDTO>> GetStudentDashboardById(Guid studentId)
        {
            try
            {
                var dashboard = await _dashboardService.GetStudentDashboardAsync(studentId);
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
