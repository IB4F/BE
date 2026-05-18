namespace TeachingBACKEND.Domain.DTOs;

public class QuizResultDTO
{
    public string Question { get; set; }
    public bool IsCorrect { get; set; }
    public int PointsDelta { get; set; }
    public bool HasChildQuiz { get; set; }
}
