using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;
using System.Text.Json;

namespace TeachingBACKEND.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _jwtSecret;
        private readonly INotificationService _notificationService;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<UserService> _logger;
        private readonly ISubscriptionService _subscriptionService;

        public UserService(
            ApplicationDbContext context,
            IConfiguration configuration,
            INotificationService notificationService,
            IPasswordService passwordService,
            ISubscriptionService subscriptionService,
            ILogger<UserService> logger)
        {
            _context = context;
            _notificationService = notificationService;
            _passwordService = passwordService;
            _logger = logger;
            _subscriptionService = subscriptionService;

        }

        /// <summary>
        /// Validates and converts class input to ensure it's always a valid class ID
        /// </summary>
        /// <param name="classInput">Either a class ID (Guid) or class name</param>
        /// <returns>The class ID as a string</returns>
        private async Task<string> ValidateAndGetClassId(string classInput)
        {
            if (string.IsNullOrWhiteSpace(classInput))
                throw new ArgumentException("Class cannot be null or empty");

            // First, try to parse as GUID (class ID)
            if (Guid.TryParse(classInput, out Guid classId))
            {
                // Verify the class ID exists in the database
                var classExists = await _context.Classes.AnyAsync(c => c.Id == classId);
                if (classExists)
                {
                    return classId.ToString();
                }
                throw new ArgumentException($"Class with ID '{classInput}' not found");
            }

            // If not a GUID, treat as class name and find the corresponding ID
            var classEntity = await _context.Classes.FirstOrDefaultAsync(c => c.Name == classInput);
            if (classEntity != null)
            {
                return classEntity.Id.ToString();
            }

            throw new ArgumentException($"Class '{classInput}' not found");
        }

        /// <summary>
        /// Converts a class ID to its corresponding class name
        /// </summary>
        /// <param name="classId">The class ID as a string</param>
        /// <returns>The class name, or null if not found</returns>
        private async Task<string?> GetClassNameById(string? classId)
        {
            if (string.IsNullOrWhiteSpace(classId) || !Guid.TryParse(classId, out Guid id))
                return null;

            var classEntity = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
            return classEntity?.Name;
        }

        // Note: RegisterStudent method removed - registration now handled by AuthController using SubscriptionService
        // Note: RegisterSchool method removed - registration now handled by AuthController using SubscriptionService

        //public async Task<UserResponseDTO> CreateStudentBySchool(CreateStudentBySchoolDTO model, Guid schoolId)
        //{
        //    // Verify the school exists and is approved
        //    var school = await _context.Users.FirstOrDefaultAsync(u => u.Id == schoolId && u.Role == UserRole.School);
        //    if (school == null)
        //    {
        //        throw new Exception("School not found");
        //    }

        //    if (school.ApprovalStatus != ApprovalStatus.Approved)
        //    {
        //        throw new Exception("School is not approved to create students");
        //    }

        //    // Check if student email already exists
        //    var existingStudent = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
        //    if (existingStudent != null)
        //    {
        //        throw new Exception("Student email already exists");
        //    }

        //    // Generate a random password for the student
        //    var studentPassword = _passwordService.GenerateRandomPassword();
        //    var verificationToken = _passwordService.GenerateVerificationToken();

        //    // Validate and ensure CurrentClass is always a class ID
        //    var validatedClassId = await ValidateAndGetClassId(model.CurrentClass);

        //    var student = new User
        //    {
        //        Email = model.Email,
        //        PasswordHash = _passwordService.HashPassword(studentPassword),
        //        Role = UserRole.Student,
        //        ApprovalStatus = ApprovalStatus.Approved, // Auto-approved since created by approved school
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        DateOfBirth = model.DateOfBirth,
        //        CurrentClass = validatedClassId,
        //        School = model.School,
        //        IsEmailVerified = false,
        //        EmailVerificationToken = verificationToken,
        //        EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24),
        //        ParentUserId = schoolId // Link to the school that created this student
        //    };

        //    _context.Users.Add(student);
        //    string sessionId;
        //    try
        //    {
        //        await _context.SaveChangesAsync();

        //        // Create a payment session for the student
        //        sessionId = await _paymentService.CreateCheckoutSessionAsync(new PaymentSessionRequestDTO
        //        {
        //            Email = model.Email,
        //            RegistrationType = "student",
        //            PlanId = model.PlanId,
        //        }, student.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.InnerException?.Message);
        //        throw;
        //    }

        //    // Send verification email with the generated password
        //    await _notificationService.SendStudentCreatedBySchoolEmail(model.Email, verificationToken, studentPassword, model.FirstName, model.LastName, "student");

        //    return new UserResponseDTO
        //    {
        //        Id = student.Id,
        //        Email = student.Email,
        //        FirstName = student.FirstName,
        //        LastName = student.LastName,
        //        Role = student.Role,
        //        ApprovalStatus = student.ApprovalStatus,
        //        School = student.School,
        //        SessionId = sessionId
        //    };
        //}

        // Note: RegisterFamily method removed - registration now handled by AuthController using SubscriptionService




        public async Task<bool> VerifyFamilyEmailAsync(Guid token)
        {
            var primaryUser = await _context.Users
                .FirstOrDefaultAsync(u => u.EmailVerificationToken == token && !u.IsEmailVerified);

            if (primaryUser == null)
                return false;

            primaryUser.IsEmailVerified = true;
            primaryUser.EmailVerificationToken = null;
            primaryUser.EmailVerificationTokenExpiry = null;

            var familyMembers = await _context.Users
                .Where(u => u.ParentUserId == primaryUser.Id && !u.IsEmailVerified)
                .ToListAsync();

            foreach (var member in familyMembers)
            {
                member.IsEmailVerified = true;
                // No token on family members, but clear if exists
                member.EmailVerificationToken = null;
                member.EmailVerificationTokenExpiry = null;
            }

            await _context.SaveChangesAsync();
            return true;
        }



        private string GenerateFamilyMemberEmail(int index, string primaryEmail)
        {
            var emailParts = primaryEmail.Split('@');
            return $"{emailParts[0]}+member{index}@{emailParts[1]}";
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
                Id =  entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CurrentClass = entity.CurrentClass,
                School = entity.School,
                PhoneNumber = entity.PhoneNumber,
                Profession = entity.Profession                
            };
        }

        public async Task<List<UserResponseDTO>> GetStudentsBySchool(Guid schoolId)
        {
            // Verify the school exists and is approved
            var school = await _context.Users.FirstOrDefaultAsync(u => u.Id == schoolId && u.Role == UserRole.School);
            if (school == null)
            {
                throw new Exception("School not found");
            }

            if (school.ApprovalStatus != ApprovalStatus.Approved)
            {
                throw new Exception("School is not approved");
            }

            var students = await _context.Users
                .Where(u => u.ParentUserId == schoolId && u.Role == UserRole.Student)
                .Select(u => new UserResponseDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role,
                    ApprovalStatus = u.ApprovalStatus,
                    School = u.School,
                    CurrentClass = u.CurrentClass,
                    DateOfBirth = u.DateOfBirth,
                    IsEmailVerified = u.IsEmailVerified
                })
                .ToListAsync();

            return students;
        }

        /// <summary>
        /// Gets all available classes with their IDs and names
        /// </summary>
        /// <returns>List of classes with ID and Name</returns>
        public async Task<List<object>> GetAvailableClasses()
        {
            var classes = await _context.Classes
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();
            
            return classes.Cast<object>().ToList();
        }

        // Note: Payment-First Registration Methods removed - registration now handled by AuthController using SubscriptionService
    }
}
