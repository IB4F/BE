using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public AdminDashboardService(
            ApplicationDbContext context,
            IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _context = context;
            _contextFactory = contextFactory;
        }

        // ── Public interface methods ──────────────────────────────────────────

        public async Task<AdminDashboardDTO> GetDashboardOverviewAsync()
        {
            await using var ctx1 = await _contextFactory.CreateDbContextAsync();
            await using var ctx2 = await _contextFactory.CreateDbContextAsync();
            await using var ctx3 = await _contextFactory.CreateDbContextAsync();
            await using var ctx4 = await _contextFactory.CreateDbContextAsync();
            await using var ctx5 = await _contextFactory.CreateDbContextAsync();
            await using var ctx6 = await _contextFactory.CreateDbContextAsync();

            var statsTask          = GetAdminStatsInternalAsync(ctx1);
            var activitiesTask     = GetRecentActivitiesInternalAsync(ctx2, 10);
            var registrationTask   = GetUserRegistrationStatsInternalAsync(ctx3);
            var learnHubTask       = GetLearnHubStatsInternalAsync(ctx4);
            var quizTask           = GetQuizStatsInternalAsync(ctx5);
            var subscriptionTask   = GetSubscriptionStatsInternalAsync(ctx6);

            await Task.WhenAll(statsTask, activitiesTask, registrationTask, learnHubTask, quizTask, subscriptionTask);

            return new AdminDashboardDTO
            {
                Stats              = statsTask.Result,
                RecentActivities   = activitiesTask.Result,
                RegistrationStats  = registrationTask.Result,
                LearnHubStats      = learnHubTask.Result,
                QuizStats          = quizTask.Result,
                SubscriptionStats  = subscriptionTask.Result
            };
        }

        public Task<AdminStatsDTO> GetAdminStatsAsync()
            => GetAdminStatsInternalAsync(_context);

        public Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int limit = 10)
            => GetRecentActivitiesInternalAsync(_context, limit);

        public Task<List<UserRegistrationStatsDTO>> GetUserRegistrationStatsAsync()
            => GetUserRegistrationStatsInternalAsync(_context);

        public Task<List<LearnHubStatsDTO>> GetLearnHubStatsAsync()
            => GetLearnHubStatsInternalAsync(_context);

        public Task<List<QuizStatsDTO>> GetQuizStatsAsync()
            => GetQuizStatsInternalAsync(_context);

        public Task<List<SubscriptionStatsDTO>> GetSubscriptionStatsAsync()
            => GetSubscriptionStatsInternalAsync(_context);

        public async Task<AdminAnalyticsDTO> GetAnalyticsAsync()
        {
            await using var ctx1 = await _contextFactory.CreateDbContextAsync();
            await using var ctx2 = await _contextFactory.CreateDbContextAsync();
            await using var ctx3 = await _contextFactory.CreateDbContextAsync();
            await using var ctx4 = await _contextFactory.CreateDbContextAsync();
            await using var ctx5 = await _contextFactory.CreateDbContextAsync();

            var monthlyTask    = GetMonthlyRevenueInternalAsync(ctx1, 12);
            var growthTask     = GetUserGrowthInternalAsync(ctx2, 30);
            var topHubsTask    = GetTopPerformingLearnHubsInternalAsync(ctx3, 10);
            var challengeTask  = GetMostChallengingQuizzesInternalAsync(ctx4, 10);
            var geoTask        = GetGeographicDistributionInternalAsync(ctx5);

            await Task.WhenAll(monthlyTask, growthTask, topHubsTask, challengeTask, geoTask);

            return new AdminAnalyticsDTO
            {
                MonthlyRevenue          = monthlyTask.Result,
                UserGrowth              = growthTask.Result,
                TopPerformingLearnHubs  = topHubsTask.Result,
                MostChallengingQuizzes  = challengeTask.Result,
                GeographicDistribution  = geoTask.Result
            };
        }

        public Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueAsync(int months = 12)
            => GetMonthlyRevenueInternalAsync(_context, months);

        public Task<List<UserGrowthDTO>> GetUserGrowthAsync(int days = 30)
            => GetUserGrowthInternalAsync(_context, days);

        public Task<List<LearnHubPerformanceDTO>> GetTopPerformingLearnHubsAsync(int limit = 10)
            => GetTopPerformingLearnHubsInternalAsync(_context, limit);

        public Task<List<QuizPerformanceDTO>> GetMostChallengingQuizzesAsync(int limit = 10)
            => GetMostChallengingQuizzesInternalAsync(_context, limit);

        public Task<List<GeographicStatsDTO>> GetGeographicDistributionAsync()
            => GetGeographicDistributionInternalAsync(_context);

        public Task<Dictionary<string, object>> GetSystemHealthAsync()
            => GetSystemHealthInternalAsync(_context);

        // ── Private internal implementations ──────────────────────────────────

        private async Task<AdminStatsDTO> GetAdminStatsInternalAsync(ApplicationDbContext db)
        {
            // One query for all user counts instead of 6 separate round-trips
            var userSummary = await db.Users
                .AsNoTracking()
                .Select(u => new { u.Role, u.ApprovalStatus })
                .ToListAsync();

            var activeLearnHubs = await db.LearnHubs.CountAsync();
            var completedQuizzes = await db.StudentQuizPerformances
                .Where(sqp => sqp.IsCompleted)
                .CountAsync();
            var totalRevenue = await db.SubscriptionPayments
                .Where(sp => sp.Status == PaymentStatus.Succeeded)
                .SumAsync(sp => (long?)sp.Amount) ?? 0L;
            var activeSubscriptions = await db.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active)
                .CountAsync();

            return new AdminStatsDTO
            {
                ActiveLearnHubs        = activeLearnHubs,
                TotalRegisteredUsers   = userSummary.Count,
                CompletedQuizzes       = completedQuizzes,
                ActiveAdministrators   = userSummary.Count(u => u.Role == UserRole.Admin),
                TotalSchools           = userSummary.Count(u => u.Role == UserRole.Supervisor),
                TotalStudents          = userSummary.Count(u => u.Role == UserRole.Student),
                TotalFamilies          = userSummary.Count(u => u.Role == UserRole.Family),
                PendingApprovals       = userSummary.Count(u => u.ApprovalStatus == ApprovalStatus.Pending),
                TotalRevenue           = totalRevenue / 100m,
                ActiveSubscriptions    = activeSubscriptions
            };
        }

        private async Task<List<RecentActivityDTO>> GetRecentActivitiesInternalAsync(ApplicationDbContext db, int limit)
        {
            var recentLearnHubs = await db.LearnHubs
                .AsNoTracking()
                .OrderByDescending(lh => lh.CreatedAt)
                .Take(5)
                .ToListAsync();

            var learnHubActivities = recentLearnHubs.Select(lh => new RecentActivityDTO
            {
                Id          = lh.Id,
                Type        = "learnhub_created",
                Description = $"LearnHub '{lh.Title}' è stato creato",
                UserName    = "Sistema",
                UserRole    = "Admin",
                Timestamp   = lh.CreatedAt,
                Icon        = "book",
                Metadata    = new Dictionary<string, object>
                {
                    ["learnHubId"] = lh.Id,
                    ["title"]      = lh.Title,
                    ["subject"]    = lh.Subject
                }
            }).ToList();

            var recentUsers = await db.Users
                .AsNoTracking()
                .OrderByDescending(u => u.CreateAt)
                .Take(5)
                .ToListAsync();

            var userActivities = recentUsers.Select(u => new RecentActivityDTO
            {
                Id          = u.Id,
                Type        = "user_registered",
                Description = $"Nuovo utente registrato: {u.FirstName} {u.LastName}",
                UserName    = $"{u.FirstName} {u.LastName}",
                UserRole    = u.Role.ToString(),
                Timestamp   = u.CreateAt,
                Icon        = "user-plus",
                Metadata    = new Dictionary<string, object>
                {
                    ["userId"] = u.Id,
                    ["email"]  = u.Email,
                    ["role"]   = u.Role.ToString()
                }
            }).ToList();

            var recentQuizCompletions = await db.StudentQuizPerformances
                .AsNoTracking()
                .Where(sqp => sqp.IsCompleted)
                .Include(sqp => sqp.Student)
                .Include(sqp => sqp.Quiz)
                    .ThenInclude(q => q.Link)
                        .ThenInclude(l => l.LearnHub)
                .OrderByDescending(sqp => sqp.CompletedAt)
                .Take(5)
                .ToListAsync();

            var quizActivities = recentQuizCompletions.Select(sqp => new RecentActivityDTO
            {
                Id          = sqp.Id,
                Type        = "quiz_completed",
                Description = $"Quiz completato in '{sqp.Quiz.Link.LearnHub.Title}'",
                UserName    = $"{sqp.Student.FirstName} {sqp.Student.LastName}",
                UserRole    = sqp.Student.Role.ToString(),
                Timestamp   = sqp.CompletedAt,
                Icon        = "check-circle",
                Metadata    = new Dictionary<string, object>
                {
                    ["quizId"]      = sqp.QuizId,
                    ["learnHubId"]  = sqp.Quiz.Link.LearnHubId,
                    ["pointsEarned"] = sqp.PointsEarned
                }
            }).ToList();

            var activities = new List<RecentActivityDTO>();
            activities.AddRange(learnHubActivities);
            activities.AddRange(userActivities);
            activities.AddRange(quizActivities);

            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(limit)
                .ToList();
        }

        // One DB query instead of 55+ (4 per role × N roles + 7 daily queries per role)
        private async Task<List<UserRegistrationStatsDTO>> GetUserRegistrationStatsInternalAsync(ApplicationDbContext db)
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            var sevenDaysAgo  = DateTime.UtcNow.AddDays(-7);
            var today         = DateTime.UtcNow.Date;

            var allUsers = await db.Users
                .AsNoTracking()
                .Select(u => new { u.Role, u.CreateAt })
                .ToListAsync();

            return Enum.GetValues<UserRole>().Select(role =>
            {
                var roleUsers = allUsers.Where(u => u.Role == role).ToList();
                var last7Days = Enumerable.Range(0, 7)
                    .Select(i => today.AddDays(i - 6))
                    .Select(date => new DailyRegistrationDTO
                    {
                        Date  = date,
                        Count = roleUsers.Count(u => u.CreateAt.Date == date)
                    })
                    .ToList();

                return new UserRegistrationStatsDTO
                {
                    Role       = role,
                    TotalCount = roleUsers.Count,
                    ThisMonth  = roleUsers.Count(u => u.CreateAt >= thirtyDaysAgo),
                    ThisWeek   = roleUsers.Count(u => u.CreateAt >= sevenDaysAgo),
                    Today      = roleUsers.Count(u => u.CreateAt >= today),
                    Last7Days  = last7Days
                };
            }).ToList();
        }

        // 2 queries instead of N+1 (one per LearnHub)
        private async Task<List<LearnHubStatsDTO>> GetLearnHubStatsInternalAsync(ApplicationDbContext db)
        {
            var learnHubs = await db.LearnHubs
                .AsNoTracking()
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            var allQuizIds = learnHubs
                .SelectMany(lh => lh.Links.SelectMany(l => l.Quizzes.Select(q => q.Id)))
                .ToList();

            var allPerformances = allQuizIds.Count > 0
                ? await db.StudentQuizPerformances
                    .AsNoTracking()
                    .Where(sqp => allQuizIds.Contains(sqp.QuizId))
                    .ToListAsync()
                : new List<StudentQuizPerformance>();

            var quizToLearnHubId = learnHubs
                .SelectMany(lh => lh.Links.SelectMany(l =>
                    l.Quizzes.Select(q => new { QuizId = q.Id, LearnHubId = lh.Id })))
                .ToDictionary(x => x.QuizId, x => x.LearnHubId);

            var perfByLearnHub = allPerformances
                .Where(p => quizToLearnHubId.ContainsKey(p.QuizId))
                .GroupBy(p => quizToLearnHubId[p.QuizId])
                .ToDictionary(g => g.Key, g => g.ToList());

            return learnHubs.Select(lh =>
            {
                var perfs           = perfByLearnHub.TryGetValue(lh.Id, out var p) ? p : new List<StudentQuizPerformance>();
                var totalAttempts   = perfs.Count;
                var uniqueStudents  = perfs.Select(x => x.StudentId).Distinct().Count();
                var averageScore    = totalAttempts > 0 ? perfs.Average(x => x.IsCorrect ? 1.0 : 0.0) : 0;
                var completionRate  = totalAttempts > 0 ? (double)perfs.Count(x => x.IsCompleted) / totalAttempts * 100 : 0;

                return new LearnHubStatsDTO
                {
                    Id             = lh.Id,
                    Title          = lh.Title,
                    Subject        = lh.Subject,
                    ClassType      = lh.ClassType,
                    Difficulty     = lh.Difficulty,
                    IsFree         = lh.IsFree,
                    CreatedAt      = lh.CreatedAt,
                    TotalLinks     = lh.Links.Count,
                    TotalQuizzes   = lh.Links.Sum(l => l.Quizzes.Count),
                    TotalAttempts  = totalAttempts,
                    UniqueStudents = uniqueStudents,
                    AverageScore   = Math.Round(averageScore, 2),
                    CompletionRate = Math.Round(completionRate, 2)
                };
            })
            .OrderByDescending(s => s.TotalAttempts)
            .ToList();
        }

        // 2 queries instead of N+1 (one per quiz)
        private async Task<List<QuizStatsDTO>> GetQuizStatsInternalAsync(ApplicationDbContext db)
        {
            var quizzes = await db.Quizzes
                .AsNoTracking()
                .Include(q => q.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            var quizIds = quizzes.Select(q => q.Id).ToHashSet();
            var allPerformances = quizIds.Count > 0
                ? await db.StudentQuizPerformances
                    .AsNoTracking()
                    .Where(sqp => quizIds.Contains(sqp.QuizId))
                    .ToListAsync()
                : new List<StudentQuizPerformance>();

            var perfByQuiz = allPerformances
                .GroupBy(p => p.QuizId)
                .ToDictionary(g => g.Key, g => g.ToList());

            return quizzes.Select(q =>
            {
                var perfs           = perfByQuiz.TryGetValue(q.Id, out var p) ? p : new List<StudentQuizPerformance>();
                var totalAttempts   = perfs.Count;
                var correctAttempts = perfs.Count(x => x.IsCompleted && x.IsCorrect);
                var successRate     = totalAttempts > 0 ? (double)correctAttempts / totalAttempts * 100 : 0;
                var avgTime         = perfs.Count > 0 ? perfs.Average(x => x.TimeSpentSeconds) : 0;

                return new QuizStatsDTO
                {
                    Id               = q.Id,
                    Question         = q.Question,
                    LearnHubTitle    = q.Link.LearnHub.Title,
                    LinkTitle        = q.Link.Title,
                    Points           = q.Points,
                    CreatedAt        = q.CreatedAt,
                    TotalAttempts    = totalAttempts,
                    CorrectAttempts  = correctAttempts,
                    SuccessRate      = Math.Round(successRate, 2),
                    AverageTimeSpent = Math.Round(avgTime, 2)
                };
            })
            .OrderByDescending(s => s.TotalAttempts)
            .ToList();
        }

        // 2 queries instead of N+1 (one totalRevenue query per package)
        private async Task<List<SubscriptionStatsDTO>> GetSubscriptionStatsInternalAsync(ApplicationDbContext db)
        {
            var packages = await db.SubscriptionPackages
                .AsNoTracking()
                .Include(sp => sp.Subscriptions)
                .ToListAsync();

            // Load all revenue in one query via navigation join
            var revenueByPackage = (await db.SubscriptionPayments
                .AsNoTracking()
                .Where(sp => sp.Status == PaymentStatus.Succeeded)
                .Select(sp => new { PackageId = sp.Subscription.SubscriptionPackageId, sp.Amount })
                .ToListAsync())
                .GroupBy(r => r.PackageId)
                .ToDictionary(g => g.Key, g => g.Sum(r => r.Amount));

            var thisMonth = DateTime.UtcNow.AddDays(-30);

            return packages.Select(package =>
            {
                var activeSubscriptions      = package.Subscriptions.Count(s => s.Status == SubscriptionStatus.Active);
                var monthlyRevenue           = package.Subscriptions
                    .Where(s => s.Status == SubscriptionStatus.Active)
                    .Sum(s => s.Amount) / 100m;
                var totalRevenue             = revenueByPackage.TryGetValue(package.Id, out var rev) ? rev / 100m : 0m;
                var newSubscriptionsThisMonth = package.Subscriptions.Count(s => s.CreatedAt >= thisMonth);
                var cancellationsThisMonth   = package.Subscriptions
                    .Count(s => s.CanceledAt.HasValue && s.CanceledAt.Value >= thisMonth);

                return new SubscriptionStatsDTO
                {
                    Id                       = package.Id,
                    PackageName              = package.Name,
                    UserType                 = package.UserType,
                    Status                   = SubscriptionStatus.Active,
                    ActiveSubscriptions      = activeSubscriptions,
                    MonthlyRevenue           = monthlyRevenue,
                    TotalRevenue             = totalRevenue,
                    CreatedAt                = package.CreatedAt,
                    NewSubscriptionsThisMonth = newSubscriptionsThisMonth,
                    CancellationsThisMonth   = cancellationsThisMonth
                };
            })
            .OrderByDescending(s => s.ActiveSubscriptions)
            .ToList();
        }

        // 2 queries instead of 36 (3 per month × 12 months)
        private async Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueInternalAsync(ApplicationDbContext db, int months)
        {
            var startDate = DateTime.UtcNow.AddMonths(-(months - 1));

            var payments = await db.SubscriptionPayments
                .AsNoTracking()
                .Where(sp => sp.CreatedAt >= startDate && sp.Status == PaymentStatus.Succeeded)
                .Select(sp => new { sp.CreatedAt, sp.Amount })
                .ToListAsync();

            var subscriptions = await db.Subscriptions
                .AsNoTracking()
                .Where(s => s.CreatedAt >= startDate || (s.CanceledAt != null && s.CanceledAt >= startDate))
                .Select(s => new { s.CreatedAt, s.CanceledAt })
                .ToListAsync();

            return Enumerable.Range(0, months).Select(i =>
            {
                var monthStart = startDate.AddMonths(i);
                var monthEnd   = monthStart.AddMonths(1);
                return new MonthlyRevenueDTO
                {
                    Year              = monthStart.Year,
                    Month             = monthStart.Month,
                    Revenue           = payments
                        .Where(p => p.CreatedAt >= monthStart && p.CreatedAt < monthEnd)
                        .Sum(p => (decimal)p.Amount) / 100m,
                    NewSubscriptions  = subscriptions.Count(s => s.CreatedAt >= monthStart && s.CreatedAt < monthEnd),
                    Cancellations     = subscriptions.Count(s =>
                        s.CanceledAt.HasValue && s.CanceledAt.Value >= monthStart && s.CanceledAt.Value < monthEnd)
                };
            }).ToList();
        }

        // 1 query instead of ~180 (2 per day × 30 days × N roles)
        private async Task<List<UserGrowthDTO>> GetUserGrowthInternalAsync(ApplicationDbContext db, int days)
        {
            var startDate = DateTime.UtcNow.AddDays(-days);

            var allUsers = await db.Users
                .AsNoTracking()
                .Select(u => new { u.Role, u.CreateAt })
                .ToListAsync();

            var roles  = Enum.GetValues<UserRole>();
            var result = new List<UserGrowthDTO>();

            for (int i = 0; i < days; i++)
            {
                var date     = startDate.AddDays(i);
                var nextDate = date.AddDays(1);
                var dayUsers = allUsers.Where(u => u.CreateAt >= date && u.CreateAt < nextDate).ToList();
                var total    = allUsers.Count(u => u.CreateAt < nextDate);

                foreach (var role in roles)
                {
                    var newByRole = dayUsers.Count(u => u.Role == role);
                    if (newByRole > 0)
                    {
                        result.Add(new UserGrowthDTO
                        {
                            Date      = date,
                            NewUsers  = newByRole,
                            TotalUsers = total,
                            Role      = role
                        });
                    }
                }
            }

            return result.OrderBy(g => g.Date).ToList();
        }

        // 2 queries instead of N+1
        private async Task<List<LearnHubPerformanceDTO>> GetTopPerformingLearnHubsInternalAsync(ApplicationDbContext db, int limit)
        {
            var learnHubs = await db.LearnHubs
                .AsNoTracking()
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            var allQuizIds = learnHubs
                .SelectMany(lh => lh.Links.SelectMany(l => l.Quizzes.Select(q => q.Id)))
                .ToHashSet();

            var allPerformances = allQuizIds.Count > 0
                ? await db.StudentQuizPerformances
                    .AsNoTracking()
                    .Where(sqp => allQuizIds.Contains(sqp.QuizId))
                    .ToListAsync()
                : new List<StudentQuizPerformance>();

            var quizToLearnHubId = learnHubs
                .SelectMany(lh => lh.Links.SelectMany(l =>
                    l.Quizzes.Select(q => new { QuizId = q.Id, LearnHubId = lh.Id })))
                .ToDictionary(x => x.QuizId, x => x.LearnHubId);

            var perfByLearnHub = allPerformances
                .Where(p => quizToLearnHubId.ContainsKey(p.QuizId))
                .GroupBy(p => quizToLearnHubId[p.QuizId])
                .ToDictionary(g => g.Key, g => g.ToList());

            return learnHubs.Select(lh =>
            {
                var perfs           = perfByLearnHub.TryGetValue(lh.Id, out var p) ? p : new List<StudentQuizPerformance>();
                var totalStudents   = perfs.Select(x => x.StudentId).Distinct().Count();
                var averageScore    = perfs.Count > 0 ? perfs.Average(x => x.IsCorrect ? 1.0 : 0.0) : 0;
                var completionRate  = perfs.Count > 0 ? (double)perfs.Count(x => x.IsCompleted) / perfs.Count * 100 : 0;
                var totalAttempts   = perfs.Count;
                var engagementScore = totalStudents * 0.4 + averageScore * 0.3 + completionRate * 0.3;

                return new LearnHubPerformanceDTO
                {
                    Id              = lh.Id,
                    Title           = lh.Title,
                    TotalStudents   = totalStudents,
                    AverageScore    = Math.Round(averageScore, 2),
                    CompletionRate  = Math.Round(completionRate, 2),
                    TotalAttempts   = totalAttempts,
                    EngagementScore = Math.Round(engagementScore, 2)
                };
            })
            .OrderByDescending(p => p.EngagementScore)
            .Take(limit)
            .ToList();
        }

        // 2 queries instead of N+1
        private async Task<List<QuizPerformanceDTO>> GetMostChallengingQuizzesInternalAsync(ApplicationDbContext db, int limit)
        {
            var quizzes = await db.Quizzes
                .AsNoTracking()
                .Include(q => q.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            var quizIds = quizzes.Select(q => q.Id).ToHashSet();
            var allPerformances = quizIds.Count > 0
                ? await db.StudentQuizPerformances
                    .AsNoTracking()
                    .Where(sqp => quizIds.Contains(sqp.QuizId))
                    .ToListAsync()
                : new List<StudentQuizPerformance>();

            var perfByQuiz = allPerformances
                .GroupBy(p => p.QuizId)
                .ToDictionary(g => g.Key, g => g.ToList());

            return quizzes.Select(q =>
            {
                var perfs         = perfByQuiz.TryGetValue(q.Id, out var p) ? p : new List<StudentQuizPerformance>();
                var totalAttempts = perfs.Count;
                var successRate   = totalAttempts > 0
                    ? (double)perfs.Count(x => x.IsCompleted && x.IsCorrect) / totalAttempts * 100
                    : 0;
                var avgTime = perfs.Count > 0 ? perfs.Average(x => x.TimeSpentSeconds) : 0;

                return new QuizPerformanceDTO
                {
                    Id               = q.Id,
                    Question         = q.Question,
                    LearnHubTitle    = q.Link.LearnHub.Title,
                    SuccessRate      = Math.Round(successRate, 2),
                    TotalAttempts    = totalAttempts,
                    AverageTimeSpent = Math.Round(avgTime, 2),
                    Difficulty       = q.Points
                };
            })
            .OrderBy(p => p.SuccessRate)
            .Take(limit)
            .ToList();
        }

        private async Task<List<GeographicStatsDTO>> GetGeographicDistributionInternalAsync(ApplicationDbContext db)
        {
            return await db.Users
                .AsNoTracking()
                .Where(u => !string.IsNullOrEmpty(u.City))
                .GroupBy(u => u.City)
                .Select(g => new GeographicStatsDTO
                {
                    City        = g.Key,
                    UserCount   = g.Count(),
                    SchoolCount = g.Count(u => u.Role == UserRole.Supervisor),
                    Revenue     = 0
                })
                .OrderByDescending(s => s.UserCount)
                .ToListAsync();
        }

        private async Task<Dictionary<string, object>> GetSystemHealthInternalAsync(ApplicationDbContext db)
        {
            var health = new Dictionary<string, object>();
            try
            {
                await db.Database.ExecuteSqlRawAsync("SELECT 1");
                health["database"] = "healthy";
            }
            catch
            {
                health["database"] = "unhealthy";
            }

            health["totalUsers"] = await db.Users.CountAsync();
            health["activeSubscriptions"] = await db.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active)
                .CountAsync();
            health["pendingApprovals"] = await db.Users
                .Where(u => u.ApprovalStatus == ApprovalStatus.Pending)
                .CountAsync();
            health["lastUpdate"] = DateTime.UtcNow;
            return health;
        }
    }
}
