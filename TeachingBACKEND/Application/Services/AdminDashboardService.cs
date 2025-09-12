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

        public AdminDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDTO> GetDashboardOverviewAsync()
        {
            var stats = await GetAdminStatsAsync();
            var recentActivities = await GetRecentActivitiesAsync(10);
            var registrationStats = await GetUserRegistrationStatsAsync();
            var learnHubStats = await GetLearnHubStatsAsync();
            var quizStats = await GetQuizStatsAsync();
            var subscriptionStats = await GetSubscriptionStatsAsync();

            return new AdminDashboardDTO
            {
                Stats = stats,
                RecentActivities = recentActivities,
                RegistrationStats = registrationStats,
                LearnHubStats = learnHubStats,
                QuizStats = quizStats,
                SubscriptionStats = subscriptionStats
            };
        }

        public async Task<AdminStatsDTO> GetAdminStatsAsync()
        {
            var activeLearnHubs = await _context.LearnHubs.CountAsync();
            var totalUsers = await _context.Users.CountAsync();
            var completedQuizzes = await _context.StudentQuizPerformances
                .Where(sqp => sqp.IsCompleted)
                .CountAsync();
            var activeAdmins = await _context.Users
                .Where(u => u.Role == UserRole.Admin)
                .CountAsync();
            var totalSchools = await _context.Users
                .Where(u => u.Role == UserRole.School)
                .CountAsync();
            var totalStudents = await _context.Users
                .Where(u => u.Role == UserRole.Student)
                .CountAsync();
            var totalFamilies = await _context.Users
                .Where(u => u.Role == UserRole.Family)
                .CountAsync();
            var pendingApprovals = await _context.Users
                .Where(u => u.ApprovalStatus == ApprovalStatus.Pending)
                .CountAsync();
            var totalRevenue = await _context.SubscriptionPayments
                .Where(sp => sp.Status == PaymentStatus.Succeeded)
                .SumAsync(sp => sp.Amount) / 100m; // Convert from cents
            var activeSubscriptions = await _context.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active)
                .CountAsync();

            return new AdminStatsDTO
            {
                ActiveLearnHubs = activeLearnHubs,
                TotalRegisteredUsers = totalUsers,
                CompletedQuizzes = completedQuizzes,
                ActiveAdministrators = activeAdmins,
                TotalSchools = totalSchools,
                TotalStudents = totalStudents,
                TotalFamilies = totalFamilies,
                PendingApprovals = pendingApprovals,
                TotalRevenue = totalRevenue,
                ActiveSubscriptions = activeSubscriptions
            };
        }

        public async Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int limit = 10)
        {
            var activities = new List<RecentActivityDTO>();

            // Get recent LearnHub creations
                var recentLearnHubs = await _context.LearnHubs
                .OrderByDescending(lh => lh.CreatedAt)
                .Take(5)
                .ToListAsync();

            var learnHubActivities = recentLearnHubs.Select(lh => new RecentActivityDTO
            {
                Id = lh.Id,
                Type = "learnhub_created",
                Description = $"LearnHub '{lh.Title}' è stato creato",
                UserName = "Sistema", // You might want to track who created it
                UserRole = "Admin",
                Timestamp = lh.CreatedAt,
                Icon = "book",
                Metadata = new Dictionary<string, object>
                {
                    ["learnHubId"] = lh.Id,
                    ["title"] = lh.Title,
                    ["subject"] = lh.Subject
                }
            }).ToList();

            // Get recent user registrations
            var recentUsers = await _context.Users
                .OrderByDescending(u => u.CreateAt)
                .Take(5)
                .ToListAsync();

            var userActivities = recentUsers.Select(u => new RecentActivityDTO
            {
                Id = u.Id,
                Type = "user_registered",
                Description = $"Nuovo utente registrato: {u.FirstName} {u.LastName}",
                UserName = $"{u.FirstName} {u.LastName}",
                UserRole = u.Role.ToString(),
                Timestamp = u.CreateAt,
                Icon = "user-plus",
                Metadata = new Dictionary<string, object>
                {
                    ["userId"] = u.Id,
                    ["email"] = u.Email,
                    ["role"] = u.Role.ToString()
                }
            }).ToList();

            // Get recent quiz completions
            var recentQuizCompletions = await _context.StudentQuizPerformances
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
                Id = sqp.Id,
                Type = "quiz_completed",
                Description = $"Quiz completato in '{sqp.Quiz.Link.LearnHub.Title}'",
                UserName = $"{sqp.Student.FirstName} {sqp.Student.LastName}",
                UserRole = sqp.Student.Role.ToString(),
                Timestamp = sqp.CompletedAt,
                Icon = "check-circle",
                Metadata = new Dictionary<string, object>
                {
                    ["quizId"] = sqp.QuizId,
                    ["learnHubId"] = sqp.Quiz.Link.LearnHubId,
                    ["pointsEarned"] = sqp.PointsEarned
                }
            }).ToList();

            // Get recent profile updates (placeholder - you might want to add an UpdatedAt field)
            var recentProfileUpdates = new List<RecentActivityDTO>();

            // Combine and sort all activities
            activities.AddRange(learnHubActivities);
            activities.AddRange(userActivities);
            activities.AddRange(quizActivities);
            activities.AddRange(recentProfileUpdates);

            return activities
                .OrderByDescending(a => a.Timestamp)
                .Take(limit)
                .ToList();
        }

        public async Task<List<UserRegistrationStatsDTO>> GetUserRegistrationStatsAsync()
        {
            var stats = new List<UserRegistrationStatsDTO>();
            var roles = Enum.GetValues<UserRole>();

            foreach (var role in roles)
            {
                var totalCount = await _context.Users
                    .Where(u => u.Role == role)
                    .CountAsync();

                var thisMonth = await _context.Users
                    .Where(u => u.Role == role && u.CreateAt >= DateTime.UtcNow.AddDays(-30))
                    .CountAsync();

                var thisWeek = await _context.Users
                    .Where(u => u.Role == role && u.CreateAt >= DateTime.UtcNow.AddDays(-7))
                    .CountAsync();

                var today = await _context.Users
                    .Where(u => u.Role == role && u.CreateAt >= DateTime.UtcNow.Date)
                    .CountAsync();

                // Get last 7 days data
                var last7Days = new List<DailyRegistrationDTO>();
                for (int i = 6; i >= 0; i--)
                {
                    var date = DateTime.UtcNow.AddDays(-i).Date;
                    var count = await _context.Users
                        .Where(u => u.Role == role && u.CreateAt.Date == date)
                        .CountAsync();

                    last7Days.Add(new DailyRegistrationDTO
                    {
                        Date = date,
                        Count = count
                    });
                }

                stats.Add(new UserRegistrationStatsDTO
                {
                    Role = role,
                    TotalCount = totalCount,
                    ThisMonth = thisMonth,
                    ThisWeek = thisWeek,
                    Today = today,
                    Last7Days = last7Days
                });
            }

            return stats;
        }

        public async Task<List<LearnHubStatsDTO>> GetLearnHubStatsAsync()
        {
            var learnHubs = await _context.LearnHubs
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            var stats = new List<LearnHubStatsDTO>();

            foreach (var learnHub in learnHubs)
            {
                var totalLinks = learnHub.Links.Count;
                var totalQuizzes = learnHub.Links.Sum(l => l.Quizzes.Count);

                // Get quiz performance data
                var learnHubQuizIds = learnHub.Links.SelectMany(l => l.Quizzes.Select(q => q.Id)).ToList();
                var quizPerformances = await _context.StudentQuizPerformances
                    .Where(sqp => learnHubQuizIds.Contains(sqp.QuizId))
                    .ToListAsync();

                var totalAttempts = quizPerformances.Count;
                var uniqueStudents = quizPerformances.Select(sqp => sqp.StudentId).Distinct().Count();
                var averageScore = quizPerformances.Any() ? quizPerformances.Average(sqp => sqp.IsCorrect ? 1.0 : 0.0) : 0;
                var completionRate = quizPerformances.Any() ? 
                    (double)quizPerformances.Count(sqp => sqp.IsCompleted) / quizPerformances.Count * 100 : 0;

                stats.Add(new LearnHubStatsDTO
                {
                    Id = learnHub.Id,
                    Title = learnHub.Title,
                    Subject = learnHub.Subject,
                    ClassType = learnHub.ClassType,
                    Difficulty = learnHub.Difficulty,
                    IsFree = learnHub.IsFree,
                    CreatedAt = learnHub.CreatedAt,
                    TotalLinks = totalLinks,
                    TotalQuizzes = totalQuizzes,
                    TotalAttempts = totalAttempts,
                    UniqueStudents = uniqueStudents,
                    AverageScore = Math.Round(averageScore, 2),
                    CompletionRate = Math.Round(completionRate, 2)
                });
            }

            return stats.OrderByDescending(s => s.TotalAttempts).ToList();
        }

        public async Task<List<QuizStatsDTO>> GetQuizStatsAsync()
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            var stats = new List<QuizStatsDTO>();

            foreach (var quiz in quizzes)
            {
                var performances = await _context.StudentQuizPerformances
                    .Where(sqp => sqp.QuizId == quiz.Id)
                    .ToListAsync();

                var totalAttempts = performances.Count;
                var correctAttempts = performances.Count(sqp => sqp.IsCompleted && sqp.IsCorrect);
                var successRate = totalAttempts > 0 ? (double)correctAttempts / totalAttempts * 100 : 0;
                var averageTimeSpent = performances.Any() ? performances.Average(sqp => sqp.TimeSpentSeconds) : 0;

                stats.Add(new QuizStatsDTO
                {
                    Id = quiz.Id,
                    Question = quiz.Question,
                    LearnHubTitle = quiz.Link.LearnHub.Title,
                    LinkTitle = quiz.Link.Title,
                    Points = quiz.Points,
                    CreatedAt = quiz.CreatedAt,
                    TotalAttempts = totalAttempts,
                    CorrectAttempts = correctAttempts,
                    SuccessRate = Math.Round(successRate, 2),
                    AverageTimeSpent = Math.Round(averageTimeSpent, 2)
                });
            }

            return stats.OrderByDescending(s => s.TotalAttempts).ToList();
        }

        public async Task<List<SubscriptionStatsDTO>> GetSubscriptionStatsAsync()
        {
            var packages = await _context.SubscriptionPackages
                .Include(sp => sp.Subscriptions)
                .ToListAsync();

            var stats = new List<SubscriptionStatsDTO>();

            foreach (var package in packages)
            {
                var activeSubscriptions = package.Subscriptions.Count(s => s.Status == SubscriptionStatus.Active);
                var monthlyRevenue = package.Subscriptions
                    .Where(s => s.Status == SubscriptionStatus.Active)
                    .Sum(s => s.Amount) / 100m; // Convert from cents

                var totalRevenue = await _context.SubscriptionPayments
                    .Where(sp => sp.Subscription.SubscriptionPackageId == package.Id && sp.Status == PaymentStatus.Succeeded)
                    .SumAsync(sp => sp.Amount) / 100m;

                var thisMonth = DateTime.UtcNow.AddDays(-30);
                var newSubscriptionsThisMonth = package.Subscriptions
                    .Count(s => s.CreatedAt >= thisMonth);
                var cancellationsThisMonth = package.Subscriptions
                    .Count(s => s.CanceledAt >= thisMonth);

                stats.Add(new SubscriptionStatsDTO
                {
                    Id = package.Id,
                    PackageName = package.Name,
                    UserType = package.UserType,
                    Status = SubscriptionStatus.Active, // This represents the package status
                    ActiveSubscriptions = activeSubscriptions,
                    MonthlyRevenue = monthlyRevenue,
                    TotalRevenue = totalRevenue,
                    CreatedAt = package.CreatedAt,
                    NewSubscriptionsThisMonth = newSubscriptionsThisMonth,
                    CancellationsThisMonth = cancellationsThisMonth
                });
            }

            return stats.OrderByDescending(s => s.ActiveSubscriptions).ToList();
        }

        public async Task<AdminAnalyticsDTO> GetAnalyticsAsync()
        {
            var monthlyRevenue = await GetMonthlyRevenueAsync(12);
            var userGrowth = await GetUserGrowthAsync(30);
            var topPerformingLearnHubs = await GetTopPerformingLearnHubsAsync(10);
            var mostChallengingQuizzes = await GetMostChallengingQuizzesAsync(10);
            var geographicDistribution = await GetGeographicDistributionAsync();

            return new AdminAnalyticsDTO
            {
                MonthlyRevenue = monthlyRevenue,
                UserGrowth = userGrowth,
                TopPerformingLearnHubs = topPerformingLearnHubs,
                MostChallengingQuizzes = mostChallengingQuizzes,
                GeographicDistribution = geographicDistribution
            };
        }

        public async Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueAsync(int months = 12)
        {
            var revenueData = new List<MonthlyRevenueDTO>();
            var startDate = DateTime.UtcNow.AddMonths(-months);

            for (int i = 0; i < months; i++)
            {
                var monthStart = startDate.AddMonths(i);
                var monthEnd = monthStart.AddMonths(1);

                var revenue = await _context.SubscriptionPayments
                    .Where(sp => sp.CreatedAt >= monthStart && sp.CreatedAt < monthEnd && sp.Status == PaymentStatus.Succeeded)
                    .SumAsync(sp => sp.Amount) / 100m;

                var newSubscriptions = await _context.Subscriptions
                    .Where(s => s.CreatedAt >= monthStart && s.CreatedAt < monthEnd)
                    .CountAsync();

                var cancellations = await _context.Subscriptions
                    .Where(s => s.CanceledAt >= monthStart && s.CanceledAt < monthEnd)
                    .CountAsync();

                revenueData.Add(new MonthlyRevenueDTO
                {
                    Year = monthStart.Year,
                    Month = monthStart.Month,
                    Revenue = revenue,
                    NewSubscriptions = newSubscriptions,
                    Cancellations = cancellations
                });
            }

            return revenueData;
        }

        public async Task<List<UserGrowthDTO>> GetUserGrowthAsync(int days = 30)
        {
            var growthData = new List<UserGrowthDTO>();
            var startDate = DateTime.UtcNow.AddDays(-days);

            for (int i = 0; i < days; i++)
            {
                var date = startDate.AddDays(i);
                var nextDate = date.AddDays(1);

                var newUsers = await _context.Users
                    .Where(u => u.CreateAt >= date && u.CreateAt < nextDate)
                    .CountAsync();

                var totalUsers = await _context.Users
                    .Where(u => u.CreateAt < nextDate)
                    .CountAsync();

                // Get growth by role
                var roles = Enum.GetValues<UserRole>();
                foreach (var role in roles)
                {
                    var newUsersByRole = await _context.Users
                        .Where(u => u.Role == role && u.CreateAt >= date && u.CreateAt < nextDate)
                        .CountAsync();

                    if (newUsersByRole > 0)
                    {
                        growthData.Add(new UserGrowthDTO
                        {
                            Date = date,
                            NewUsers = newUsersByRole,
                            TotalUsers = totalUsers,
                            Role = role
                        });
                    }
                }
            }

            return growthData.OrderBy(g => g.Date).ToList();
        }

        public async Task<List<LearnHubPerformanceDTO>> GetTopPerformingLearnHubsAsync(int limit = 10)
        {
            var learnHubs = await _context.LearnHubs
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            var performances = new List<LearnHubPerformanceDTO>();

            foreach (var learnHub in learnHubs)
            {
                var learnHubQuizIds = learnHub.Links.SelectMany(l => l.Quizzes.Select(q => q.Id)).ToList();
                var quizPerformances = await _context.StudentQuizPerformances
                    .Where(sqp => learnHubQuizIds.Contains(sqp.QuizId))
                    .ToListAsync();

                var totalStudents = quizPerformances.Select(sqp => sqp.StudentId).Distinct().Count();
                var averageScore = quizPerformances.Any() ? quizPerformances.Average(sqp => sqp.IsCorrect ? 1.0 : 0.0) : 0;
                var completionRate = quizPerformances.Any() ? 
                    (double)quizPerformances.Count(sqp => sqp.IsCompleted) / quizPerformances.Count * 100 : 0;
                var totalAttempts = quizPerformances.Count;
                var engagementScore = totalStudents * 0.4 + averageScore * 0.3 + completionRate * 0.3;

                performances.Add(new LearnHubPerformanceDTO
                {
                    Id = learnHub.Id,
                    Title = learnHub.Title,
                    TotalStudents = totalStudents,
                    AverageScore = Math.Round(averageScore, 2),
                    CompletionRate = Math.Round(completionRate, 2),
                    TotalAttempts = totalAttempts,
                    EngagementScore = Math.Round(engagementScore, 2)
                });
            }

            return performances.OrderByDescending(p => p.EngagementScore).Take(limit).ToList();
        }

        public async Task<List<QuizPerformanceDTO>> GetMostChallengingQuizzesAsync(int limit = 10)
        {
            var quizzes = await _context.Quizzes
                .Include(q => q.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            var performances = new List<QuizPerformanceDTO>();

            foreach (var quiz in quizzes)
            {
                var quizPerformances = await _context.StudentQuizPerformances
                    .Where(sqp => sqp.QuizId == quiz.Id)
                    .ToListAsync();

                var totalAttempts = quizPerformances.Count;
                var successRate = totalAttempts > 0 ? 
                    (double)quizPerformances.Count(sqp => sqp.IsCompleted && sqp.IsCorrect) / totalAttempts * 100 : 0;
                var averageTimeSpent = quizPerformances.Any() ? quizPerformances.Average(sqp => sqp.TimeSpentSeconds) : 0;

                performances.Add(new QuizPerformanceDTO
                {
                    Id = quiz.Id,
                    Question = quiz.Question,
                    LearnHubTitle = quiz.Link.LearnHub.Title,
                    SuccessRate = Math.Round(successRate, 2),
                    TotalAttempts = totalAttempts,
                    AverageTimeSpent = Math.Round(averageTimeSpent, 2),
                    Difficulty = quiz.Points // Using points as difficulty indicator
                });
            }

            return performances.OrderBy(p => p.SuccessRate).Take(limit).ToList();
        }

        public async Task<List<GeographicStatsDTO>> GetGeographicDistributionAsync()
        {
            var stats = await _context.Users
                .Where(u => !string.IsNullOrEmpty(u.City))
                .GroupBy(u => u.City)
                .Select(g => new GeographicStatsDTO
                {
                    City = g.Key,
                    UserCount = g.Count(),
                    SchoolCount = g.Count(u => u.Role == UserRole.School),
                    Revenue = 0 // You might want to calculate this based on subscriptions
                })
                .OrderByDescending(s => s.UserCount)
                .ToListAsync();

            return stats;
        }

        public async Task<Dictionary<string, object>> GetSystemHealthAsync()
        {
            var health = new Dictionary<string, object>();

            // Database connectivity
            try
            {
                await _context.Database.ExecuteSqlRawAsync("SELECT 1");
                health["database"] = "healthy";
            }
            catch
            {
                health["database"] = "unhealthy";
            }

            // System metrics
            health["totalUsers"] = await _context.Users.CountAsync();
            health["activeSubscriptions"] = await _context.Subscriptions
                .Where(s => s.Status == SubscriptionStatus.Active)
                .CountAsync();
            health["pendingApprovals"] = await _context.Users
                .Where(u => u.ApprovalStatus == ApprovalStatus.Pending)
                .CountAsync();
            health["lastUpdate"] = DateTime.UtcNow;

            return health;
        }
    }
}
