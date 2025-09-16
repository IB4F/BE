using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgressController : ControllerBase
    {
        private readonly IStudentProgressService _studentProgressService;
        private readonly ILogger<ProgressController> _logger;

        public ProgressController(IStudentProgressService studentProgressService, ILogger<ProgressController> logger)
        {
            _studentProgressService = studentProgressService;
            _logger = logger;
        }

        /// <summary>
        /// Gets detailed progress information for a student (for testing purposes)
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>Detailed progress information</returns>
        [HttpGet("student/{studentId}")]
        [Authorize(Roles = "Student,Supervisor,Admin")]
        public async Task<IActionResult> GetStudentProgress(Guid studentId)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
                {
                    return Unauthorized("Invalid token.");
                }

                // Get current user role
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Check permissions
                if (currentUserRole == "Student" && currentUserId != studentId)
                {
                    return Forbid("You can only view your own progress.");
                }

                var progressDetail = await _studentProgressService.GetStudentProgressDetailAsync(studentId);
                
                return Ok(progressDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving progress for student {StudentId}", studentId);
                return StatusCode(500, "An error occurred while retrieving progress information.");
            }
        }

        /// <summary>
        /// Gets progress statistics for a supervisor's students (for testing purposes)
        /// </summary>
        /// <param name="supervisorId">The supervisor's ID</param>
        /// <returns>Progress statistics</returns>
        [HttpGet("supervisor/{supervisorId}")]
        [Authorize(Roles = "Supervisor,Admin")]
        public async Task<IActionResult> GetSupervisorProgressStats(Guid supervisorId)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
                {
                    return Unauthorized("Invalid token.");
                }

                // Get current user role
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Check permissions
                if (currentUserRole == "Supervisor" && currentUserId != supervisorId)
                {
                    return Forbid("You can only view your own students' progress.");
                }

                var progressStats = await _studentProgressService.GetSupervisorProgressStatsAsync(supervisorId);
                
                return Ok(progressStats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving progress stats for supervisor {SupervisorId}", supervisorId);
                return StatusCode(500, "An error occurred while retrieving progress statistics.");
            }
        }

        /// <summary>
        /// Calculates progress for a specific student (for testing purposes)
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>Progress percentage</returns>
        [HttpGet("calculate/{studentId}")]
        [Authorize(Roles = "Student,Supervisor,Admin")]
        public async Task<IActionResult> CalculateStudentProgress(Guid studentId)
        {
            try
            {
                // Get current user ID from JWT token
                var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(currentUserIdClaim, out var currentUserId))
                {
                    return Unauthorized("Invalid token.");
                }

                // Get current user role
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Check permissions
                if (currentUserRole == "Student" && currentUserId != studentId)
                {
                    return Forbid("You can only view your own progress.");
                }

                var progress = await _studentProgressService.CalculateStudentProgressAsync(studentId);
                
                return Ok(new { StudentId = studentId, ProgressPercentage = progress });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating progress for student {StudentId}", studentId);
                return StatusCode(500, "An error occurred while calculating progress.");
            }
        }
    }
}
