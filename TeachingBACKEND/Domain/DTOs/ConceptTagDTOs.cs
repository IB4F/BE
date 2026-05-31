using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs;

public class ConceptTagDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateConceptTagDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class UserConceptMasteryDTO
{
    public Guid ConceptTagId { get; set; }
    public string? ConceptTagName { get; set; }
    public float MasteryLevel { get; set; }
    public int TotalAttempts { get; set; }
    public int CorrectAttempts { get; set; }
    public DateTime? NextReviewDate { get; set; }
    public DateTime LastAttemptAt { get; set; }
}
