namespace TeachingBACKEND.Domain.DTOs
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
