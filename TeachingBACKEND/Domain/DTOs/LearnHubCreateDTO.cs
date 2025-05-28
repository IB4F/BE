namespace TeachingBACKEND.Domain.DTOs
{
    public class LearnHubCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ClassType { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }

    }
}
