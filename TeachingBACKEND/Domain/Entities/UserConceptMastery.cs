using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.Entities;

public class UserConceptMastery
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public Guid ConceptTagId { get; set; }
    public ConceptTag ConceptTag { get; set; } = null!;

    public float MasteryLevel { get; set; } = 0f;
    public int TotalAttempts { get; set; } = 0;
    public int CorrectAttempts { get; set; } = 0;
    public DateTime? NextReviewDate { get; set; }
    public DateTime LastAttemptAt { get; set; }
}
