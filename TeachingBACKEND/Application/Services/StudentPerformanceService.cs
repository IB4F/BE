using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.DTOs.Quizzes;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services;

/// <summary>
/// Scalable service for managing student performance tracking
/// </summary>
public class StudentPerformanceService : IStudentPerformanceService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StudentPerformanceService> _logger;

    public StudentPerformanceService(ApplicationDbContext context, ILogger<StudentPerformanceService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<StudentQuizStartResponseDTO> StartQuizSession(Guid quizId, Guid studentId)
    {
        var quiz = await _context.Quizzes
            .FirstOrDefaultAsync(q => q.Id == quizId);

        if (quiz == null)
            throw new Exception("Quiz not found");

        var startTime = DateTime.UtcNow;

        // Check if there's already an active session for this student and quiz
        var existingSession = await _context.StudentQuizSessions
            .FirstOrDefaultAsync(sqs => sqs.StudentId == studentId && 
                                       sqs.QuizId == quizId && 
                                       sqs.LinkId == quiz.LinkId &&
                                       sqs.IsActive);

        if (existingSession != null)
        {
            // Update the existing session
            existingSession.StartedAt = startTime;
            existingSession.LastActivityAt = startTime;
            existingSession.UpdatedAt = startTime;
        }
        else
        {
            // Create a new session
            var session = new StudentQuizSession
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                QuizId = quizId,
                LinkId = quiz.LinkId,
                StartedAt = startTime,
                LastActivityAt = startTime,
                IsActive = true
            };

            _context.StudentQuizSessions.Add(session);
        }

        await _context.SaveChangesAsync();

        return new StudentQuizStartResponseDTO
        {
            QuizId = quizId,
            StartedAt = startTime,
            Message = "Quiz session started successfully"
        };
    }

    public async Task<StudentQuizSubmissionResponseDTO> SubmitAnswer(StudentQuizSubmissionDTO submission, Guid studentId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Options)
            .Include(q => q.ChildQuizzes)
            .Include(q => q.ExplanationAudio)
            .FirstOrDefaultAsync(q => q.Id == submission.QuizId);

        if (quiz == null)
            throw new Exception("Quiz not found");

        // Check if the answer is correct
        var correctOption = quiz.Options.FirstOrDefault(o => o.IsCorrect);
        var isCorrect = correctOption?.Id.ToString() == submission.AnswerId;
        var pointsEarned = isCorrect ? quiz.Points : 0;

        var completedAt = DateTime.UtcNow;

        // Find or create performance record
        var performance = await _context.StudentQuizPerformances
            .FirstOrDefaultAsync(sqp => sqp.StudentId == studentId && 
                                       sqp.QuizId == submission.QuizId &&
                                       sqp.LinkId == quiz.LinkId);

        if (performance == null)
        {
            // Create new performance record
            var startTime = submission.StartedAt ?? completedAt;
            var timeSpent = (int)(completedAt - startTime).TotalSeconds;

            performance = new StudentQuizPerformance
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                QuizId = quiz.Id,
                LinkId = quiz.LinkId,
                SubmittedAnswerId = submission.AnswerId,
                IsCorrect = isCorrect,
                PointsEarned = pointsEarned,
                StartedAt = startTime,
                CompletedAt = completedAt,
                TimeSpentSeconds = timeSpent,
                AttemptsCount = 1,
                LastAttemptAt = completedAt
            };

            _context.StudentQuizPerformances.Add(performance);
        }
        else
        {
            // Update existing performance record
            performance.SubmittedAnswerId = submission.AnswerId;
            performance.IsCorrect = isCorrect;
            performance.PointsEarned = pointsEarned;
            performance.CompletedAt = completedAt;
            performance.AttemptsCount++;
            performance.LastAttemptAt = completedAt;
            performance.UpdatedAt = completedAt;

            // Update time spent if this is a new attempt
            if (submission.StartedAt.HasValue)
            {
                var timeSpent = (int)(completedAt - submission.StartedAt.Value).TotalSeconds;
                performance.TimeSpentSeconds = timeSpent;
            }
        }

        // Close the active session
        var activeSession = await _context.StudentQuizSessions
            .FirstOrDefaultAsync(sqs => sqs.StudentId == studentId && 
                                       sqs.QuizId == submission.QuizId &&
                                       sqs.LinkId == quiz.LinkId &&
                                       sqs.IsActive);

        if (activeSession != null)
        {
            activeSession.IsActive = false;
            activeSession.UpdatedAt = completedAt;
        }

        await _context.SaveChangesAsync();

        // Update performance summary
        await UpdatePerformanceSummary(studentId, quiz.LinkId);

        // Determine next quiz based on answer correctness
        var nextQuizId = await DetermineNextQuiz(quiz, isCorrect);

        // Determine if the next quiz is a parent or child quiz
        string? parentQuizId = null;
        string? childQuizId = null;

        if (nextQuizId.HasValue)
        {
            var nextQuiz = await _context.Quizzes
                .Where(q => q.Id == nextQuizId.Value)
                .Select(q => new { q.Id, q.ParentQuizId })
                .FirstOrDefaultAsync();

            if (nextQuiz != null)
            {
                if (nextQuiz.ParentQuizId == null)
                {
                    // Next quiz is a parent quiz
                    parentQuizId = nextQuiz.Id.ToString();
                }
                else
                {
                    // Next quiz is a child quiz
                    childQuizId = nextQuiz.Id.ToString();
                }
            }
        }

        return new StudentQuizSubmissionResponseDTO
        {
            Answer = isCorrect,
            ParentQuizId = parentQuizId,
            ChildQuizId = childQuizId,
            Explanation = isCorrect ? null : quiz.Explanation,
            ExplanationAudioUrl = isCorrect ? null : quiz.ExplanationAudio?.FileUrl
        };
    }

    public async Task<StudentQuizSimpleResponseDTO> GetQuizzesWithPerformance(Guid linkId, Guid studentId)
    {
        // Get all parent quizzes for the link
        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null)
            .OrderBy(q => q.CreatedAt)
            .ToListAsync();

        // Get ALL quizzes (parent + child) for total possible points
        var allQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId)
            .ToListAsync();

        // Get performance summary (use cached data if available)
        var performanceSummary = await _context.StudentPerformanceSummaries
            .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);

        if (performanceSummary == null)
        {
            // Calculate from raw data if summary doesn't exist
            await UpdatePerformanceSummary(studentId, linkId);
            performanceSummary = await _context.StudentPerformanceSummaries
                .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);
        }

        var progress = new StudentProgressSummaryDTO
        {
            TotalQuizzes = parentQuizzes.Count,
            CompletedQuizzes = performanceSummary?.CompletedQuizzes ?? 0,
            CurrentQuizIndex = performanceSummary?.CompletedQuizzes ?? 0,
            TotalPointsEarned = performanceSummary?.TotalPointsEarned ?? 0,
            TotalPossiblePoints = allQuizzes.Sum(q => q.Points),
            LastCompletedQuizId = null, // Would need to query from performance records
            LastCompletedAt = performanceSummary?.LastAttemptAt
        };

        var parentQuizIds = parentQuizzes.Select(q => q.Id).ToList();

        return new StudentQuizSimpleResponseDTO
        {
            ParentQuizIds = parentQuizIds,
            Progress = progress
        };
    }

    public async Task<StudentQuizDTO?> GetSingleQuiz(Guid quizId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Options)
            .Include(q => q.QuizzType)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .FirstOrDefaultAsync(q => q.Id == quizId);

        if (quiz == null)
            return null;

        return new StudentQuizDTO
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Points = quiz.Points,
            Options = quiz.Options.Select(o => new StudentOptionDTO
            {
                Id = o.Id.ToString(),
                OptionText = o.OptionText,
                OptionImageUrl = o.OptionImage?.FileUrl
            }).ToList(),
            QuestionAudioUrl = quiz.QuestionAudio?.FileUrl,
            ExplanationAudioUrl = quiz.ExplanationAudio?.FileUrl,
            QuizzTypeName = quiz.QuizzType.Name,
            ParentQuizId = quiz.ParentQuizId
        };
    }

    public async Task<StudentPerformanceAnalyticsDTO> GetPerformanceAnalytics(Guid studentId, Guid linkId)
    {
        var performanceSummary = await _context.StudentPerformanceSummaries
            .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);

        if (performanceSummary == null)
        {
            await UpdatePerformanceSummary(studentId, linkId);
            performanceSummary = await _context.StudentPerformanceSummaries
                .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);
        }

        var quizPerformances = await _context.StudentQuizPerformances
            .Where(sqp => sqp.StudentId == studentId && sqp.LinkId == linkId)
            .Include(sqp => sqp.Quiz)
            .ToListAsync();

        var analytics = new StudentPerformanceAnalyticsDTO
        {
            StudentId = studentId,
            LinkId = linkId,
            TotalQuizzes = performanceSummary?.TotalQuizzes ?? 0,
            CompletedQuizzes = performanceSummary?.CompletedQuizzes ?? 0,
            CompletionRate = performanceSummary?.CompletionRate ?? 0,
            TotalPointsEarned = performanceSummary?.TotalPointsEarned ?? 0,
            TotalPossiblePoints = performanceSummary?.TotalPossiblePoints ?? 0,
            AverageScore = performanceSummary?.AverageScore ?? 0,
            ScorePercentage = performanceSummary?.TotalPossiblePoints > 0 
                ? (double)performanceSummary.TotalPointsEarned / performanceSummary.TotalPossiblePoints * 100 
                : 0,
            TotalTimeSpent = performanceSummary?.TotalTimeSpent ?? 0,
            AverageTimePerQuiz = performanceSummary?.AverageTimePerQuiz ?? 0,
            FirstAttemptAt = performanceSummary?.FirstAttemptAt,
            LastAttemptAt = performanceSummary?.LastAttemptAt,
            QuizPerformances = quizPerformances.Select(qp => new QuizPerformanceDetailDTO
            {
                QuizId = qp.QuizId,
                Question = qp.Quiz.Question,
                IsCompleted = qp.IsCompleted,
                PointsEarned = qp.PointsEarned,
                MaxPoints = qp.Quiz.Points,
                TimeSpentSeconds = qp.TimeSpentSeconds,
                AttemptsCount = qp.AttemptsCount,
                CompletedAt = qp.CompletedAt
            }).ToList()
        };

        // Generate recommendations
        analytics.Recommendations = GenerateRecommendations(analytics);

        return analytics;
    }

    public async Task<int> CleanupExpiredSessions(TimeSpan expirationThreshold)
    {
        var cutoffTime = DateTime.UtcNow.Subtract(expirationThreshold);
        
        var expiredSessions = await _context.StudentQuizSessions
            .Where(sqs => sqs.LastActivityAt < cutoffTime && sqs.IsActive)
            .ToListAsync();

        foreach (var session in expiredSessions)
        {
            session.IsActive = false;
            session.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return expiredSessions.Count;
    }

    public async Task UpdatePerformanceSummary(Guid studentId, Guid linkId)
    {
        var performances = await _context.StudentQuizPerformances
            .Where(sqp => sqp.StudentId == studentId && sqp.LinkId == linkId)
            .ToListAsync();

        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null)
            .ToListAsync();

        var allQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId)
            .ToListAsync();

        var summary = await _context.StudentPerformanceSummaries
            .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);

        if (summary == null)
        {
            summary = new StudentPerformanceSummary
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                LinkId = linkId
            };
            _context.StudentPerformanceSummaries.Add(summary);
        }

        // Update metrics
        summary.TotalQuizzes = parentQuizzes.Count;
        summary.CompletedQuizzes = performances.Count(sqp => sqp.IsCorrect);
        summary.TotalPointsEarned = performances.Sum(sqp => sqp.PointsEarned);
        summary.TotalPossiblePoints = allQuizzes.Sum(q => q.Points);
        summary.CompletionRate = summary.TotalQuizzes > 0 ? (double)summary.CompletedQuizzes / summary.TotalQuizzes * 100 : 0;
        summary.AverageScore = summary.CompletedQuizzes > 0 ? (double)summary.TotalPointsEarned / summary.CompletedQuizzes : 0;
        summary.TotalTimeSpent = performances.Sum(sqp => sqp.TimeSpentSeconds);
        summary.AverageTimePerQuiz = summary.CompletedQuizzes > 0 ? (double)summary.TotalTimeSpent / summary.CompletedQuizzes : 0;
        summary.FirstAttemptAt = performances.Any() ? performances.Min(sqp => sqp.StartedAt) : null;
        summary.LastAttemptAt = performances.Any() ? performances.Max(sqp => sqp.CompletedAt) : null;
        summary.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    private async Task<Guid?> DetermineNextQuiz(Quizz currentQuiz, bool isCorrect)
    {
        if (isCorrect)
        {
            // Move to next parent quiz
            var nextParentQuiz = await _context.Quizzes
                .Where(q => q.LinkId == currentQuiz.LinkId && 
                           q.ParentQuizId == null &&
                           q.CreatedAt > currentQuiz.CreatedAt)
                .OrderBy(q => q.CreatedAt)
                .FirstOrDefaultAsync();

            return nextParentQuiz?.Id;
        }
        else
        {
            // If this is a parent quiz, check for child quizzes
            if (currentQuiz.ParentQuizId == null)
            {
                var childQuiz = await _context.Quizzes
                    .Where(q => q.ParentQuizId == currentQuiz.Id)
                    .OrderBy(q => q.CreatedAt)
                    .FirstOrDefaultAsync();

                if (childQuiz != null)
                {
                    // Show child quiz if exists
                    return childQuiz.Id;
                }
                else
                {
                    // No child quizzes available, move to next parent quiz
                    var nextParentQuiz = await _context.Quizzes
                        .Where(q => q.LinkId == currentQuiz.LinkId && 
                                   q.ParentQuizId == null &&
                                   q.CreatedAt > currentQuiz.CreatedAt)
                        .OrderBy(q => q.CreatedAt)
                        .FirstOrDefaultAsync();

                    return nextParentQuiz?.Id;
                }
            }
            else
            {
                // This is a child quiz, check for next child quiz or return to parent
                var allChildQuizzes = await _context.Quizzes
                    .Where(q => q.ParentQuizId == currentQuiz.ParentQuizId)
                    .OrderBy(q => q.CreatedAt)
                    .ToListAsync();

                var currentChildIndex = allChildQuizzes.FindIndex(cq => cq.Id == currentQuiz.Id);

                if (currentChildIndex >= 0 && currentChildIndex < allChildQuizzes.Count - 1)
                {
                    // Show next child quiz
                    return allChildQuizzes[currentChildIndex + 1].Id;
                }
                else
                {
                    // This was the last child quiz, return to parent quiz
                    return currentQuiz.ParentQuizId;
                }
            }
        }
    }

    private List<string> GenerateRecommendations(StudentPerformanceAnalyticsDTO analytics)
    {
        var recommendations = new List<string>();

        if (analytics.CompletionRate < 50)
        {
            recommendations.Add("Focus on completing more quizzes to improve your overall performance.");
        }

        if (analytics.AverageTimePerQuiz > 300) // 5 minutes
        {
            recommendations.Add("Consider spending less time on each quiz to improve efficiency.");
        }

        if (analytics.ScorePercentage < 70)
        {
            recommendations.Add("Review incorrect answers to understand the concepts better.");
        }

        if (analytics.TotalTimeSpent > 3600) // 1 hour
        {
            recommendations.Add("Take regular breaks to maintain focus and performance.");
        }

        if (recommendations.Count == 0)
        {
            recommendations.Add("Great job! Keep up the excellent performance.");
        }

        return recommendations;
    }
}
