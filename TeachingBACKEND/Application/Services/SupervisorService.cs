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
                    Notes = string.IsNullOrWhiteSpace(model.Notes) ? null : model.Notes.Trim(),
                    DateOfBirth = model.DateOfBirth,
                    Role = UserRole.Student,
                    SupervisorId = supervisorId,
                    ApprovalStatus = ApprovalStatus.Approved,
                    IsEmailVerified = true,
                    MustChangePasswordOnNextLogin = true,
                    IsOneTimeLoginUsed = false,
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

        public async Task<PasswordResetApprovalResultDTO> HandlePasswordResetRequest(Guid studentId, bool approve, Guid supervisorId)
        {
            try
            {
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);

                if (student == null || student.SupervisorId != supervisorId)
                    throw new UnauthorizedAccessException("Student does not belong to this supervisor");

                var hasPendingRequest = student.PasswordResetToken.HasValue
                    && student.PasswordResetTokenExpiry > DateTime.UtcNow;

                if (!approve)
                {
                    if (hasPendingRequest)
                    {
                        student.PasswordResetToken = null;
                        student.PasswordResetTokenExpiry = null;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Password reset rejected for student {StudentId}", studentId);
                        return new PasswordResetApprovalResultDTO { Message = "Kërkesa u refuzua." };
                    }

                    return new PasswordResetApprovalResultDTO { Message = "Nuk ka kërkesa në pritje." };
                }

                // approve == true: generate new password regardless of pending request
                var newPassword = _passwordService.GenerateRandomPassword();
                student.PasswordHash = _passwordService.HashPassword(newPassword);
                student.MustChangePasswordOnNextLogin = true;
                student.IsOneTimeLoginUsed = false;
                student.PasswordResetToken = null;
                student.PasswordResetTokenExpiry = null;

                await _context.SaveChangesAsync();

                _logger.LogInformation("Password reset approved for student {StudentId} (proactive: {Proactive})",
                    studentId, !hasPendingRequest);

                return new PasswordResetApprovalResultDTO
                {
                    StudentName = $"{student.FirstName} {student.LastName}",
                    StudentEmail = student.Email,
                    NewPassword = newPassword
                };
            }
            catch (Exception ex) when (ex is not UnauthorizedAccessException)
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
                        Reason = "Harroi fjalëkalimin"
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
                var now = DateTime.UtcNow;
                var sevenDaysAgo = now.AddDays(-7);
                var threeDaysAgo = now.AddDays(-3);

                var students = await _context.Users
                    .AsNoTracking()
                    .Where(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student)
                    .ToListAsync();

                var studentIds = students.Select(s => s.Id).ToList();

                // Load performance summaries for status & activity calculations
                var perfSummaries = await _context.StudentPerformanceSummaries
                    .AsNoTracking()
                    .Where(s => studentIds.Contains(s.StudentId))
                    .Include(s => s.Link)
                        .ThenInclude(l => l.LearnHub)
                    .ToListAsync();

                var lastActivityByStudent = perfSummaries
                    .GroupBy(s => s.StudentId)
                    .ToDictionary(g => g.Key, g => g.Max(s => s.LastAttemptAt));

                // Progress
                var studentProgress = await _studentProgressService.CalculateStudentsProgressAsync(studentIds);
                var averageProgress = studentProgress.Values.Any() ? studentProgress.Values.Average() : 0.0;

                // Class names
                var classIds = students.Where(s => !string.IsNullOrEmpty(s.CurrentClass)).Select(s => s.CurrentClass).ToList();
                var classes = await _context.Classes
                    .AsNoTracking()
                    .Where(c => classIds.Contains(c.Id.ToString()))
                    .ToDictionaryAsync(c => c.Id.ToString(), c => c.Name);

                // Build student list with status
                var studentCredentials = students.Select(s =>
                {
                    var isNew = s.CreateAt >= sevenDaysAgo;
                    var isApproved = s.ApprovalStatus == ApprovalStatus.Approved;
                    var lastActivity = lastActivityByStudent.GetValueOrDefault(s.Id);
                    var isRecentlyActive = lastActivity.HasValue && lastActivity.Value >= threeDaysAgo;

                    var status = isNew ? "new"
                        : (isApproved && isRecentlyActive) ? "aktiv"
                        : "idle";

                    return new StudentCredentialsDTO
                    {
                        StudentId = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Email = s.Email,
                        Password = "***",
                        CurrentClass = !string.IsNullOrEmpty(s.CurrentClass) && classes.ContainsKey(s.CurrentClass)
                            ? classes[s.CurrentClass]
                            : s.CurrentClass ?? "N/A",
                        School = s.School,
                        DateOfBirth = s.DateOfBirth ?? DateTime.UtcNow,
                        IsActive = isApproved,
                        ProgressPercentage = studentProgress.GetValueOrDefault(s.Id, 0.0),
                        Status = status
                    };
                }).ToList();

                var newStudents = studentCredentials.Count(s => s.Status == "new");

                // Pending password resets
                var pendingPasswordResetRequests = await GetPendingPasswordResetRequests(supervisorId);

                // Weekly progress trend
                var weeklyTrend = await BuildWeeklyProgressTrend(studentIds, now);

                // Recent activities
                var recentActivities = await BuildRecentActivities(students, perfSummaries, lastActivityByStudent, sevenDaysAgo, threeDaysAgo, now);

                return new SupervisorDashboardDTO
                {
                    TotalStudents = students.Count,
                    ActiveStudents = studentCredentials.Count(s => s.IsActive),
                    NewStudents = newStudents,
                    AverageProgress = averageProgress,
                    PendingPasswordResetRequests = pendingPasswordResetRequests.Count,
                    WeeklyProgressTrend = weeklyTrend,
                    RecentActivities = recentActivities,
                    Students = studentCredentials
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data for supervisor {SupervisorId}", supervisorId);
                throw;
            }
        }

        private async Task<List<double?>> BuildWeeklyProgressTrend(List<Guid> studentIds, DateTime now)
        {
            if (studentIds.Count == 0)
                return Enumerable.Repeat<double?>(0.0, 7).ToList();

            var totalQuizzes = await _context.Quizzes.CountAsync();
            if (totalQuizzes == 0)
                return Enumerable.Repeat<double?>(0.0, 7).ToList();

            // Load all completion timestamps for these students (lightweight projection)
            var completions = await _context.StudentQuizPerformances
                .AsNoTracking()
                .Where(p => studentIds.Contains(p.StudentId))
                .Select(p => new { p.StudentId, p.CompletedAt })
                .ToListAsync();

            var trend = new List<double?>(7);
            double? lastValue = null;

            for (int i = 6; i >= 0; i--)
            {
                var dayEnd = now.Date.AddDays(-i + 1); // exclusive upper bound for day (today - i)
                var perStudentProgress = studentIds.Select(id =>
                {
                    var count = completions.Count(c => c.StudentId == id && c.CompletedAt < dayEnd);
                    return Math.Min(100.0, count * 100.0 / totalQuizzes);
                });

                var avg = perStudentProgress.Average();
                // Forward-fill: if no change, carry last value
                lastValue = Math.Round(avg, 1);
                trend.Add(lastValue);
            }

            return trend;
        }

        private async Task<List<SupervisorActivityDTO>> BuildRecentActivities(
            List<User> students,
            List<StudentPerformanceSummary> perfSummaries,
            Dictionary<Guid, DateTime?> lastActivityByStudent,
            DateTime sevenDaysAgo,
            DateTime threeDaysAgo,
            DateTime now)
        {
            var studentById = students.ToDictionary(s => s.Id);
            var activities = new List<(DateTime Timestamp, SupervisorActivityDTO Dto)>();

            // ok — module/link completions (CompletedQuizzes == TotalQuizzes) updated in last 7 days
            var completedSummaries = perfSummaries
                .Where(s => s.TotalQuizzes > 0
                    && s.CompletedQuizzes >= s.TotalQuizzes
                    && s.UpdatedAt >= sevenDaysAgo
                    && studentById.ContainsKey(s.StudentId))
                .GroupBy(s => s.StudentId)
                .Select(g => g.OrderByDescending(s => s.UpdatedAt).First());

            foreach (var summary in completedSummaries)
            {
                var student = studentById[summary.StudentId];
                var linkTitle = summary.Link?.Title ?? "";
                var hubTitle = summary.Link?.LearnHub?.Title ?? "";
                var moduleLabel = string.IsNullOrEmpty(hubTitle) ? linkTitle : $"{hubTitle} · {linkTitle}";
                var score = $"{summary.CorrectAnswerQuiz}/{summary.TotalQuizzes}";
                var raw = $"Përfundoi modulin '{moduleLabel}' me {score}";
                activities.Add((summary.UpdatedAt, new SupervisorActivityDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Type = "ok",
                    Description = raw.Length > 80 ? raw[..80] : raw,
                    TimeAgo = FormatTimeAgo(now - summary.UpdatedAt)
                }));
            }

            // warn — no activity for 3+ days (students who had activity but went quiet)
            var inactiveStudents = students
                .Where(s => lastActivityByStudent.TryGetValue(s.Id, out var last)
                    && last.HasValue && last.Value < threeDaysAgo);

            foreach (var student in inactiveStudents)
            {
                var last = lastActivityByStudent[student.Id]!.Value;
                var daysSince = (int)(now - last).TotalDays;
                var desc = $"Nuk ka aktivitet që prej {daysSince} ditësh — kontaktoni familjen";
                activities.Add((last, new SupervisorActivityDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Type = "warn",
                    Description = desc.Length > 80 ? desc[..80] : desc,
                    TimeAgo = FormatTimeAgo(now - last)
                }));
            }

            // default — new student registrations in last 7 days
            foreach (var student in students.Where(s => s.CreateAt >= sevenDaysAgo))
            {
                activities.Add((student.CreateAt, new SupervisorActivityDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Type = "default",
                    Description = "U regjistrua dhe filloi onboarding-un",
                    TimeAgo = FormatTimeAgo(now - student.CreateAt)
                }));
            }

            // info — reached 50%+ completion on a module for the first time (UpdatedAt recent)
            var milestoneSummaries = perfSummaries
                .Where(s => s.TotalQuizzes > 0
                    && (double)s.CompletedQuizzes / s.TotalQuizzes >= 0.5
                    && s.CompletedQuizzes < s.TotalQuizzes  // not fully completed (already captured above)
                    && s.UpdatedAt >= sevenDaysAgo
                    && studentById.ContainsKey(s.StudentId))
                .GroupBy(s => s.StudentId)
                .Select(g => g.OrderByDescending(s => s.UpdatedAt).First());

            foreach (var summary in milestoneSummaries)
            {
                var student = studentById[summary.StudentId];
                var pct = (int)Math.Round((double)summary.CompletedQuizzes / summary.TotalQuizzes * 100);
                var linkTitle = summary.Link?.Title ?? "";
                var raw = $"Ka arritur {pct}% të progresit në '{linkTitle}'";
                activities.Add((summary.UpdatedAt, new SupervisorActivityDTO
                {
                    StudentId = student.Id,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    Type = "info",
                    Description = raw.Length > 80 ? raw[..80] : raw,
                    TimeAgo = FormatTimeAgo(now - summary.UpdatedAt)
                }));
            }

            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(5)
                .Select(a => a.Dto)
                .ToList();
        }

        private static string FormatTimeAgo(TimeSpan elapsed)
        {
            if (elapsed.TotalMinutes < 60)
                return $"{(int)elapsed.TotalMinutes} min";
            if (elapsed.TotalHours < 24)
                return $"{(int)elapsed.TotalHours} orë";
            return $"{(int)elapsed.TotalDays} ditë";
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

        public async Task<UpdatedStudentDTO> UpdateStudentAsync(Guid studentId, Guid supervisorId, UpdateStudentBySupervisorDTO dto)
        {
            try
            {
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);

                if (student == null)
                    throw new KeyNotFoundException("Student not found");

                if (student.SupervisorId != supervisorId)
                    throw new UnauthorizedAccessException("Student does not belong to this supervisor");

                student.FirstName = dto.FirstName.Trim();
                student.LastName = dto.LastName.Trim();
                student.CurrentClass = dto.CurrentClass;
                student.DateOfBirth = DateTime.Parse(dto.DateOfBirth);
                student.Notes = string.IsNullOrWhiteSpace(dto.Notes) ? null : dto.Notes.Trim();

                await _context.SaveChangesAsync();

                _logger.LogInformation("Student {StudentId} updated by supervisor {SupervisorId}", studentId, supervisorId);

                return new UpdatedStudentDTO
                {
                    StudentId = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                    CurrentClass = student.CurrentClass,
                    School = student.School,
                    DateOfBirth = student.DateOfBirth ?? DateTime.UtcNow,
                    Notes = student.Notes
                };
            }
            catch (Exception ex) when (ex is not KeyNotFoundException && ex is not UnauthorizedAccessException)
            {
                _logger.LogError(ex, "Error updating student {StudentId} for supervisor {SupervisorId}", studentId, supervisorId);
                throw;
            }
        }

        public async Task<StudentProgressDetailDTO> GetStudentProgressDetail(Guid studentId)
        {
            try
            {
                var progressDetail = await _studentProgressService.GetStudentProgressDetailAsync(studentId);

                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);

                progressDetail.GeneratedPassword = null;
                progressDetail.Notes = student?.Notes;
                progressDetail.FirstName = student?.FirstName;
                progressDetail.LastName = student?.LastName;
                progressDetail.CurrentClass = student?.CurrentClass;
                progressDetail.DateOfBirth = student?.DateOfBirth?.ToString("O");

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
