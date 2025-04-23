using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace TeachingBACKEND.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly INotificationService _notificationService;

        public UserService(ApplicationDbContext context, IConfiguration configuration, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
            _jwtSecret = configuration["Jwt:SecretKey"];
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }
        public Guid GenerateVerificationToken()
        {
            return Guid.NewGuid();
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public async Task<UserResponseDTO> RegisterStudent(StudentRegistrationDTO model)
        {
            var existingStudent = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingStudent != null)
            {
                throw new Exception("Email already exists");
            }

            var verificationToken = GenerateVerificationToken();

            var student = new User
            {
                Email = model.Email,
                PasswordHash = HashPassword(model.Password),
                Role = UserRole.Student,
                ApprovalStatus = ApprovalStatus.Pending,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CurrentClass = model.CurrentClass,
                School = model.School,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }


            //Send verification email
            await _notificationService.SendEmailVerification(model.Email, verificationToken);


            return new UserResponseDTO
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Role = student.Role,
                ApprovalStatus = student.ApprovalStatus,
                School = student.School
            };
        }
        public async Task<UserResponseDTO> RegisterSchool(SchoolRegistrationDTO model)
        {
            var existingSchool = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingSchool != null)
            {
                throw new Exception("School already exists in DB");
            }

            var verificationToken = GenerateVerificationToken();

            var school = new User
            {
                Email = model.Email,
                //PasswordHash = HashPassword(model.Password),
                Role = UserRole.School,
                ApprovalStatus = ApprovalStatus.Pending,
                FirstName = model.FirstName,
                PhoneNumber = model.PhoneNumber,
                Profession = model.Profession,
                City = model.City,
                PostalCode = model.PostalCode,
                School = model.School,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(school);
            await _context.SaveChangesAsync();


            //Send verification email
            await _notificationService.SendEmailVerification(model.Email, verificationToken);

            return new UserResponseDTO
            {
                Id = school.Id,
                Email = school.Email,
                FirstName = school.FirstName,
                Role = school.Role,
                ApprovalStatus = school.ApprovalStatus,
                School = school.School
            };
        }
        public async Task<LoginResponseDTO> Login(LoginDTO model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            if (!user.IsEmailVerified)
            {
                throw new Exception("Email is not verified! Please check your inbox.");
            }

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);

            await _context.SaveChangesAsync();

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        public async Task<string> VerifyEmail(Guid? token)
        {
            if (!token.HasValue)
            {
                return "Invalid verification token";
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token && u.EmailVerificationTokenExpiry > DateTime.UtcNow);

            if(user == null)
            {
                return "Ïnvalid or expired token";
            }

            user.IsEmailVerified = true;
            user.EmailVerificationToken = null; 
            user.EmailVerificationTokenExpiry = null;

            await _context.SaveChangesAsync();

            return "Email verified successfully.";
        }
        public async Task<string> RequestPasswordReset(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(user == null)
            {
                return "No account found with this email.";
            }


            var resetToken = Guid.NewGuid();
            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(1);

            await _context.SaveChangesAsync();

            //Send Email
            await _notificationService.SendPasswordResetEmail(email, resetToken);

            return "Password reset email sent successfully.";
        }
        public async Task<string> ResetPassword(Guid token, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.PasswordResetTokenExpiry > DateTime.UtcNow);
            if(user == null)
            {
                return "Invalid or expired token";
            }

            user.PasswordHash = HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiry = null;
             
            await _context.SaveChangesAsync();
            return "Password reset successfully.";
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }
        public async Task<User> GetUserById(Guid Id)
        {
           var user =  await _context.Users.FirstOrDefaultAsync(u => u.Id == Id);
            if (user == null) throw new Exception("User not found");
            return user;
        }
        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
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
        public Guid GenerateRefreshToken()
        {
            var randomBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return new Guid(randomBytes);
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
                return "Password has already been set.";


            school.PasswordHash = HashPassword(password);
            await _context.SaveChangesAsync();

            return "Password set successfully";
        }

        public async Task<string> Logout(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                throw new Exception("User not found");

            user.RefreshToken = Guid.Empty;
            user.RefreshTokenExpiry = DateTime.MinValue;

            await _context.SaveChangesAsync();

            return "User successfully logged out.";
        }
        
    }
}
