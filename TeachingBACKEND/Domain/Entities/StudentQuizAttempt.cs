using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.Entities;

/// <summary>
/// Stores final quiz performance results for a student
/// One record per student-quiz combination (no duplicates)
/// </summary>
public class StudentQuizPerformance
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    
    [Required]
    public Guid QuizId { get; set; }
    public Quizz Quiz { get; set; }
    
    [Required]
    public Guid LinkId { get; set; }
    public Link Link { get; set; }
    
    [Required]
    public string SubmittedAnswerId { get; set; }
    
    [Required]
    public bool IsCorrect { get; set; }
    
    public int PointsEarned { get; set; }
    
    [Required]
    public DateTime StartedAt { get; set; }
    
    [Required]
    public DateTime CompletedAt { get; set; }
    
    public int TimeSpentSeconds { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // New fields for better performance tracking
    public int AttemptsCount { get; set; } = 1; // Number of attempts for this quiz
    public DateTime? LastAttemptAt { get; set; } // Last attempt timestamp
    public bool IsCompleted { get; set; } = true; // Always true for performance records
}
