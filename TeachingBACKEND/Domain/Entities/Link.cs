namespace TeachingBACKEND.Domain.Entities
{
    public class Link
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Progress { get; set; }
        public LearnHub LearnHub { get; set; }
        public Guid LearnHubId { get; set; }
        public List<Quizz> Quizzes { get; set; }
    }
}
