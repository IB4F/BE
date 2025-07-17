using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Domain.DTOs
{
    public class FilteredLearnHubDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
        public int Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<LinkWithQuizzedDTO> Links { get; set; } = new();
    }
}