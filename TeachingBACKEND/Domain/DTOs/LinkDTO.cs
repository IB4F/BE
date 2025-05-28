namespace TeachingBACKEND.Domain.DTOs
{
    public class LinkDTO
    {
        public Guid Id { get; set; }  
        public Guid LearnHubId { get; set; }
        public string Title { get; set; }
        public double Progress { get; set; }
    }
}
