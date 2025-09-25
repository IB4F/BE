using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Domain.DTOs
{
    public class FilteredLearnHubDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
        public string? RequiredTier { get; set; } // NEW: Required tier name (Basic, Standard, Premium)
        public List<LinkDTO> Links { get; set; }
        public int Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}