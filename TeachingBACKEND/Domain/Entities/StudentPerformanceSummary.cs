using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.Entities;

/// <summary>
/// Aggregated performance metrics for students
/// Updated incrementally to avoid recalculating from raw data
/// </summary>
public class StudentPerformanceSummary
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    
    [Required]
    public Guid LinkId { get; set; }
    public Link Link { get; set; }
    
    // Aggregated metrics (updated incrementally)
    public int TotalQuizzes { get; set; }
    public int CompletedQuizzes { get; set; }
    public int TotalPointsEarned { get; set; }
    public int TotalPossiblePoints { get; set; }
    
    // Performance statistics
    public double CompletionRate { get; set; } // Percentage of completed quizzes
    public double AverageScore { get; set; } // Average points per quiz
    public int TotalTimeSpent { get; set; } // Total time spent in seconds
    public double AverageTimePerQuiz { get; set; } // Average time per quiz
    
    // Tracking
    public DateTime? FirstAttemptAt { get; set; }
    public DateTime? LastAttemptAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Index for fast lookups
    public string StudentLinkKey => $"{StudentId}_{LinkId}";
}

