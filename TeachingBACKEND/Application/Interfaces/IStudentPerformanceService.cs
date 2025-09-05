using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.DTOs.Quizzes;

namespace TeachingBACKEND.Application.Interfaces;

/// <summary>
/// Service for managing student performance tracking with scalable architecture
/// </summary>
public interface IStudentPerformanceService
{
    /// <summary>
    /// Start a quiz session for a student
    /// </summary>
    Task<StudentQuizStartResponseDTO> StartQuizSession(Guid quizId, Guid studentId);
    
    /// <summary>
    /// Submit a student's answer and update performance
    /// </summary>
    Task<StudentQuizSubmissionResponseDTO> SubmitAnswer(StudentQuizSubmissionDTO submission, Guid studentId);
    
    /// <summary>
    /// Get simplified quiz list with performance summary
    /// </summary>
    Task<StudentQuizSimpleResponseDTO> GetQuizzesWithPerformance(Guid linkId, Guid studentId);
    
    /// <summary>
    /// Get a single quiz by ID (for students)
    /// </summary>
    Task<StudentQuizDTO?> GetSingleQuiz(Guid quizId);
    
    /// <summary>
    /// Get detailed performance analytics for a student
    /// </summary>
    Task<StudentPerformanceAnalyticsDTO> GetPerformanceAnalytics(Guid studentId, Guid linkId);
    
    /// <summary>
    /// Clean up expired quiz sessions (for maintenance)
    /// </summary>
    Task<int> CleanupExpiredSessions(TimeSpan expirationThreshold);
    
    /// <summary>
    /// Update performance summary for a student-link combination
    /// </summary>
    Task UpdatePerformanceSummary(Guid studentId, Guid linkId);
}
