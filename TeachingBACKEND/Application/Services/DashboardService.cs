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
            await using var ctx4 = await _contextFactory.CreateDbContextAsync();

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

            var conceptMasteryTask = ctx4.UserConceptMastery
                .AsNoTracking()
                .Where(m => m.UserId == studentId)
                .Include(m => m.ConceptTag)
                .OrderBy(m => m.MasteryLevel)
                .ToListAsync();

            await Task.WhenAll(learnHubsTask, studentPerformancesTask, quizPerformancesTask, conceptMasteryTask);

            var learnHubs = learnHubsTask.Result;
            var studentPerformances = studentPerformancesTask.Result;
            var quizPerformances = quizPerformancesTask.Result;
            var conceptMastery = conceptMasteryTask.Result;

            var stats = CalculateDashboardStats(learnHubs, studentPerformances, quizPerformances);
            var latestLearnHubs = GetLatestLearnHubsWithProgress(studentId, learnHubs, studentPerformances, quizPerformances);
            var weeklyActivity = CalculateWeeklyActivity(quizPerformances);

            var today = DateTime.UtcNow.Date;

            return new DashboardDTO
            {
                Stats = stats,
                LatestLearnHubs = latestLearnHubs,
                WeeklyActivity = weeklyActivity,
                ConceptMastery = conceptMastery.Select(m => new UserConceptMasteryDTO
                {
                    ConceptTagId    = m.ConceptTagId,
                    ConceptTagName  = m.ConceptTag?.Name,
                    MasteryLevel    = m.MasteryLevel,
                    TotalAttempts   = m.TotalAttempts,
                    CorrectAttempts = m.CorrectAttempts,
                    NextReviewDate  = m.NextReviewDate,
                    LastAttemptAt   = m.LastAttemptAt
                }).ToList(),
                PendingReviews = conceptMastery.Count(m =>
                    m.NextReviewDate.HasValue && m.NextReviewDate.Value.Date <= today)
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

            // Change 1 — average accuracy
            double? averageAccuracy = null;
            if (quizPerformances.Count > 0)
            {
                var correctCount = quizPerformances.Count(qp => qp.IsCorrect);
                averageAccuracy = Math.Round((double)correctCount / quizPerformances.Count * 100, 1);
            }

            // Change 2 — daily goal
            var todayUtc = DateTime.UtcNow.Date;
            var todayExercisesCompleted = quizPerformances.Count(qp =>
                (qp.LastAttemptAt ?? qp.CompletedAt).Date == todayUtc);

            return new DashboardStatsDTO
            {
                TotalProgress = Math.Round(totalProgress, 1),
                PointsCollected = totalPointsCollected,
                CompletedLearnHubs = completedLearnHubs,
                TotalLearnHubs = totalLearnHubs,
                AverageAccuracy = averageAccuracy,
                TodayExercisesCompleted = todayExercisesCompleted,
                DailyGoal = 5
            };
        }

        private List<LearnHubProgressDTO> GetLatestLearnHubsWithProgress(
            Guid studentId,
            List<LearnHub> learnHubs,
            List<StudentPerformanceSummary> studentPerformances,
            List<StudentQuizPerformance> quizPerformances)
        {
            // All LearnHubs the student has ever touched, ordered by most recent activity DESC
            var recentLearnHubIds = quizPerformances
                .GroupBy(qp => qp.Quiz.Link.LearnHubId)
                .OrderByDescending(g => g.Max(qp => qp.LastAttemptAt ?? qp.CompletedAt))
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

                // Cap at exactly 100 so completed hubs never show 99.x or 100.something
                var rawProgress = (double)lhPerformances.Count / totalQuizzesInLearnHub * 100;
                var progressPercentage = Math.Min(100.0, Math.Round(rawProgress, 1));

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
                    ProgressPercentage = progressPercentage,
                    LastExercise = lastExercise,
                    LastExerciseLinkId = lastQuizPerformance?.LinkId,
                    LastActivityAt = lastQuizPerformance?.LastAttemptAt ?? lastQuizPerformance?.CompletedAt,
                    PointsEarned = pointsEarned,
                    TotalPossiblePoints = totalPossiblePoints
                });
            }

            return result;
        }

        private static List<WeeklyActivityDayDTO> CalculateWeeklyActivity(List<StudentQuizPerformance> quizPerformances)
        {
            var today = DateTime.UtcNow.Date;

            // ISO week: Monday = 0 … Sunday = 6
            var dow = (int)today.DayOfWeek; // 0=Sun, 1=Mon, …, 6=Sat
            var daysFromMonday = dow == 0 ? 6 : dow - 1;
            var monday = today.AddDays(-daysFromMonday);

            return Enumerable.Range(0, 7).Select(i =>
            {
                var day = monday.AddDays(i);

                if (day > today)
                    return new WeeklyActivityDayDTO { DayIndex = i, Date = day.ToString("yyyy-MM-dd"), ExercisesCompleted = 0, PointsEarned = 0 };

                var dayPerfs = quizPerformances
                    .Where(qp => (qp.LastAttemptAt ?? qp.CompletedAt).Date == day)
                    .ToList();

                return new WeeklyActivityDayDTO
                {
                    DayIndex = i,
                    Date = day.ToString("yyyy-MM-dd"),
                    ExercisesCompleted = dayPerfs.Count,
                    PointsEarned = dayPerfs.Sum(qp => qp.PointsEarned)
                };
            }).ToList();
        }
    }
}
