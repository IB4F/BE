using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/dashboard")]
    public class AdminDashboardController : ControllerBase
    {
        private readonly IAdminDashboardService _adminDashboardService;
        private readonly ApplicationDbContext _context;

        public AdminDashboardController(IAdminDashboardService adminDashboardService, ApplicationDbContext context)
        {
            _adminDashboardService = adminDashboardService;
            _context = context;
        }

        /// <summary>
        /// Get complete admin dashboard overview with all statistics and recent activities
        /// </summary>
        /// <returns>Complete dashboard data</returns>
        [HttpGet("overview")]
        public async Task<ActionResult<AdminDashboardDTO>> GetDashboardOverview()
        {
            try
            {
                var dashboard = await _adminDashboardService.GetDashboardOverviewAsync();
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get main statistics for admin dashboard cards
        /// </summary>
        /// <returns>Admin statistics including user counts, revenue, etc.</returns>
        [HttpGet("stats")]
        public async Task<ActionResult<AdminStatsDTO>> GetAdminStats()
        {
            try
            {
                var stats = await _adminDashboardService.GetAdminStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get recent activities for admin dashboard
        /// </summary>
        /// <param name="limit">Number of activities to return (default: 10)</param>
        /// <returns>List of recent activities</returns>
        [HttpGet("recent-activities")]
        public async Task<ActionResult<List<RecentActivityDTO>>> GetRecentActivities([FromQuery] int limit = 10)
        {
            try
            {
                if (limit <= 0 || limit > 50)
                {
                    return BadRequest(new { error = "Limit must be between 1 and 50" });
                }

                var activities = await _adminDashboardService.GetRecentActivitiesAsync(limit);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get user registration statistics by role
        /// </summary>
        /// <returns>Registration statistics for each user role</returns>
        [HttpGet("user-registration-stats")]
        public async Task<ActionResult<List<UserRegistrationStatsDTO>>> GetUserRegistrationStats()
        {
            try
            {
                var stats = await _adminDashboardService.GetUserRegistrationStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get LearnHub statistics and performance data
        /// </summary>
        /// <returns>LearnHub statistics including engagement metrics</returns>
        [HttpGet("learnhub-stats")]
        public async Task<ActionResult<List<LearnHubStatsDTO>>> GetLearnHubStats()
        {
            try
            {
                var stats = await _adminDashboardService.GetLearnHubStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get quiz statistics and performance data
        /// </summary>
        /// <returns>Quiz statistics including success rates and attempts</returns>
        [HttpGet("quiz-stats")]
        public async Task<ActionResult<List<QuizStatsDTO>>> GetQuizStats()
        {
            try
            {
                var stats = await _adminDashboardService.GetQuizStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get subscription statistics and revenue data
        /// </summary>
        /// <returns>Subscription statistics including revenue metrics</returns>
        [HttpGet("subscription-stats")]
        public async Task<ActionResult<List<SubscriptionStatsDTO>>> GetSubscriptionStats()
        {
            try
            {
                var stats = await _adminDashboardService.GetSubscriptionStatsAsync();
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get comprehensive analytics data
        /// </summary>
        /// <returns>Analytics including revenue trends, user growth, and performance metrics</returns>
        [HttpGet("analytics")]
        public async Task<ActionResult<AdminAnalyticsDTO>> GetAnalytics()
        {
            try
            {
                var analytics = await _adminDashboardService.GetAnalyticsAsync();
                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get monthly revenue data for charts
        /// </summary>
        /// <param name="months">Number of months to retrieve (default: 12)</param>
        /// <returns>Monthly revenue data</returns>
        [HttpGet("monthly-revenue")]
        public async Task<ActionResult<List<MonthlyRevenueDTO>>> GetMonthlyRevenue([FromQuery] int months = 12)
        {
            try
            {
                if (months <= 0 || months > 24)
                {
                    return BadRequest(new { error = "Months must be between 1 and 24" });
                }

                var revenue = await _adminDashboardService.GetMonthlyRevenueAsync(months);
                return Ok(revenue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get user growth data for charts
        /// </summary>
        /// <param name="days">Number of days to retrieve (default: 30)</param>
        /// <returns>User growth data by role</returns>
        [HttpGet("user-growth")]
        public async Task<ActionResult<List<UserGrowthDTO>>> GetUserGrowth([FromQuery] int days = 30)
        {
            try
            {
                if (days <= 0 || days > 365)
                {
                    return BadRequest(new { error = "Days must be between 1 and 365" });
                }

                var growth = await _adminDashboardService.GetUserGrowthAsync(days);
                return Ok(growth);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get top performing LearnHubs based on engagement
        /// </summary>
        /// <param name="limit">Number of LearnHubs to return (default: 10)</param>
        /// <returns>Top performing LearnHubs</returns>
        [HttpGet("top-performing-learnhubs")]
        public async Task<ActionResult<List<LearnHubPerformanceDTO>>> GetTopPerformingLearnHubs([FromQuery] int limit = 10)
        {
            try
            {
                if (limit <= 0 || limit > 50)
                {
                    return BadRequest(new { error = "Limit must be between 1 and 50" });
                }

                var learnHubs = await _adminDashboardService.GetTopPerformingLearnHubsAsync(limit);
                return Ok(learnHubs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get most challenging quizzes (lowest success rates)
        /// </summary>
        /// <param name="limit">Number of quizzes to return (default: 10)</param>
        /// <returns>Most challenging quizzes</returns>
        [HttpGet("most-challenging-quizzes")]
        public async Task<ActionResult<List<QuizPerformanceDTO>>> GetMostChallengingQuizzes([FromQuery] int limit = 10)
        {
            try
            {
                if (limit <= 0 || limit > 50)
                {
                    return BadRequest(new { error = "Limit must be between 1 and 50" });
                }

                var quizzes = await _adminDashboardService.GetMostChallengingQuizzesAsync(limit);
                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get geographic distribution of users
        /// </summary>
        /// <returns>User distribution by city</returns>
        [HttpGet("geographic-distribution")]
        public async Task<ActionResult<List<GeographicStatsDTO>>> GetGeographicDistribution()
        {
            try
            {
                var distribution = await _adminDashboardService.GetGeographicDistributionAsync();
                return Ok(distribution);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get system health status
        /// </summary>
        /// <returns>System health metrics</returns>
        [HttpGet("system-health")]
        public async Task<ActionResult<Dictionary<string, object>>> GetSystemHealth()
        {
            try
            {
                var health = await _adminDashboardService.GetSystemHealthAsync();
                return Ok(health);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get detailed user analytics for a specific role
        /// </summary>
        /// <param name="role">User role to analyze</param>
        /// <returns>Detailed analytics for the specified role</returns>
        [HttpGet("user-analytics/{role}")]
        public async Task<ActionResult<object>> GetUserAnalyticsByRole(string role)
        {
            try
            {
                if (!Enum.TryParse<UserRole>(role, true, out var userRole))
                {
                    return BadRequest(new { error = "Invalid user role" });
                }

                // Get specific analytics for the role
                var users = await _context.Users
                    .Where(u => u.Role == userRole)
                    .ToListAsync();

                var analytics = new
                {
                    TotalUsers = users.Count,
                    ActiveUsers = users.Count(u => u.ApprovalStatus == ApprovalStatus.Approved),
                    PendingApprovals = users.Count(u => u.ApprovalStatus == ApprovalStatus.Pending),
                    VerifiedUsers = users.Count(u => u.IsEmailVerified),
                    RecentRegistrations = users.Count(u => u.CreateAt >= DateTime.UtcNow.AddDays(-7)),
                    GeographicDistribution = users
                        .Where(u => !string.IsNullOrEmpty(u.City))
                        .GroupBy(u => u.City)
                        .Select(g => new { City = g.Key, Count = g.Count() })
                        .OrderByDescending(x => x.Count)
                        .Take(10)
                        .ToList()
                };

                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        /// <summary>
        /// Get LearnHub performance analytics for a specific LearnHub
        /// </summary>
        /// <param name="learnHubId">LearnHub ID</param>
        /// <returns>Detailed performance analytics for the LearnHub</returns>
        [HttpGet("learnhub-analytics/{learnHubId}")]
        public async Task<ActionResult<object>> GetLearnHubAnalytics(Guid learnHubId)
        {
            try
            {
                var learnHub = await _context.LearnHubs
                    .Include(lh => lh.Links)
                        .ThenInclude(l => l.Quizzes)
                    .FirstOrDefaultAsync(lh => lh.Id == learnHubId);

                if (learnHub == null)
                {
                    return NotFound(new { error = "LearnHub not found" });
                }

                var learnHubQuizIds = learnHub.Links.SelectMany(l => l.Quizzes.Select(q => q.Id)).ToList();
                var quizPerformances = await _context.StudentQuizPerformances
                    .Where(sqp => learnHubQuizIds.Contains(sqp.QuizId))
                    .Include(sqp => sqp.Student)
                    .ToListAsync();

                var analytics = new
                {
                    LearnHub = new
                    {
                        Id = learnHub.Id,
                        Title = learnHub.Title,
                        Subject = learnHub.Subject,
                        ClassType = learnHub.ClassType,
                        Difficulty = learnHub.Difficulty,
                        IsFree = learnHub.IsFree,
                        CreatedAt = learnHub.CreatedAt
                    },
                    Performance = new
                    {
                        TotalStudents = quizPerformances.Select(sqp => sqp.StudentId).Distinct().Count(),
                        TotalAttempts = quizPerformances.Count,
                        AverageScore = quizPerformances.Any() ? Math.Round(quizPerformances.Average(sqp => sqp.IsCorrect ? 1.0 : 0.0), 2) : 0,
                        CompletionRate = quizPerformances.Any() ? 
                            Math.Round((double)quizPerformances.Count(sqp => sqp.IsCompleted) / quizPerformances.Count * 100, 2) : 0,
                        AverageTimeSpent = quizPerformances.Any() ? 
                            Math.Round(quizPerformances.Average(sqp => sqp.TimeSpentSeconds), 2) : 0
                    },
                    Links = learnHub.Links.Select(l => 
                    {
                        var linkQuizIds = l.Quizzes.Select(q => q.Id).ToList();
                        var linkPerformances = quizPerformances.Where(sqp => linkQuizIds.Contains(sqp.QuizId)).ToList();
                        return new
                        {
                            Id = l.Id,
                            Title = l.Title,
                            Progress = l.Progress,
                            QuizCount = l.Quizzes.Count,
                            TotalAttempts = linkPerformances.Count,
                            AverageScore = linkPerformances.Any() ? Math.Round(linkPerformances.Average(sqp => sqp.IsCorrect ? 1.0 : 0.0), 2) : 0
                        };
                    }).ToList()
                };

                return Ok(analytics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
