namespace TeachingBACKEND.Domain.DTOs
{
    public class PasswordResetRequestDTO
    {
        public Guid StudentId { get; set; }
        public string StudentEmail { get; set; } // firstname.lastname@bga.al
        public string StudentName { get; set; }
        public string Reason { get; set; }
        public DateTime RequestDate { get; set; }
        public Guid ResetToken { get; set; }
    }
}

