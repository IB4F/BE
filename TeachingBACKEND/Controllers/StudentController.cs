using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Api.Controllers;

[ApiController]
[Route("api/student")]
[Authorize(Roles = "Student")]
public class StudentController : ControllerBase
{
    private readonly IStudentPerformanceService _performanceService;

    public StudentController(IStudentPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    /// <summary>
    /// Get all parent quizzes for a specific link with student progress (for students)
    /// This endpoint returns only parent quiz IDs and progress information
    /// </summary>
    /// <param name="linkId">The ID of the link to get quizzes for</param>
    /// <returns>Parent quiz IDs and progress information</returns>
    [HttpGet("quizzes/{linkId}")]
    public async Task<IActionResult> GetQuizzesByLinkId(Guid linkId)
    {
        try
        {
            // Get the current user's ID from the JWT token
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
            {
                return Unauthorized(new { error = "Invalid user authentication" });
            }

            var result = await _performanceService.GetQuizzesWithPerformance(linkId, studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }



    /// <summary>
    /// Get a single quiz by ID (for students)
    /// This endpoint excludes sensitive information like correct answers for security
    /// </summary>
    /// <param name="quizId">The ID of the quiz to retrieve</param>
    /// <returns>The quiz details without sensitive information</returns>
    [HttpGet("quizzes/single/{quizId}")]
    public async Task<IActionResult> GetSingleQuiz(Guid quizId)
    {
        try
        {
            if (quizId == Guid.Empty)
                return BadRequest(new { error = "Valid quiz ID is required" });

            var result = await _performanceService.GetSingleQuiz(quizId);
            
            if (result == null)
                return NotFound(new { error = "Quiz not found" });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Start a quiz (record the start time for time tracking)
    /// </summary>
    /// <param name="quizId">The ID of the quiz to start</param>
    /// <returns>Confirmation that the quiz has been started</returns>
    [HttpPost("quizzes/{quizId}/start")]
    public async Task<IActionResult> StartQuiz(Guid quizId)
    {
        try
        {
            if (quizId == Guid.Empty)
                return BadRequest(new { error = "Valid quiz ID is required" });

            // Get the current user's ID from the JWT token
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
            {
                return Unauthorized(new { error = "Invalid user authentication" });
            }

            var result = await _performanceService.StartQuizSession(quizId, studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Submit a student's answer to a quiz
    /// This endpoint validates the answer and determines the next quiz to present
    /// </summary>
    /// <param name="submission">The quiz submission containing quizId, answer, and start time</param>
    /// <returns>Response indicating if the answer was correct and what quiz to show next</returns>
    [HttpPost("quizzes/submit")]
    public async Task<IActionResult> SubmitAnswer([FromBody] StudentQuizSubmissionDTO submission)
    {
        try
        {
            if (submission == null)
                return BadRequest(new { error = "Submission data is required" });

            if (submission.QuizId == Guid.Empty)
                return BadRequest(new { error = "Valid quiz ID is required" });

            // Validate that at least one answer is provided
            if (string.IsNullOrWhiteSpace(submission.AnswerId) && 
                (submission.AnswerIds == null || !submission.AnswerIds.Any()))
                return BadRequest(new { error = "Either AnswerId or AnswerIds is required" });

            // Get the current user's ID from the JWT token
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
            {
                return Unauthorized(new { error = "Invalid user authentication" });
            }

            var result = await _performanceService.SubmitAnswer(submission, studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get detailed performance analytics for the current student
    /// </summary>
    /// <param name="linkId">The ID of the link to get analytics for</param>
    /// <returns>Detailed performance analytics and recommendations</returns>
    [HttpGet("analytics/{linkId}")]
    public async Task<IActionResult> GetPerformanceAnalytics(Guid linkId)
    {
        try
        {
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
            {
                return Unauthorized(new { error = "Invalid user authentication" });
            }

            var result = await _performanceService.GetPerformanceAnalytics(studentId, linkId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
