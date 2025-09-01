namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionResponseDTO
    {
        public bool Answer { get; set; }
        public string? QuizId { get; set; } // ID of the next quiz to present (child quiz or next parent quiz)
        public string? Explanation { get; set; } // Explanation shown when answer is incorrect
        public string? ExplanationAudioUrl { get; set; } // Audio explanation URL when answer is incorrect
    }
}
