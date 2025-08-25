namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateQuizzDTO
    {
        public string Question { get; set; }
        public string? QuestionAudioId { get; set; }
        public string Explanation { get; set; }
        public string? ExplanationAudioId { get; set; }
        public int Points { get; set; }
        public List<OptionDTO> Options { get; set; }
        public string QuizType { get; set; }
        public string? ParentQuizId { get; set; } // Optional parent quiz ID for child quizzes
    }
}
