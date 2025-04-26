namespace TeachingBACKEND.Domain.DTOs
{
    public class RefreshTokenRequestDTO
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken {  get; set; }
    }
}
