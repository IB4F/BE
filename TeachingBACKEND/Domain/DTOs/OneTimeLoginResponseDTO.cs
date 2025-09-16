namespace TeachingBACKEND.Domain.DTOs
{
    public class OneTimeLoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool MustChangePassword { get; set; }
        public string Message { get; set; }
    }
}

