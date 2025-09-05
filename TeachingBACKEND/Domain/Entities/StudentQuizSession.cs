using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.Entities;

/// <summary>
/// Manages active quiz sessions for students
/// This table can be cleaned up periodically to maintain performance
/// </summary>
public class StudentQuizSession
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
    public DateTime StartedAt { get; set; }
    
    public DateTime? LastActivityAt { get; set; } = DateTime.UtcNow;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Session management
    public bool IsActive { get; set; } = true;
    public string SessionToken { get; set; } = Guid.NewGuid().ToString(); // For session validation
    
    // Optional: Store temporary progress
    public string? CurrentAnswerId { get; set; }
    public int? TimeSpentSeconds { get; set; }
}

