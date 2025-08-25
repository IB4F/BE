using TeachingBACKEND.Domain.DTOs.Quizzes;

namespace TeachingBACKEND.Domain.DTOs.Quizzes
{
    public class ChildQuizDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public List<OptionTextDTO> Options { get; set; }
        public bool IsAnswered { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public string? QuestionAudioId { get; set; }
        public string? ExplanationAudioId { get; set; }
        public string QuizType { get; set; }
        public Guid? ParentQuizId { get; set; }
        // No ChildQuizzes property since child quizzes don't have children
    }
}
