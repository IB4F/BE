namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public int Points { get; set; }
        public List<StudentOptionDTO> Options { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public string QuizzTypeName { get; set; }
        public Guid? ParentQuizId { get; set; }
    }

    public class StudentOptionDTO
    {
        public string Id { get; set; }
        public string OptionText { get; set; }
        public string? OptionImageUrl { get; set; }
        // Note: IsCorrect is intentionally excluded for security
    }
}
