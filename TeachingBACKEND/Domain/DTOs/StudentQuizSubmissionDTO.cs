namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionDTO
    {
        public Guid QuizId { get; set; }
        public string? AnswerId { get; set; } // For single answer quizzes (backward compatibility)
        public List<string>? AnswerIds { get; set; } // For multiple answer quizzes
        public DateTime? StartedAt { get; set; } // Optional: if not provided, will use the stored start time
    }
}
