using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public DashboardService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<DashboardDTO> GetStudentDashboardAsync(Guid studentId)
        {
            await using var ctx1 = await _contextFactory.CreateDbContextAsync();
            await using var ctx2 = await _contextFactory.CreateDbContextAsync();
            await using var ctx3 = await _contextFactory.CreateDbContextAsync();

            var learnHubsTask = ctx1.LearnHubs
                .AsNoTracking()
                .Include(lh => lh.Links)
                    .ThenInclude(l => l.Quizzes)
                .ToListAsync();

            var studentPerformancesTask = ctx2.StudentPerformanceSummaries
                .AsNoTracking()
                .Where(sps => sps.StudentId == studentId)
                .Include(sps => sps.Link)
                    .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            var quizPerformancesTask = ctx3.StudentQuizPerformances
                .AsNoTracking()
                .Where(sqp => sqp.StudentId == studentId)
                .Include(sqp => sqp.Quiz)
                    .ThenInclude(q => q.Link)
                        .ThenInclude(l => l.LearnHub)
                .ToListAsync();

            await Task.WhenAll(learnHubsTask, studentPerformancesTask, quizPerformancesTask);

            var learnHubs = learnHubsTask.Result;
            var studentPerformances = studentPerformancesTask.Result;
            var quizPerformances = quizPerformancesTask.Result;

            var stats = CalculateDashboardStats(learnHubs, studentPerformances, quizPerformances);
            var latestLearnHubs = GetLatestLearnHubsWithProgress(studentId, learnHubs, studentPerformances, quizPerformances);

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

            var completedLearnHubs = 0;
            var totalProgressSum = 0.0;

            foreach (var learnHub in learnHubs)
            {
                var learnHubLinks = learnHub.Links;
                var totalQuizzesInLearnHub = learnHubLinks.Sum(l => l.Quizzes.Count);

                if (totalQuizzesInLearnHub == 0) continue;

                var linkIds = new HashSet<Guid>(learnHubLinks.Select(l => l.Id));
                var completedQuizzesInLearnHub = quizPerformances.Count(qp => linkIds.Contains(qp.LinkId));

                var learnHubProgress = (double)completedQuizzesInLearnHub / totalQuizzesInLearnHub * 100;
                totalProgressSum += learnHubProgress;

                if (learnHubProgress >= 80)
                    completedLearnHubs++;
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

        private List<LearnHubProgressDTO> GetLatestLearnHubsWithProgress(
            Guid studentId,
            List<LearnHub> learnHubs,
            List<StudentPerformanceSummary> studentPerformances,
            List<StudentQuizPerformance> quizPerformances)
        {
            var recentLearnHubIds = quizPerformances
                .GroupBy(qp => qp.Quiz.Link.LearnHubId)
                .OrderByDescending(g => g.Max(qp => qp.LastAttemptAt ?? qp.CompletedAt))
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            var learnHubById = learnHubs.ToDictionary(lh => lh.Id);
            var result = new List<LearnHubProgressDTO>();

            foreach (var learnHubId in recentLearnHubIds)
            {
                if (!learnHubById.TryGetValue(learnHubId, out var learnHub)) continue;

                var linkIds = new HashSet<Guid>(learnHub.Links.Select(l => l.Id));
                var totalQuizzesInLearnHub = learnHub.Links.Sum(l => l.Quizzes.Count);

                if (totalQuizzesInLearnHub == 0) continue;

                var lhPerformances = quizPerformances.Where(qp => linkIds.Contains(qp.LinkId)).ToList();
                var progressPercentage = (double)lhPerformances.Count / totalQuizzesInLearnHub * 100;

                var lastQuizPerformance = lhPerformances
                    .OrderByDescending(qp => qp.LastAttemptAt ?? qp.CompletedAt)
                    .FirstOrDefault();

                var lastExercise = lastQuizPerformance?.Quiz?.Link?.Title ?? "Nuk ka ushtrime të përfunduara";
                var pointsEarned = lhPerformances.Sum(qp => qp.PointsEarned);
                var totalPossiblePoints = learnHub.Links.Sum(l => l.Quizzes.Sum(q => q.Points));

                result.Add(new LearnHubProgressDTO
                {
                    Id = learnHub.Id,
                    Title = learnHub.Title,
                    ProgressPercentage = Math.Round(progressPercentage, 1),
                    LastExercise = lastExercise,
                    LastExerciseLinkId = lastQuizPerformance?.LinkId,
                    LastActivityAt = lastQuizPerformance?.LastAttemptAt ?? lastQuizPerformance?.CompletedAt,
                    PointsEarned = pointsEarned,
                    TotalPossiblePoints = totalPossiblePoints
                });
            }

            return result;
        }
    }
}
