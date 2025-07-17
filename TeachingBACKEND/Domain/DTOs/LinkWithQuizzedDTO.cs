namespace TeachingBACKEND.Domain.DTOs
{
    public class LinkWithQuizzedDTO : LinkDTO
    {
        public List<GetQuizzDTO> Quizzes { get; set; } = new();
    }
}
