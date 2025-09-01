namespace TeachingBACKEND.Domain.DTOs;

public class StudentQuizStartResponseDTO
{
    public Guid QuizId { get; set; }
    public DateTime StartedAt { get; set; }
    public string Message { get; set; } = "Quiz started successfully";
}
