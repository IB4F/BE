namespace TeachingBACKEND.Domain.DTOs;

public class StudentQuizProgressDTO
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }
    public int PointsEarned { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? TimeSpentSeconds { get; set; }
    public string? QuestionAudioUrl { get; set; }
    public string? ExplanationAudioUrl { get; set; }
    public string QuizzTypeName { get; set; }
    public Guid? ParentQuizId { get; set; }
    public List<StudentOptionDTO> Options { get; set; }
}

public class StudentQuizListResponseDTO
{
    public StudentProgressSummaryDTO Progress { get; set; }
    public List<StudentQuizProgressDTO> Quizzes { get; set; }
}

// New simplified DTO for the quizzes endpoint
public class StudentQuizSimpleResponseDTO
{
    public List<Guid> ParentQuizIds { get; set; } = new List<Guid>();
    public StudentProgressSummaryDTO Progress { get; set; }
}

public class StudentProgressSummaryDTO
{
    public int TotalQuizzes { get; set; }
    public int CompletedQuizzes { get; set; }
    public int CurrentQuizIndex { get; set; }
    public int TotalPointsEarned { get; set; }
    public int TotalPossiblePoints { get; set; } // New field for total points from all quizzes
    public Guid? LastCompletedQuizId { get; set; }
    public DateTime? LastCompletedAt { get; set; }
    public int CorrectAnswerQuiz { get; set; } // Count of correctly answered quizzes
}
