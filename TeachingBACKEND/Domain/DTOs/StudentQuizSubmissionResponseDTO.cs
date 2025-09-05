namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionResponseDTO
    {
        public bool Answer { get; set; }
        public string? ParentQuizId { get; set; } // ID of the next parent quiz (if moving to next parent)
        public string? ChildQuizId { get; set; } // ID of the child quiz (if moving to child quiz)
        public string? Explanation { get; set; } // Explanation shown when answer is incorrect
        public string? ExplanationAudioUrl { get; set; } // Audio explanation URL when answer is incorrect
    }
}
