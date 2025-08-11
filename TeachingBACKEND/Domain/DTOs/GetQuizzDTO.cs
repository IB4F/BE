namespace TeachingBACKEND.Domain.DTOs
{
    public class GetQuizzDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public List<OptionDTO> Options { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AudioUrl { get; set; }
        public string ImageUrl { get; set; }
    }
}
