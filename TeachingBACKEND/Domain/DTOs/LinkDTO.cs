using System.Text.Json.Serialization;

namespace TeachingBACKEND.Domain.DTOs
{
    public class LinkDTO
    {
        public Guid Id { get; set; }  
        public string Title { get; set; }
        public int QuizzesCount { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Status { get; set; } // "Not Started", "In Progress", "Completed"
    }
}
