using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Domain.DTOs
{
    public class GetSingleLearnHub
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
        public int? RequiredTier { get; set; } // NEW: Required tier value (1=Basic, 2=Standard, 3=Premium)
        public List<LinkDTO> Links { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Difficulty { get; set; }
    }
}
