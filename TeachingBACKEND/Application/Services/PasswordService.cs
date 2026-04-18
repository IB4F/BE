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
            _jwtSecret = configuration["JWT_SECRET_KEY"]
                ?? throw new InvalidOperationException("JWT_SECRET_KEY is not configured.");
            _passwordValidation = passwordValidation;
        }

        public async Task<string> RequestPasswordReset(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                // Check if there's already a pending password reset request
                if (user.PasswordResetToken.HasValue && user.PasswordResetTokenExpiry.HasValue && 
                    user.PasswordResetTokenExpiry > DateTime.UtcNow)
                {
                    // Check if this is a student created by supervisor
                    if (email.EndsWith("@bga.al") && user.SupervisorId.HasValue)
                    {
                        return "Supervizori juaj ka marrë tashmë kërkesën tuaj për rivendosjen e fjalëkalimit dhe do ta aprovojë së shpejti. Ju lutemi prisni përgjigjen e tyre.";
                    }
                    else
                    {
                        return "Një kërkesë për rivendosjen e fjalëkalimit është tashmë në pritje. Ju lutemi kontrolloni email-in tuaj ose prisni para se të bëni një kërkesë tjetër.";
                    }
                }

                var resetToken = Guid.NewGuid();
                user.PasswordResetToken = resetToken;
                user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

                await _context.SaveChangesAsync();

                // Check if this is a @bga.al email (student created by supervisor)
                if (email.EndsWith("@bga.al"))
                {
                    // Find the supervisor for this student
                    if (user.SupervisorId.HasValue)
                    {
                        var supervisor = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.SupervisorId.Value);
                        if (supervisor != null)
                        {
                            // Send notification to supervisor instead of student
                            await _notificationService.SendStudentPasswordResetRequestToSupervisor(
                                supervisor.Email, 
                                $"{supervisor.FirstName} {supervisor.LastName}", 
                                $"{user.FirstName} {user.LastName}", 
                                user.Email, 
                                resetToken);
                            return "Kërkesa për rivendosjen e fjalëkalimit është dërguar te supervizori juaj. Ai do ta aprovojë së shpejti.";
                        }
                    }
                }

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
                        return "Kërkesa për rivendosjen e fjalëkalimit është dërguar në adresën e email-it të prindit tuaj.";
                    }
                }

                //Send Email to the user directly (for non-child users)
                await _notificationService.SendPasswordResetEmail(emailToSendTo, resetToken);
                return "Lidhja për rivendosjen e fjalëkalimit është dërguar në adresën tuaj të email-it.";
            }
            
            return "Nëse ekziston një llogari me atë email, një lidhje për rivendosjen e fjalëkalimit është dërguar.";
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
                u.Role == UserRole.Supervisor &&
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
            var key = Encoding.UTF8.GetBytes(_jwtSecret);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.UTF8.GetBytes(_jwtSecret);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret)),
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

        // Store the hash in the DB — never the raw token.
        // SHA256 is sufficient for high-entropy random tokens (no brute-force risk).
        public Guid HashRefreshToken(Guid rawToken)
        {
            var hash = SHA256.HashData(rawToken.ToByteArray());
            return new Guid(hash[..16]);
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

            var incomingHash = HashRefreshToken(model.RefreshToken);

            if (user == null
           || user.RefreshToken != incomingHash
           || user.RefreshTokenExpiry <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid or expired refresh token");
            }

            //generate new tokens
            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();

            //persist — store hash, never the raw token
            user.RefreshToken = HashRefreshToken(newRefreshToken);
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
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

        private static readonly char[] PasswordChars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%*?&".ToCharArray();

        public string GenerateRandomPassword(int length = 12)
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[length];

            string password;
            do
            {
                rng.GetBytes(randomBytes);
                password = new string(randomBytes.Select(b => PasswordChars[b % PasswordChars.Length]).ToArray());
            }
            while (!password.Any(char.IsUpper) || !password.Any(char.IsLower) ||
                   !password.Any(char.IsDigit) || !password.Any(c => "!@#$%*?&".Contains(c)));

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
            
            // Clear the must change password flag if it was set
            if (user.MustChangePasswordOnNextLogin)
            {
                user.MustChangePasswordOnNextLogin = false;
            }
            
            await _context.SaveChangesAsync();

            return "Password changed successfully.";
        }
    }
}
