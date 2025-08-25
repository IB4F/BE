using TeachingBACKEND.Domain.DTOs.Quizzes;

namespace TeachingBACKEND.Domain.DTOs.Quizzes
{
    public class QuizDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public int Points { get; set; }
        public List<OptionTextDTO> Options { get; set; }
        public bool IsAnswered { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public string QuizzTypeName { get; set; }
        public Guid? ParentQuizId { get; set; } // Parent quiz ID if this is a child quiz
        public List<QuizDTO> ChildQuizzes { get; set; } = new List<QuizDTO>(); // Child quizzes if this is a parent
    }
}
