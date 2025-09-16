using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class SupervisorService : ISupervisorService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;
        private readonly IPasswordService _passwordService;
        private readonly IStudentProgressService _studentProgressService;
        private readonly ILogger<SupervisorService> _logger;

        public SupervisorService(
            ApplicationDbContext context,
            INotificationService notificationService,
            IPasswordService passwordService,
            IStudentProgressService studentProgressService,
            ILogger<SupervisorService> logger)
        {
            _context = context;
            _notificationService = notificationService;
            _passwordService = passwordService;
            _studentProgressService = studentProgressService;
            _logger = logger;
        }

        public async Task<Guid> SubmitSupervisorApplication(SupervisorApplicationDTO model)
        {
            try
            {
                // Check if email already exists in applications or users
                var existingApplication = await _context.SupervisorApplications
                    .FirstOrDefaultAsync(sa => sa.ContactPersonEmail == model.ContactPersonEmail);
                
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.ContactPersonEmail);
                
                if (existingApplication != null || existingUser != null)
                {
                    throw new InvalidOperationException("Email already exists");
                }

                // Generate a temporary password that meets all requirements
                var tempPassword = GenerateSecurePassword(); // Generate a password that meets all requirements

                // Create supervisor application
                var supervisorApplication = new SupervisorApplication
                {
                    Id = Guid.NewGuid(),
                    SchoolName = model.SchoolName,
                    ContactPersonFirstName = model.ContactPersonFirstName,
                    ContactPersonLastName = model.ContactPersonLastName,
                    ContactPersonEmail = model.ContactPersonEmail,
                    ContactPersonPhone = model.ContactPersonPhone,
                    City = model.City,
                    Address = model.Address,
                    AdditionalInfo = model.AdditionalInfo,
                    ApprovalStatus = ApprovalStatus.Pending,
                    ApplicationDate = DateTime.UtcNow,
                    TemporaryPassword = tempPassword,
                    CreatedAt = DateTime.UtcNow
                };

                _context.SupervisorApplications.Add(supervisorApplication);
                await _context.SaveChangesAsync();

                // Send notification to admin
                await _notificationService.SendSupervisorApplicationNotification(
                    supervisorApplication.ContactPersonEmail, 
                    $"{supervisorApplication.ContactPersonFirstName} {supervisorApplication.ContactPersonLastName}", 
                    supervisorApplication.SchoolName);

                _logger.LogInformation("Supervisor application submitted for {Email}", model.ContactPersonEmail);
                return supervisorApplication.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting supervisor application for {Email}", model.ContactPersonEmail);
                throw;
            }
        }

        public async Task<bool> ApproveSupervisor(SupervisorApprovalDTO model)
        {
            try
            {
                var supervisorApplication = await _context.SupervisorApplications
                    .FirstOrDefaultAsync(sa => sa.Id == model.SupervisorId);

                if (supervisorApplication == null)
                {
                    throw new ArgumentException("Supervisor application not found");
                }

                if (model.IsApproved)
                {
                    // Update the application with approval details (but don't create user yet)
                    supervisorApplication.ApprovalStatus = ApprovalStatus.Approved;
                    supervisorApplication.ApprovalDate = DateTime.UtcNow;
                    supervisorApplication.UpdatedAt = DateTime.UtcNow;

                    // Send approval email with package selection link (no credentials yet)
                    var packageSelectionLink = $"https://braingainalbania.al/subscription-packages?supervisorApplicationId={supervisorApplication.Id}";
                    await _notificationService.SendSupervisorApprovalEmail(
                        supervisorApplication.ContactPersonEmail, 
                        $"{supervisorApplication.ContactPersonFirstName} {supervisorApplication.ContactPersonLastName}", 
                        packageSelectionLink);
                }
                else
                {
                    // Send rejection email first
                    await _notificationService.SendSupervisorRejectionEmail(
                        supervisorApplication.ContactPersonEmail, 
                        $"{supervisorApplication.ContactPersonFirstName} {supervisorApplication.ContactPersonLastName}", 
                        model.RejectionReason ?? "Aplikimi nuk plotëson kriteret e nevojshme.");
                    
                    // Update application with rejection details
                    supervisorApplication.ApprovalStatus = ApprovalStatus.Rejected;
                    supervisorApplication.ApprovalDate = DateTime.UtcNow;
                    supervisorApplication.RejectionReason = model.RejectionReason;
                    supervisorApplication.UpdatedAt = DateTime.UtcNow;
                    
                    // Delete the supervisor application from the database
                    _context.SupervisorApplications.Remove(supervisorApplication);
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Supervisor application {ApplicationId} approval status set to {IsApproved}", 
                    model.SupervisorId, model.IsApproved);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving supervisor application {ApplicationId}", model.SupervisorId);
                throw;
            }
        }

        public async Task<List<SupervisorApplicationDTO>> GetPendingSupervisorApplications()
        {
            var applications = await _context.SupervisorApplications
                .Where(sa => sa.ApprovalStatus == ApprovalStatus.Pending)
                .Select(sa => new SupervisorApplicationDTO
                {
                    SupervisorId = sa.Id,
                    SchoolName = sa.SchoolName,
                    ContactPersonFirstName = sa.ContactPersonFirstName,
                    ContactPersonLastName = sa.ContactPersonLastName,
                    ContactPersonEmail = sa.ContactPersonEmail,
                    ContactPersonPhone = sa.ContactPersonPhone,
                    City = sa.City,
                    Address = sa.Address ?? "",
                    AdditionalInfo = sa.AdditionalInfo ?? ""
                })
                .ToListAsync();

            return applications;
        }

        public async Task<StudentCreatedResponseDTO> CreateStudent(CreateStudentBySupervisorDTO model, Guid supervisorId)
        {
            try
            {
                // Verify supervisor exists and is approved
                var supervisor = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == supervisorId && 
                                            u.Role == UserRole.Supervisor && 
                                            u.ApprovalStatus == ApprovalStatus.Approved);

                if (supervisor == null)
                {
                    throw new ArgumentException("Supervisor not found or not approved");
                }

                // Check student limit
                var currentStudentCount = await _context.Users
                    .CountAsync(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student);

                // Get supervisor's subscription package to check limits
                var subscription = await _context.Subscriptions
                    .Include(s => s.SubscriptionPackage)
                    .FirstOrDefaultAsync(s => s.UserId == supervisorId && s.Status == SubscriptionStatus.Active);

                if (subscription != null && currentStudentCount >= subscription.SubscriptionPackage.MaxUsers)
                {
                    throw new InvalidOperationException("Student limit reached for this supervisor");
                }

                // Generate unique email
                var generatedEmail = GenerateUniqueStudentEmail(model.FirstName, model.LastName);
                var generatedPassword = GenerateSecurePassword();

                // Create student
                var student = new User
                {
                    Id = Guid.NewGuid(),
                    Email = generatedEmail,
                    PasswordHash = _passwordService.HashPassword(generatedPassword),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CurrentClass = model.CurrentClass,
                    School = model.School,
                    DateOfBirth = model.DateOfBirth,
                    Role = UserRole.Student,
                    SupervisorId = supervisorId,
                    ApprovalStatus = ApprovalStatus.Approved,
                    IsEmailVerified = true,
                    MustChangePasswordOnNextLogin = true,
                    IsOneTimeLoginUsed = false,
                    OriginalGeneratedPassword = generatedPassword, // Store original password
                    CreateAt = DateTime.UtcNow
                };

                _context.Users.Add(student);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Student created for supervisor {SupervisorId}: {StudentEmail}", 
                    supervisorId, generatedEmail);

                return new StudentCreatedResponseDTO
                {
                    StudentId = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    GeneratedEmail = generatedEmail,
                    GeneratedPassword = generatedPassword,
                    CurrentClass = student.CurrentClass,
                    School = student.School,
                    DateOfBirth = student.DateOfBirth ?? DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student for supervisor {SupervisorId}", supervisorId);
                throw;
            }
        }

        public async Task<List<StudentCreatedResponseDTO>> GetSupervisedStudents(Guid supervisorId)
        {
            var students = await _context.Users
                .AsNoTracking()
                .Where(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student)
                .ToListAsync();

            // Get all class names for the students
            var classIds = students.Where(s => !string.IsNullOrEmpty(s.CurrentClass)).Select(s => s.CurrentClass).ToList();
            var classes = await _context.Classes
                .AsNoTracking()
                .Where(c => classIds.Contains(c.Id.ToString()))
                .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

            var studentCredentials = students.Select(u => new StudentCreatedResponseDTO
            {
                StudentId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                GeneratedEmail = u.Email,
                GeneratedPassword = "***", // Don't return actual password for security
                CurrentClass = !string.IsNullOrEmpty(u.CurrentClass) && classes.ContainsKey(u.CurrentClass) 
                    ? classes[u.CurrentClass] 
                    : u.CurrentClass ?? "N/A", // Return class name or fallback to ID or N/A
                School = u.School,
                DateOfBirth = u.DateOfBirth ?? DateTime.UtcNow
            }).ToList();

            return studentCredentials;
        }

        public async Task<PaginatedResponseDTO<StudentCreatedResponseDTO>> GetSupervisedStudentsPaged(Guid supervisorId, int page, int pageSize)
        {
            var query = _context.Users
                .AsNoTracking()
                .Where(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student);

            var totalCount = await query.CountAsync();
            var students = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Get all class names for the students
            var classIds = students.Where(s => !string.IsNullOrEmpty(s.CurrentClass)).Select(s => s.CurrentClass).ToList();
            var classes = await _context.Classes
                .AsNoTracking()
                .Where(c => classIds.Contains(c.Id.ToString()))
                .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

            var studentCredentials = students.Select(u => new StudentCreatedResponseDTO
            {
                StudentId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                GeneratedEmail = u.Email,
                GeneratedPassword = "***", // Don't return actual password for security
                CurrentClass = !string.IsNullOrEmpty(u.CurrentClass) && classes.ContainsKey(u.CurrentClass) 
                    ? classes[u.CurrentClass] 
                    : u.CurrentClass ?? "N/A", // Return class name or fallback to ID or N/A
                School = u.School,
                DateOfBirth = u.DateOfBirth ?? DateTime.UtcNow
            }).ToList();

            return new PaginatedResponseDTO<StudentCreatedResponseDTO>
            {
                Data = studentCredentials,
                Pagination = new PaginationDTO
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalCount = totalCount
                }
            };
        }

        public async Task<bool> HandlePasswordResetRequest(Guid studentId, bool approve)
        {
            try
            {
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);

                if (student == null)
                {
                    throw new ArgumentException("Student not found");
                }

                if (approve)
                {
                    // Generate new password
                    var newPassword = GenerateStudentPassword();
                    student.PasswordHash = _passwordService.HashPassword(newPassword);
                    student.MustChangePasswordOnNextLogin = true;
                    student.IsOneTimeLoginUsed = false;
                    
                    // Store the original generated password (like when creating a student)
                    student.OriginalGeneratedPassword = newPassword;
                    
                    // Clear the password reset token since it's been handled
                    student.PasswordResetToken = null;
                    student.PasswordResetTokenExpiry = null;
                }
                else
                {
                    // If rejected, clear the password reset token
                    student.PasswordResetToken = null;
                    student.PasswordResetTokenExpiry = null;
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation("Password reset request for student {StudentId} {Status}", 
                    studentId, approve ? "approved" : "rejected");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling password reset request for student {StudentId}", studentId);
                throw;
            }
        }


        public async Task<List<PasswordResetRequestDTO>> GetPendingPasswordResetRequests(Guid supervisorId)
        {
            try
            {
                // Get all students under this supervisor who have pending password reset requests
                var studentsWithPendingResets = await _context.Users
                    .Where(u => u.SupervisorId == supervisorId && 
                               u.Role == UserRole.Student &&
                               u.PasswordResetToken.HasValue &&
                               u.PasswordResetTokenExpiry > DateTime.UtcNow)
                    .Select(u => new PasswordResetRequestDTO
                    {
                        StudentId = u.Id,
                        StudentName = $"{u.FirstName} {u.LastName}",
                        StudentEmail = u.Email,
                        RequestDate = u.PasswordResetTokenExpiry.Value.AddHours(-1), // Approximate request time
                        ResetToken = u.PasswordResetToken.Value,
                        Reason = "Student requested password reset"
                    })
                    .ToListAsync();

                _logger.LogInformation("Found {Count} pending password reset requests for supervisor {SupervisorId}", 
                    studentsWithPendingResets.Count, supervisorId);

                return studentsWithPendingResets;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending password reset requests for supervisor {SupervisorId}", supervisorId);
                throw;
            }
        }

        public async Task<SupervisorDashboardDTO> GetDashboardData(Guid supervisorId)
        {
            try
            {
                var students = await _context.Users
                    .AsNoTracking()
                    .Where(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student)
                    .ToListAsync();

                var totalStudents = students.Count;
                var activeStudents = students.Count(s => s.ApprovalStatus == ApprovalStatus.Approved);

                // Get real progress data for all students
                var studentIds = students.Select(s => s.Id).ToList();
                var studentProgress = await _studentProgressService.CalculateStudentsProgressAsync(studentIds);

                // Calculate average progress from real data
                var averageProgress = studentProgress.Values.Any() ? studentProgress.Values.Average() : 0.0;

                // Get all class names for the students
                var classIds = students.Where(s => !string.IsNullOrEmpty(s.CurrentClass)).Select(s => s.CurrentClass).ToList();
                var classes = await _context.Classes
                    .AsNoTracking()
                    .Where(c => classIds.Contains(c.Id.ToString()))
                    .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

                var studentCredentials = students.Select(s => new StudentCredentialsDTO
                {
                    StudentId = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Password = "***", // Don't return actual password for security
                    CurrentClass = !string.IsNullOrEmpty(s.CurrentClass) && classes.ContainsKey(s.CurrentClass) 
                        ? classes[s.CurrentClass] 
                        : s.CurrentClass ?? "N/A", // Return class name or fallback to ID or N/A
                    School = s.School,
                    DateOfBirth = s.DateOfBirth ?? DateTime.UtcNow,
                    IsActive = s.ApprovalStatus == ApprovalStatus.Approved,
                    ProgressPercentage = studentProgress.GetValueOrDefault(s.Id, 0.0) // Real progress calculation
                }).ToList();

                // Get the actual count of pending password reset requests
                var pendingPasswordResetRequests = await GetPendingPasswordResetRequests(supervisorId);

                return new SupervisorDashboardDTO
                {
                    TotalStudents = totalStudents,
                    ActiveStudents = activeStudents,
                    AverageProgress = averageProgress,
                    PendingPasswordResetRequests = pendingPasswordResetRequests.Count,
                    Students = studentCredentials
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data for supervisor {SupervisorId}", supervisorId);
                throw;
            }
        }

        public async Task<bool> IsSupervisorApproved(Guid supervisorId)
        {
            var supervisor = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == supervisorId && u.Role == UserRole.Supervisor);

            return supervisor?.ApprovalStatus == ApprovalStatus.Approved;
        }

        public async Task<User> CreateSupervisorFromApprovedApplicationAsync(Guid supervisorApplicationId)
        {
            try
            {
                var supervisorApplication = await _context.SupervisorApplications
                    .FirstOrDefaultAsync(sa => sa.Id == supervisorApplicationId && sa.ApprovalStatus == ApprovalStatus.Approved);

                if (supervisorApplication == null)
                {
                    throw new ArgumentException("Approved supervisor application not found");
                }

                // Check if user already exists
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == supervisorApplication.ContactPersonEmail);
                
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User with this email already exists");
                }

                // Create the supervisor user
                var tempPassword = supervisorApplication.TemporaryPassword ?? GenerateSecurePassword();
                var tempPasswordHash = _passwordService.HashPassword(tempPassword);

                var supervisor = new User
                {
                    Id = Guid.NewGuid(),
                    Email = supervisorApplication.ContactPersonEmail,
                    FirstName = supervisorApplication.ContactPersonFirstName,
                    LastName = supervisorApplication.ContactPersonLastName,
                    School = supervisorApplication.SchoolName,
                    City = supervisorApplication.City,
                    PhoneNumber = supervisorApplication.ContactPersonPhone,
                    Role = UserRole.Supervisor,
                    ApprovalStatus = ApprovalStatus.Approved,
                    IsEmailVerified = true,
                    PasswordHash = tempPasswordHash,
                    CreateAt = DateTime.UtcNow
                };

                _context.Users.Add(supervisor);
                await _context.SaveChangesAsync();

                // Update the application with the created user ID
                supervisorApplication.ApprovedUserId = supervisor.Id;
                supervisorApplication.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                // Send credentials email
                await _notificationService.SendSupervisorCredentialsEmail(
                    supervisor.Email,
                    $"{supervisor.FirstName} {supervisor.LastName}",
                    tempPassword);

                _logger.LogInformation("Created supervisor user {UserId} from approved application {ApplicationId}", 
                    supervisor.Id, supervisorApplicationId);

                return supervisor;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating supervisor from approved application {ApplicationId}", supervisorApplicationId);
                throw;
            }
        }

        public async Task<int> GetStudentCount(Guid supervisorId)
        {
            return await _context.Users
                .AsNoTracking()
                .CountAsync(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student);
        }

        public async Task<bool> CanCreateMoreStudents(Guid supervisorId)
        {
            var currentCount = await GetStudentCount(supervisorId);
            
            var subscription = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .FirstOrDefaultAsync(s => s.UserId == supervisorId && s.Status == SubscriptionStatus.Active);

            if (subscription == null)
                return false;

            return currentCount < subscription.SubscriptionPackage.MaxUsers;
        }

        public async Task<bool> DeleteStudent(Guid studentId, Guid supervisorId)
        {
            try
            {
                // Verify the student exists and belongs to this supervisor
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && 
                                            u.SupervisorId == supervisorId && 
                                            u.Role == UserRole.Student);

                if (student == null)
                {
                    throw new ArgumentException("Student not found or not under this supervisor");
                }

                // Remove the student from the database
                _context.Users.Remove(student);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Student {StudentId} deleted by supervisor {SupervisorId}", 
                    studentId, supervisorId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student {StudentId} for supervisor {SupervisorId}", 
                    studentId, supervisorId);
                throw;
            }
        }

        public async Task<StudentProgressDetailDTO> GetStudentProgressDetail(Guid studentId)
        {
            try
            {
                // Get the progress detail from the student progress service
                var progressDetail = await _studentProgressService.GetStudentProgressDetailAsync(studentId);
                
                // Get the student to check if they have logged in for the first time
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);
                
                if (student != null && student.SupervisorId.HasValue && !student.IsOneTimeLoginUsed && !string.IsNullOrEmpty(student.OriginalGeneratedPassword))
                {
                    // If the student is under a supervisor, hasn't logged in yet, and we have the original password, include it
                    progressDetail.GeneratedPassword = student.OriginalGeneratedPassword;
                }
                else
                {
                    // Student has already logged in, is not under a supervisor, or no original password available, don't include password
                    progressDetail.GeneratedPassword = null;
                }
                
                return progressDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting progress detail for student {StudentId}", studentId);
                throw;
            }
        }


        // Utility Methods
        private string GenerateStudentEmail(string firstName, string lastName)
        {
            var cleanFirstName = firstName.ToLower().Replace(" ", "");
            var cleanLastName = lastName.ToLower().Replace(" ", "");
            return $"{cleanFirstName}.{cleanLastName}@bga.al";
        }

        private string GenerateUniqueStudentEmail(string firstName, string lastName)
        {
            var baseEmail = GenerateStudentEmail(firstName, lastName);
            var email = baseEmail;
            var counter = 1;

            while (_context.Users.Any(u => u.Email == email))
            {
                email = $"{baseEmail.Split('@')[0]}{counter}@bga.al";
                counter++;
            }

            return email;
        }

        private string GenerateStudentPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateSecurePassword()
        {
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string special = "!@#$%^&*";
            
            var random = new Random();
            var password = new List<char>();
            
            // Ensure at least one character from each required category
            password.Add(lowercase[random.Next(lowercase.Length)]);
            password.Add(uppercase[random.Next(uppercase.Length)]);
            password.Add(numbers[random.Next(numbers.Length)]);
            password.Add(special[random.Next(special.Length)]);
            
            // Fill the remaining 8 characters with any character from all categories
            const string allChars = lowercase + uppercase + numbers + special;
            for (int i = 0; i < 8; i++)
            {
                password.Add(allChars[random.Next(allChars.Length)]);
            }
            
            // Shuffle the password to randomize the position of required characters
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}
