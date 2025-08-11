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
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
