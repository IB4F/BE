namespace TeachingBACKEND.Domain.Entities;

public class ConceptTag
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Quizz> Quizzes { get; set; } = new List<Quizz>();
    public ICollection<UserConceptMastery> UserMasteries { get; set; } = new List<UserConceptMastery>();
}
