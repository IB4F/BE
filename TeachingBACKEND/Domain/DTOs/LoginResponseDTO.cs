namespace TeachingBACKEND.Domain.DTOs
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
        public bool IsFirstTimeLogin { get; set; }
        public bool MustChangePassword { get; set; }
    }
}
