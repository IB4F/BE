using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class AdminDashboardDTO
    {
        public AdminStatsDTO Stats { get; set; }
        public List<RecentActivityDTO> RecentActivities { get; set; }
        public List<UserRegistrationStatsDTO> RegistrationStats { get; set; }
        public List<LearnHubStatsDTO> LearnHubStats { get; set; }
        public List<QuizStatsDTO> QuizStats { get; set; }
        public List<SubscriptionStatsDTO> SubscriptionStats { get; set; }
    }

    public class AdminStatsDTO
    {
        public int ActiveLearnHubs { get; set; }
        public int TotalRegisteredUsers { get; set; }
        public int CompletedQuizzes { get; set; }
        public int ActiveAdministrators { get; set; }
        public int TotalSchools { get; set; }
        public int TotalStudents { get; set; }
        public int TotalFamilies { get; set; }
        public int PendingApprovals { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveSubscriptions { get; set; }
    }

    public class RecentActivityDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; } // "learnhub_created", "user_registered", "quiz_completed", "profile_updated", "milestone_reached"
        public string Description { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public DateTime Timestamp { get; set; }
        public string Icon { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }

    public class UserRegistrationStatsDTO
    {
        public UserRole Role { get; set; }
        public int TotalCount { get; set; }
        public int ThisMonth { get; set; }
        public int ThisWeek { get; set; }
        public int Today { get; set; }
        public List<DailyRegistrationDTO> Last7Days { get; set; } = new();
    }

    public class DailyRegistrationDTO
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    public class LearnHubStatsDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string ClassType { get; set; }
        public int Difficulty { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalLinks { get; set; }
        public int TotalQuizzes { get; set; }
        public int TotalAttempts { get; set; }
        public int UniqueStudents { get; set; }
        public double AverageScore { get; set; }
        public double CompletionRate { get; set; }
    }

    public class QuizStatsDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string LearnHubTitle { get; set; }
        public string LinkTitle { get; set; }
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalAttempts { get; set; }
        public int CorrectAttempts { get; set; }
        public double SuccessRate { get; set; }
        public double AverageTimeSpent { get; set; }
    }

    public class SubscriptionStatsDTO
    {
        public Guid Id { get; set; }
        public string PackageName { get; set; }
        public UserType UserType { get; set; }
        public SubscriptionStatus Status { get; set; }
        public int ActiveSubscriptions { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NewSubscriptionsThisMonth { get; set; }
        public int CancellationsThisMonth { get; set; }
    }

    public class AdminAnalyticsDTO
    {
        public List<MonthlyRevenueDTO> MonthlyRevenue { get; set; } = new();
        public List<UserGrowthDTO> UserGrowth { get; set; } = new();
        public List<LearnHubPerformanceDTO> TopPerformingLearnHubs { get; set; } = new();
        public List<QuizPerformanceDTO> MostChallengingQuizzes { get; set; } = new();
        public List<GeographicStatsDTO> GeographicDistribution { get; set; } = new();
    }

    public class MonthlyRevenueDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public int NewSubscriptions { get; set; }
        public int Cancellations { get; set; }
    }

    public class UserGrowthDTO
    {
        public DateTime Date { get; set; }
        public int NewUsers { get; set; }
        public int TotalUsers { get; set; }
        public UserRole Role { get; set; }
    }

    public class LearnHubPerformanceDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int TotalStudents { get; set; }
        public double AverageScore { get; set; }
        public double CompletionRate { get; set; }
        public int TotalAttempts { get; set; }
        public double EngagementScore { get; set; }
    }

    public class QuizPerformanceDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string LearnHubTitle { get; set; }
        public double SuccessRate { get; set; }
        public int TotalAttempts { get; set; }
        public double AverageTimeSpent { get; set; }
        public int Difficulty { get; set; }
    }

    public class GeographicStatsDTO
    {
        public string City { get; set; }
        public int UserCount { get; set; }
        public int SchoolCount { get; set; }
        public decimal Revenue { get; set; }
    }
}
