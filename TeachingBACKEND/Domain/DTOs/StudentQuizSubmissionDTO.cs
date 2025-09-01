namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionDTO
    {
        public Guid QuizId { get; set; }
        public string AnswerId { get; set; }
        public DateTime? StartedAt { get; set; } // Optional: if not provided, will use the stored start time
    }
}
