namespace TeachingBACKEND.Domain.Entities;

public class OrphanedFile
{
    public Guid Id { get; set; }
    public Guid FileId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Reason { get; set; }
}
