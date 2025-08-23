namespace TeachingBACKEND.Domain.DTOs.Quizzes
{
    public class SimpleQuizDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string QuizType { get; set; }
    }
}
