namespace TeachingBACKEND.Domain.DTOs
{
    public class SupervisorApprovalDTO
    {
        public Guid SupervisorId { get; set; }
        public bool IsApproved { get; set; }
        public string? RejectionReason { get; set; }
    }
}

