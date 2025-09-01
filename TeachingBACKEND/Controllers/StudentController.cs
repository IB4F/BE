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
    private readonly ILearnHubService _learnHubService;

    public StudentController(ILearnHubService learnHubService)
    {
        _learnHubService = learnHubService;
    }

    /// <summary>
    /// Get all parent quizzes for a specific link with student progress (for students)
    /// This endpoint excludes sensitive information like correct answers and includes progress tracking
    /// </summary>
    /// <param name="linkId">The ID of the link to get quizzes for</param>
    /// <returns>List of quizzes with progress information</returns>
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

            var result = await _learnHubService.GetStudentQuizzesWithProgress(linkId, studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Get a specific quiz by ID (for students)
    /// This endpoint excludes sensitive information like correct answers
    /// </summary>
    /// <param name="quizId">The ID of the quiz to get</param>
    /// <returns>Quiz without correct answer information</returns>
    [HttpGet("quizzes/single/{quizId}")]
    public async Task<IActionResult> GetQuizById(Guid quizId)
    {
        try
        {
            var result = await _learnHubService.GetStudentQuizById(quizId);
            return result != null ? Ok(result) : NotFound();
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

            var result = await _learnHubService.StartStudentQuiz(quizId, studentId);
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

            if (string.IsNullOrWhiteSpace(submission.AnswerId))
                return BadRequest(new { error = "Answer ID is required" });

            // Get the current user's ID from the JWT token
            var studentIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if (string.IsNullOrEmpty(studentIdClaim) || !Guid.TryParse(studentIdClaim, out var studentId))
            {
                return Unauthorized(new { error = "Invalid user authentication" });
            }

            var result = await _learnHubService.SubmitStudentAnswer(submission, studentId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
