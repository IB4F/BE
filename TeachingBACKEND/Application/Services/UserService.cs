using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly INotificationService _notificationService;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<UserService> _logger;
        private readonly IPaymentService _paymentService;

        public UserService(
            ApplicationDbContext context,
            IConfiguration configuration,
            INotificationService notificationService,
            IPasswordService passwordService,
            IPaymentService paymentService,
            ILogger<UserService> logger)
        {
            _context = context;
            _notificationService = notificationService;
            _passwordService = passwordService;
            _logger = logger;
            _paymentService = paymentService;

        }

        public async Task<UserResponseDTO> RegisterStudent(StudentRegistrationDTO model)
        {

            //_logger.LogInformation("Registering new student: {Email}", model.Email);

            if (!Regex.IsMatch(model.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$"))
                throw new InvalidOperationException("Password does not meet complexity requirements.");

            var existingStudent = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingStudent != null)
            {
              //  _logger.LogWarning("Email already exists: {Email}", model.Email);
                throw new Exception("Email already exists");
            }

            var verificationToken = _passwordService.GenerateVerificationToken();

            var student = new User
            {
                Email = model.Email,
                PasswordHash = _passwordService.HashPassword(model.Password),
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
            string sessionId;
            try
            {
                await _context.SaveChangesAsync();
               // _logger.LogInformation("Student registered successfully: {Email}", model.Email);

                //Create a payment session
                sessionId = await _paymentService.CreateCheckoutSessionAsync(new PaymentSessionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "student",
                    PlanId = model.PlanId,
                }, student.Id);
            }
            catch (Exception ex)
            {
               // _logger.LogInformation(ex,"Failed to register student: {Email}", model.Email);
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
                School = student.School,
                SessionId = sessionId
            };
        }
        public async Task<UserResponseDTO> RegisterSchool(SchoolRegistrationDTO model)
        {
            var existingSchool = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingSchool != null)
            {
                throw new Exception("School already exists in DB");
            }

            var verificationToken = _passwordService.GenerateVerificationToken();

            var school = new User
            {
                Email = model.Email,
                Role = UserRole.School,
                ApprovalStatus = ApprovalStatus.Pending,
                FirstName = model.SchoolName,
                PhoneNumber = model.PhoneNumber,
                Profession = model.Profession,
                City = model.City,
                PostalCode = model.PostalCode,
                //School = model.School,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(school);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }



            //Send verification email
            await _notificationService.SendEmailVerification(model.Email, verificationToken);

            return new UserResponseDTO
            {
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
            if (user == null || !_passwordService.VerifyPassword(model.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            if (!user.IsEmailVerified)
            {
                throw new Exception("Email is not verified! Please check your inbox.");
            }

            var accessToken = _passwordService.GenerateJwtToken(user);
            var refreshToken = _passwordService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);

            await _context.SaveChangesAsync();

            return new LoginResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
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
        public async Task<UserDetails> GetUserDetails(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new Exception("Invalid or missing user ID in token.");
            }

            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (entity == null)
                throw new Exception("User not found");

            return new UserDetails
            {
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CurrentClass = entity.CurrentClass,
                School = entity.School,
                PhoneNumber = entity.PhoneNumber,
                Profession = entity.Profession                
            };
        }
    }
}
