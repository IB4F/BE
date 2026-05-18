namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionResponseDTO
    {
        public bool Answer { get; set; }
        public string? ParentQuizId { get; set; }
        public string? ChildQuizId { get; set; }
        public string? Explanation { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public StudentProgressSummaryDTO? Progress { get; set; }
    }
}
