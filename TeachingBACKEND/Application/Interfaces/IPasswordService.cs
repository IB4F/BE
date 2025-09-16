using System.Security.Claims;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IPasswordService
    {
        Task<string> RequestPasswordReset(string email);
        Task<string> ResetPassword(Guid token, string newPassword);
        Task<string> GeneratePasswordForApprovedSchool(Guid schoolId, string password);
        Task<string> VerifyEmail(Guid? token);
        string GenerateJwtToken(User user);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Guid GenerateRefreshToken();
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashed);
        Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO model);
        Guid GenerateVerificationToken();
        string GenerateRandomPassword();
        Task<string> ChangePassword(Guid userId, string currentPassword, string newPassword);
    }
}
