using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardDTO> GetDashboardOverviewAsync();
        Task<AdminStatsDTO> GetAdminStatsAsync();
        Task<List<RecentActivityDTO>> GetRecentActivitiesAsync(int limit = 10);
        Task<List<UserRegistrationStatsDTO>> GetUserRegistrationStatsAsync();
        Task<List<LearnHubStatsDTO>> GetLearnHubStatsAsync();
        Task<List<QuizStatsDTO>> GetQuizStatsAsync();
        Task<List<SubscriptionStatsDTO>> GetSubscriptionStatsAsync();
        Task<AdminAnalyticsDTO> GetAnalyticsAsync();
        Task<List<MonthlyRevenueDTO>> GetMonthlyRevenueAsync(int months = 12);
        Task<List<UserGrowthDTO>> GetUserGrowthAsync(int days = 30);
        Task<List<LearnHubPerformanceDTO>> GetTopPerformingLearnHubsAsync(int limit = 10);
        Task<List<QuizPerformanceDTO>> GetMostChallengingQuizzesAsync(int limit = 10);
        Task<List<GeographicStatsDTO>> GetGeographicDistributionAsync();
        Task<Dictionary<string, object>> GetSystemHealthAsync();
    }
}
