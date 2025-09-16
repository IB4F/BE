using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController : ControllerBase
    {
        private readonly ISupervisorService _supervisorService;
        private readonly ILogger<SupervisorController> _logger;

        public SupervisorController(ISupervisorService supervisorService, ILogger<SupervisorController> logger)
        {
            _supervisorService = supervisorService;
            _logger = logger;
        }

        /// <summary>
        /// Dërgon një aplikim për të bërë supervizor
        /// </summary>
        /// <param name="model">Detajet e aplikimit të supervizorit</param>
        /// <returns>ID-ja e aplikimit</returns>
        [HttpPost("apply")]
        public async Task<IActionResult> SubmitApplication([FromBody] SupervisorApplicationDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var applicationId = await _supervisorService.SubmitSupervisorApplication(model);
                
                _logger.LogInformation("Supervisor application submitted with ID: {ApplicationId}", applicationId);
                
                return Ok(new { ApplicationId = applicationId, Message = "Aplikimi u dërgua me sukses. Do të kontaktoheni së shpejti." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting supervisor application");
                return StatusCode(500, "Ndodhi një gabim gjatë dërgimit të aplikimit.");
            }
        }

        /// <summary>
        /// Pranon ose refuzon një aplikim supervizori (vetëm Admin)
        /// </summary>
        /// <param name="model">Detajet e miratimit</param>
        /// <returns>Statusi i suksesit</returns>
        [HttpPost("approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveSupervisor([FromBody] SupervisorApprovalDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _supervisorService.ApproveSupervisor(model);
                
                if (result)
                {
                    var message = model.IsApproved ? "Supervizori u pranua me sukses." : "Supervizori u refuzua me sukses.";
                    _logger.LogInformation("Supervisor {SupervisorId} approval status set to {IsApproved}", model.SupervisorId, model.IsApproved);
                    return Ok(new { Message = message });
                }
                
                return BadRequest("Ndodhi një gabim gjatë përpunimit të aplikimit.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing supervisor approval for {SupervisorId}", model.SupervisorId);
                return StatusCode(500, "Ndodhi një gabim gjatë përpunimit të aplikimit.");
            }
        }

        /// <summary>
        /// Merr të gjitha aplikimet e supervizorëve që janë në pritje (vetëm Admin)
        /// </summary>
        /// <returns>Lista e aplikimeve të supervizorëve</returns>
        [HttpGet("applications")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingApplications()
        {
            try
            {
                var applications = await _supervisorService.GetPendingSupervisorApplications();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending supervisor applications");
                return StatusCode(500, "Ndodhi një gabim gjatë marrjes së aplikimeve.");
            }
        }

        /// <summary>
        /// Krijon një nxënës të ri nën menaxhimin e supervizorit
        /// </summary>
        /// <param name="model">Detajet e krijimit të nxënësit</param>
        /// <returns>Kredencialet e nxënësit</returns>
        [HttpPost("students")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentBySupervisorDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var result = await _supervisorService.CreateStudent(model, supervisorId);
                
                _logger.LogInformation("Student created by supervisor {SupervisorId}: {StudentEmail}", supervisorId, result.GeneratedEmail);
                
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation when creating student");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student for supervisor");
                return StatusCode(500, "Ndodhi një gabim gjatë krijimit të nxënësit.");
            }
        }

        /// <summary>
        /// Merr të gjithë nxënësit nën menaxhimin e supervizorit
        /// </summary>
        /// <returns>Lista e nxënësve të supervizuar</returns>
        [HttpGet("students")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> GetSupervisedStudents()
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var students = await _supervisorService.GetSupervisedStudents(supervisorId);
                
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving supervised students");
                return StatusCode(500, "Ndodhi një gabim gjatë marrjes së listës së nxënësve.");
            }
        }

        /// <summary>
        /// Fshin një nxënës nga menaxhimi i supervizorit
        /// </summary>
        /// <param name="studentId">ID-ja e nxënësit që duhet fshirë</param>
        /// <returns>Statusi i suksesit</returns>
        [HttpDelete("students/{studentId}")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var result = await _supervisorService.DeleteStudent(studentId, supervisorId);
                
                if (result)
                {
                    _logger.LogInformation("Student {StudentId} deleted by supervisor {SupervisorId}", 
                        studentId, supervisorId);
                    return Ok(new { Message = "Nxënësi u fshi me sukses." });
                }
                
                return BadRequest("Ndodhi një gabim gjatë fshirjes së nxënësit.");
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid operation when deleting student {StudentId}", studentId);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student {StudentId}", studentId);
                return StatusCode(500, "Ndodhi një gabim gjatë fshirjes së nxënësit.");
            }
        }

        /// <summary>
        /// Trajton kërkesën për rivendosje të fjalëkalimit për një nxënës
        /// </summary>
        /// <param name="studentId">ID-ja e nxënësit</param>
        /// <param name="approve">Nëse duhet të pranojë kërkesën për rivendosje</param>
        /// <returns>Statusi i suksesit</returns>
        [HttpPost("students/{studentId}/password-reset")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> HandlePasswordResetRequest(Guid studentId, [FromBody] bool approve)
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var result = await _supervisorService.HandlePasswordResetRequest(studentId, approve);
                
                if (result)
                {
                    var message = approve ? "Kërkesa për rivendosje të fjalëkalimit u pranua." : "Kërkesa për rivendosje të fjalëkalimit u refuzua.";
                    _logger.LogInformation("Password reset request for student {StudentId} {Status} by supervisor {SupervisorId}", 
                        studentId, approve ? "approved" : "rejected", supervisorId);
                    return Ok(new { Message = message });
                }
                
                return BadRequest("Ndodhi një gabim gjatë përpunimit të kërkesës.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling password reset request for student {StudentId}", studentId);
                return StatusCode(500, "Ndodhi një gabim gjatë përpunimit të kërkesës.");
            }
        }


        /// <summary>
        /// Merr kërkesat në pritje për rivendosje të fjalëkalimit
        /// </summary>
        /// <returns>Lista e kërkesave në pritje</returns>
        [HttpGet("password-reset-requests")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> GetPendingPasswordResetRequests()
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var requests = await _supervisorService.GetPendingPasswordResetRequests(supervisorId);
                
                return Ok(requests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending password reset requests");
                return StatusCode(500, "Ndodhi një gabim gjatë marrjes së kërkesave.");
            }
        }

        /// <summary>
        /// Merr të dhënat e dashboard-it të supervizorit
        /// </summary>
        /// <returns>Të dhënat e dashboard-it</returns>
        [HttpGet("dashboard")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                var dashboardData = await _supervisorService.GetDashboardData(supervisorId);
                
                return Ok(dashboardData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving dashboard data for supervisor");
                return StatusCode(500, "Ndodhi një gabim gjatë marrjes së të dhënave të dashboard-it.");
            }
        }

        /// <summary>
        /// Merr detajet e hollësishme të progresit për një nxënës specifik
        /// </summary>
        /// <param name="studentId">ID-ja e nxënësit</param>
        /// <returns>Detajet e hollësishme të progresit të nxënësit</returns>
        [HttpGet("students/{studentId}/progress-detail")]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> GetStudentProgressDetail(Guid studentId)
        {
            try
            {
                // Get supervisor ID from JWT token
                var supervisorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(supervisorIdClaim, out var supervisorId))
                {
                    return Unauthorized("Token i pavlefshëm.");
                }

                // Verify the student belongs to this supervisor
                var student = await _supervisorService.GetSupervisedStudents(supervisorId);
                if (!student.Any(s => s.StudentId == studentId))
                {
                    return Forbid("Nuk keni akses në këtë nxënës.");
                }

                var progressDetail = await _supervisorService.GetStudentProgressDetail(studentId);
                
                return Ok(progressDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving progress detail for student {StudentId}", studentId);
                return StatusCode(500, "Ndodhi një gabim gjatë marrjes së detajeve të progresit.");
            }
        }

    }
}
