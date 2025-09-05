namespace TeachingBACKEND.Domain.DTOs;

/// <summary>
/// Detailed performance analytics for a student
/// </summary>
public class StudentPerformanceAnalyticsDTO
{
    public Guid StudentId { get; set; }
    public Guid LinkId { get; set; }
    
    // Overall progress
    public int TotalQuizzes { get; set; }
    public int CompletedQuizzes { get; set; }
    public double CompletionRate { get; set; }
    
    // Points and scoring
    public int TotalPointsEarned { get; set; }
    public int TotalPossiblePoints { get; set; }
    public double AverageScore { get; set; }
    public double ScorePercentage { get; set; }
    
    // Time analytics
    public int TotalTimeSpent { get; set; }
    public double AverageTimePerQuiz { get; set; }
    public TimeSpan TotalTimeSpentFormatted => TimeSpan.FromSeconds(TotalTimeSpent);
    public TimeSpan AverageTimePerQuizFormatted => TimeSpan.FromSeconds((int)AverageTimePerQuiz);
    
    // Performance trends
    public DateTime? FirstAttemptAt { get; set; }
    public DateTime? LastAttemptAt { get; set; }
    public TimeSpan? TotalDuration => LastAttemptAt.HasValue && FirstAttemptAt.HasValue 
        ? LastAttemptAt.Value - FirstAttemptAt.Value 
        : null;
    
    // Quiz-specific performance
    public List<QuizPerformanceDetailDTO> QuizPerformances { get; set; } = new();
    
    // Recommendations
    public List<string> Recommendations { get; set; } = new();
}

public class QuizPerformanceDetailDTO
{
    public Guid QuizId { get; set; }
    public string Question { get; set; }
    public bool IsCompleted { get; set; }
    public int PointsEarned { get; set; }
    public int MaxPoints { get; set; }
    public int TimeSpentSeconds { get; set; }
    public int AttemptsCount { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double ScorePercentage => MaxPoints > 0 ? (double)PointsEarned / MaxPoints * 100 : 0;
}

