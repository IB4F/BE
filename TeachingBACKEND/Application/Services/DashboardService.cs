using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDTO> GetStudentDashboardAsync(Guid studentId)
        {
            // Get all LearnHubs that the student has access to
            var learnHubs = await _context.LearnHubs
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            // Get student's performance data
            var studentPerformances = await _context.StudentPerformanceSummaries
                .Where(sps => sps.StudentId == studentId)
                .Include(sps => sps.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            // Get student's quiz performances for detailed calculations
            var quizPerformances = await _context.StudentQuizPerformances
                .Where(sqp => sqp.StudentId == studentId)
                .Include(sqp => sqp.Quiz)
                    .ThenInclude(q => q.Link)
                        .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            // Calculate dashboard stats
            var stats = CalculateDashboardStats(learnHubs, studentPerformances, quizPerformances);

            // Get latest LearnHubs with progress
            var latestLearnHubs = await GetLatestLearnHubsWithProgressAsync(studentId, learnHubs, studentPerformances, quizPerformances);

            return new DashboardDTO
            {
                Stats = stats,
                LatestLearnHubs = latestLearnHubs
            };
        }

        private DashboardStatsDTO CalculateDashboardStats(
            List<LearnHub> learnHubs, 
            List<StudentPerformanceSummary> studentPerformances,
            List<StudentQuizPerformance> quizPerformances)
        {
            var totalLearnHubs = learnHubs.Count;
            var totalPointsCollected = quizPerformances.Sum(qp => qp.PointsEarned);

            // Calculate completed LearnHubs
            var completedLearnHubs = 0;
            var totalProgressSum = 0.0;

            foreach (var learnHub in learnHubs)
            {
                var learnHubLinks = learnHub.Links;
                var totalQuizzesInLearnHub = learnHubLinks.Sum(l => l.Quizzes.Count);
                
                if (totalQuizzesInLearnHub == 0) continue;

                var completedQuizzesInLearnHub = quizPerformances
                    .Where(qp => learnHubLinks.Any(l => l.Id == qp.LinkId))
                    .Count();

                var learnHubProgress = totalQuizzesInLearnHub > 0 
                    ? (double)completedQuizzesInLearnHub / totalQuizzesInLearnHub * 100 
                    : 0;

                totalProgressSum += learnHubProgress;

                // Consider LearnHub completed if 80% or more quizzes are done
                if (learnHubProgress >= 80)
                {
                    completedLearnHubs++;
                }
            }

            var totalProgress = totalLearnHubs > 0 ? totalProgressSum / totalLearnHubs : 0;

            return new DashboardStatsDTO
            {
                TotalProgress = Math.Round(totalProgress, 1),
                PointsCollected = totalPointsCollected,
                CompletedLearnHubs = completedLearnHubs,
                TotalLearnHubs = totalLearnHubs
            };
        }

        private async Task<List<LearnHubProgressDTO>> GetLatestLearnHubsWithProgressAsync(
            Guid studentId,
            List<LearnHub> learnHubs,
            List<StudentPerformanceSummary> studentPerformances,
            List<StudentQuizPerformance> quizPerformances)
        {
            var latestLearnHubs = new List<LearnHubProgressDTO>();

            // Get the 3 most recently accessed LearnHubs
            var recentLearnHubIds = quizPerformances
                .GroupBy(qp => qp.Quiz.Link.LearnHubId)
                .OrderByDescending(g => g.Max(qp => qp.LastAttemptAt ?? qp.CompletedAt))
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            foreach (var learnHubId in recentLearnHubIds)
            {
                var learnHub = learnHubs.FirstOrDefault(lh => lh.Id == learnHubId);
                if (learnHub == null) continue;

                var learnHubLinks = learnHub.Links;
                var totalQuizzesInLearnHub = learnHubLinks.Sum(l => l.Quizzes.Count);
                
                if (totalQuizzesInLearnHub == 0) continue;

                var completedQuizzesInLearnHub = quizPerformances
                    .Where(qp => learnHubLinks.Any(l => l.Id == qp.LinkId))
                    .Count();

                var progressPercentage = totalQuizzesInLearnHub > 0 
                    ? (double)completedQuizzesInLearnHub / totalQuizzesInLearnHub * 100 
                    : 0;

                // Get last exercise (most recent quiz completed)
                var lastQuizPerformance = quizPerformances
                    .Where(qp => learnHubLinks.Any(l => l.Id == qp.LinkId))
                    .OrderByDescending(qp => qp.LastAttemptAt ?? qp.CompletedAt)
                    .FirstOrDefault();

                var lastExercise = lastQuizPerformance?.Quiz?.Link?.Title ?? "Nuk ka ushtrime të përfunduara";
                var lastExerciseLinkId = lastQuizPerformance?.LinkId;

                // Get points earned in this LearnHub
                var pointsEarned = quizPerformances
                    .Where(qp => learnHubLinks.Any(l => l.Id == qp.LinkId))
                    .Sum(qp => qp.PointsEarned);

                var totalPossiblePoints = learnHubLinks.Sum(l => l.Quizzes.Sum(q => q.Points));

                latestLearnHubs.Add(new LearnHubProgressDTO
                {
                    Id = learnHub.Id,
                    Title = learnHub.Title,
                    ProgressPercentage = Math.Round(progressPercentage, 1),
                    LastExercise = lastExercise,
                    LastExerciseLinkId = lastExerciseLinkId,
                    LastActivityAt = lastQuizPerformance?.LastAttemptAt ?? lastQuizPerformance?.CompletedAt,
                    PointsEarned = pointsEarned,
                    TotalPossiblePoints = totalPossiblePoints
                });
            }

            return latestLearnHubs;
        }
    }
}
