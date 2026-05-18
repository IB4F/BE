namespace TeachingBACKEND.Domain.Entities;

public class StudentQuizResult
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    public Guid LinkId { get; set; }
    public Link Link { get; set; }
    public Guid QuizId { get; set; }
    public Quizz Quiz { get; set; }
    public bool IsCorrect { get; set; }
    public int PointsDelta { get; set; }
    public bool HasChildQuiz { get; set; }
    public DateTime AttemptedAt { get; set; }
}
