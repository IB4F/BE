namespace TeachingBACKEND.Domain.DTOs
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }   // internal only — never sent in HTTP body
        public bool RememberMe { get; set; }     // internal only — used by controller for cookie
        public bool IsFirstTimeLogin { get; set; }
        public bool MustChangePassword { get; set; }
        public bool RequiresTermsReAcceptance { get; set; }
    }
}
