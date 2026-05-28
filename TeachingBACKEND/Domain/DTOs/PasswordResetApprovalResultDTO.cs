namespace TeachingBACKEND.Domain.DTOs
{
    public class PasswordResetApprovalResultDTO
    {
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }
        public string? NewPassword { get; set; }
        public string? Message { get; set; }
    }
}
