namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateQuizzDTO
    {
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public string Options { get; set; }
    }
}
