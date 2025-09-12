using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;
using static TeachingBACKEND.Application.Services.PasswordValidationService;

namespace TeachingBACKEND.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly INotificationService _notificationService;
        private readonly IPasswordValidationService _passwordValidation;

        public PasswordService(ApplicationDbContext context, IConfiguration configuration, INotificationService notificationService, IPasswordValidationService passwordValidation)
        {
            _context = context;
            _notificationService = notificationService;
            _jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            _passwordValidation = passwordValidation;
        }

        public async Task RequestPasswordReset(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                var resetToken = Guid.NewGuid();
                user.PasswordResetToken = resetToken;
                user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

                await _context.SaveChangesAsync();

                // Determine the email address to send the reset email to
                string emailToSendTo = email;
                
                // If this is a child user (has a parent), send the reset email to the parent instead
                if (user.ParentUserId.HasValue)
                {
                    var parent = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.ParentUserId.Value);
                    if (parent != null)
                    {
                        emailToSendTo = parent.Email;
                        // Send a special notification to the parent about their child's password reset
                        await _notificationService.SendChildPasswordResetEmail(parent.Email, user.FirstName, user.LastName, resetToken);
                        return; // Exit early since we've sent the parent-specific email
                    }
                }

                //Send Email to the user directly (for non-child users)
                await _notificationService.SendPasswordResetEmail(emailToSendTo, resetToken);
            }
        }
        public async Task<string> ResetPassword(Guid token, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.PasswordResetTokenExpiry > DateTime.UtcNow);
            if (user == null)
            {
                return "Invalid or expired token";
            }

            //Check if new password matches the old one
            if(VerifyPassword(newPassword, user.PasswordHash))
            {
                return "New password must be different from the old one.";
            }

            if (!_passwordValidation.IsValid(newPassword, out var error))
                return error;

            user.PasswordHash = HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;

            await _context.SaveChangesAsync();
            return "Password reset successfully.";
        }
        public async Task<string> GeneratePasswordForApprovedSchool(Guid schoolId, string password)
        {
            var school = await _context.Users.FirstOrDefaultAsync(u =>
                u.Id == schoolId &&
                u.Role == UserRole.School &&
                u.ApprovalStatus == ApprovalStatus.Approved &&
                u.IsEmailVerified);

            if (school == null)
                return "School is not eligible to set a password.";

            if (!string.IsNullOrEmpty(school.PasswordHash))
            {
                if (VerifyPassword(password, school.PasswordHash))
                    return "New password must be different from the previous one.";

                return "Password has already been set.";
            }

            if (!_passwordValidation.IsValid(password, out var error))
                return error;

            school.PasswordHash = HashPassword(password);
            await _context.SaveChangesAsync();

            return "Password set successfully";
        }

        public async Task<string> VerifyEmail(Guid? token)
        {
            if (!token.HasValue)
            {
                return "Invalid verification token";
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token && u.EmailVerificationTokenExpiry > DateTime.UtcNow);

            if (user == null)
            {
                return "Ïnvalid or expired token";
            }

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiry = null;

            await _context.SaveChangesAsync();

            return "Email verified successfully.";
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = new JwtSecurityTokenHandler().CreateToken(descriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSecret)),
                ValidateLifetime = false
            };

            var handler = new JwtSecurityTokenHandler();
            var principal = handler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwt
                || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.Ordinal))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        public Guid GenerateRefreshToken()
        {
            var randomBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return new Guid(randomBytes);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public async Task<LoginResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? throw new SecurityTokenException("Invalid token claims");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            if (user == null
           || user.RefreshToken != model.RefreshToken
           || user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }

            //generate new tokens 
            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            //persist
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            await _context.SaveChangesAsync();


            return new LoginResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };

        }
        public Guid GenerateVerificationToken()
        {
            return Guid.NewGuid();
        }

        public string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@$!%*?&";
            var random = new Random();
            var password = new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
            // Ensure password meets complexity requirements
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || 
                !password.Any(char.IsDigit) || !password.Any(c => "@$!%*?&".Contains(c)))
            {
                // If it doesn't meet requirements, generate a new one
                return GenerateRandomPassword();
            }
            
            return password;
        }

        public async Task<string> ChangePassword(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return "User not found.";
            }

            // Verify current password
            if (!VerifyPassword(currentPassword, user.PasswordHash))
            {
                return "Current password is incorrect.";
            }

            // Check if new password is different from current password
            if (VerifyPassword(newPassword, user.PasswordHash))
            {
                return "New password must be different from the current password.";
            }

            // Validate new password
            if (!_passwordValidation.IsValid(newPassword, out var error))
                return error;

            // Update password
            user.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return "Password changed successfully.";
        }
    }
}
