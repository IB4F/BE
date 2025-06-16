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

        public DateTime CreatedAt { get; set; }
    }
}
