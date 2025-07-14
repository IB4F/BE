using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.Entities
{
    public class LearnHub
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
        public List<Link>Links {get; set;}
        [Range(1,10, ErrorMessage ="Difficulty must be between 1 and 10.")]
        public int Difficulty { get; set; } 
        public DateTime CreatedAt { get; set; }

    }
}
