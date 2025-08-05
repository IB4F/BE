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
            await _notificationService.SendEmailVerification(model.Email, verificationToken, "student");


            return new UserResponseDTO
            {
                Id = student.Id,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Role = student.Role,
                ApprovalStatus = student.ApprovalStatus,
                School = student.School,
                SessionId = sessionId,
                VerificationType = "student"
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
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(school);
            string sessionId;
            try
            {
                await _context.SaveChangesAsync();

                // Create a payment session for the school
                sessionId = await _paymentService.CreateCheckoutSessionAsync(new PaymentSessionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "school",
                    PlanId = model.PlanId,
                }, school.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            // Send verification email
            await _notificationService.SendEmailVerification(model.Email, verificationToken, "school");

            return new UserResponseDTO
            {
                Id = school.Id,
                Email = school.Email,
                FirstName = school.FirstName,
                Role = school.Role,
                ApprovalStatus = school.ApprovalStatus,
                School = school.School,
                SessionId = sessionId,
                VerificationType = "school"
            };
        }

        public async Task<UserResponseDTO> CreateStudentBySchool(CreateStudentBySchoolDTO model, Guid schoolId)
        {
            // Verify the school exists and is approved
            var school = await _context.Users.FirstOrDefaultAsync(u => u.Id == schoolId && u.Role == UserRole.School);
            if (school == null)
            {
                throw new Exception("School not found");
            }

            if (school.ApprovalStatus != ApprovalStatus.Approved)
            {
                throw new Exception("School is not approved to create students");
            }

            // Check if student email already exists
            var existingStudent = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existingStudent != null)
            {
                throw new Exception("Student email already exists");
            }

            // Generate a random password for the student
            var studentPassword = _passwordService.GenerateRandomPassword();
            var verificationToken = _passwordService.GenerateVerificationToken();

            var student = new User
            {
                Email = model.Email,
                PasswordHash = _passwordService.HashPassword(studentPassword),
                Role = UserRole.Student,
                ApprovalStatus = ApprovalStatus.Approved, // Auto-approved since created by approved school
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                CurrentClass = model.CurrentClass,
                School = model.School,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24),
                ParentUserId = schoolId // Link to the school that created this student
            };

            _context.Users.Add(student);
            string sessionId;
            try
            {
                await _context.SaveChangesAsync();

                // Create a payment session for the student
                sessionId = await _paymentService.CreateCheckoutSessionAsync(new PaymentSessionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "student",
                    PlanId = model.PlanId,
                }, student.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            // Send verification email with the generated password
            await _notificationService.SendStudentCreatedBySchoolEmail(model.Email, verificationToken, studentPassword, model.FirstName, model.LastName, "student");

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

        public async Task<UserResponseDTO> RegisterFamily(FamilyRegistrationDTO model)
        {
            var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == model.Email.ToLower());
            if (existing != null)
                throw new Exception("Email already exists");

            var verificationToken = _passwordService.GenerateVerificationToken();
            var passwordHash = _passwordService.HashPassword(model.Password);

            var primaryUser = new User
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                Role = UserRole.Family,
                ApprovalStatus = ApprovalStatus.Pending,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.UtcNow.AddHours(24)
            };

            _context.Users.Add(primaryUser);
            await _context.SaveChangesAsync();

            foreach (var member in model.FamilyMembers)
            {
                // Base email without suffix
                string baseEmail = $"{member.FirstName.ToLower()}.{member.LastName.ToLower()}@bg.com";
                string generatedEmail = baseEmail;
                int suffix = 1;

                // Check if email exists, if yes, add suffix and keep checking
                while (await _context.Users.AnyAsync(u => u.Email == generatedEmail))
                {
                    generatedEmail = $"{member.FirstName.ToLower()}.{member.LastName.ToLower()}{suffix}@bg.com";
                    suffix++;
                }

                var familyMember = new User
                {
                    Email = generatedEmail,
                    PasswordHash = passwordHash,  // same hashed password
                    Role = UserRole.Family,
                    ApprovalStatus = ApprovalStatus.Pending,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IsEmailVerified = false,
                    EmailVerificationToken = null,
                    EmailVerificationTokenExpiry = null,
                    ParentUserId = primaryUser.Id,
                    CurrentClass = member.CurrentClass
                };

                _context.Users.Add(familyMember);
            }

            await _context.SaveChangesAsync();

            var sessionId = await _paymentService.CreateCheckoutSessionAsync(new PaymentSessionRequestDTO
            {
                Email = model.Email,
                PlanId = Guid.Parse(model.PlanId),
                RegistrationType = "family",
                FamilyMemberCount = model.FamilyMembers.Count + 1
            }, primaryUser.Id);

            var familyNames = model.FamilyMembers.Select(m => $"{m.FirstName} {m.LastName}").ToList();
            await _notificationService.SendFamilyEmailVerification(model.Email, verificationToken, familyNames, "family");

            return new UserResponseDTO
            {
                Id = primaryUser.Id,
                Email = primaryUser.Email,
                FirstName = primaryUser.FirstName,
                LastName = primaryUser.LastName,
                Role = primaryUser.Role,
                ApprovalStatus = primaryUser.ApprovalStatus,
                SessionId = sessionId,
                VerificationType = "family"
            };
        }




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
    }
}
