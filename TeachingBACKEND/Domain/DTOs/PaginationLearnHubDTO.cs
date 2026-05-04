using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class PaginationLearnHubDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
        public PackageTier? RequiredTier { get; set; }
        public List<LinkDTO> Links { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
 