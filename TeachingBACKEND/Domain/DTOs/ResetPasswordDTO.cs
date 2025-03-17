namespace TeachingBACKEND.Domain.DTOs
{
    public class ResetPasswordDTO
    {
        public Guid Token {  get; set; }
        public string NewPassword { get; set; }
    }
}
