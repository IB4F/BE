using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class StudentProgressService : IStudentProgressService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudentProgressService> _logger;

        public StudentProgressService(ApplicationDbContext context, ILogger<StudentProgressService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<double> CalculateStudentProgressAsync(Guid studentId)
        {
            try
            {
                // Get all available links (learning materials)
                var totalLinks = await _context.Links
                    .Include(l => l.LearnHub)
                    .CountAsync();

                if (totalLinks == 0)
                    return 0.0;

                // Get student's performance summaries
                var performanceSummaries = await _context.StudentPerformanceSummaries
                    .Where(sps => sps.StudentId == studentId)
                    .ToListAsync();

                // Calculate progress based on completion rates and scores
                var totalProgress = 0.0;
                var linksWithActivity = 0;

                foreach (var summary in performanceSummaries)
                {
                    if (summary.TotalQuizzes > 0)
                    {
                        // Weight: 70% completion rate + 30% average score
                        var completionWeight = 0.7;
                        var scoreWeight = 0.3;
                        
                        var completionRate = (double)summary.CompletedQuizzes / summary.TotalQuizzes;
                        var normalizedScore = summary.AverageScore / 100.0; // Assuming max score is 100
                        
                        var linkProgress = (completionRate * completionWeight + normalizedScore * scoreWeight) * 100;
                        totalProgress += linkProgress;
                        linksWithActivity++;
                    }
                }

                // If student has no activity, return 0
                if (linksWithActivity == 0)
                    return 0.0;

                // Calculate overall progress
                var overallProgress = totalProgress / totalLinks;
                
                // Ensure progress is between 0 and 100
                return Math.Max(0, Math.Min(100, overallProgress));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating progress for student {StudentId}", studentId);
                return 0.0;
            }
        }

        public async Task<Dictionary<Guid, double>> CalculateStudentsProgressAsync(IEnumerable<Guid> studentIds)
        {
            var result = new Dictionary<Guid, double>();
            
            foreach (var studentId in studentIds)
            {
                var progress = await CalculateStudentProgressAsync(studentId);
                result[studentId] = progress;
            }
            
            return result;
        }

        public async Task<StudentProgressDetailDTO> GetStudentProgressDetailAsync(Guid studentId)
        {
            try
            {
                var student = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == studentId && u.Role == UserRole.Student);

                if (student == null)
                    throw new ArgumentException("Student not found");

                // Get all available links
                var allLinks = await _context.Links
                    .Include(l => l.LearnHub)
                    .Include(l => l.Quizzes)
                    .ToListAsync();

                // Get student's performance summaries
                var performanceSummaries = await _context.StudentPerformanceSummaries
                    .Where(sps => sps.StudentId == studentId)
                    .Include(sps => sps.Link)
                    .ThenInclude(l => l.LearnHub)
                    .ToListAsync();

                var linkProgressList = new List<LinkProgressDTO>();
                var totalQuizzes = 0;
                var completedQuizzes = 0;
                var totalPointsEarned = 0;
                var totalPossiblePoints = 0;
                var totalTimeSpent = 0;
                DateTime? firstActivity = null;
                DateTime? lastActivity = null;

                foreach (var link in allLinks)
                {
                    var summary = performanceSummaries.FirstOrDefault(s => s.LinkId == link.Id);
                    
                    var linkProgress = new LinkProgressDTO
                    {
                        LinkId = link.Id,
                        LinkTitle = link.Title,
                        LearnHubId = link.LearnHubId,
                        LearnHubTitle = link.LearnHub.Title,
                        TotalQuizzes = link.Quizzes.Count,
                        CompletedQuizzes = summary?.CompletedQuizzes ?? 0,
                        PointsEarned = summary?.TotalPointsEarned ?? 0,
                        PossiblePoints = link.Quizzes.Sum(q => q.Points),
                        TimeSpentMinutes = summary?.TotalTimeSpent / 60 ?? 0,
                        FirstAttemptAt = summary?.FirstAttemptAt,
                        LastAttemptAt = summary?.LastAttemptAt,
                        IsCompleted = summary != null && summary.CompletedQuizzes == link.Quizzes.Count
                    };

                    if (linkProgress.TotalQuizzes > 0)
                    {
                        linkProgress.ProgressPercentage = (double)linkProgress.CompletedQuizzes / linkProgress.TotalQuizzes * 100;
                        linkProgress.AverageScore = summary?.AverageScore ?? 0;
                    }

                    linkProgressList.Add(linkProgress);

                    // Aggregate totals
                    totalQuizzes += linkProgress.TotalQuizzes;
                    completedQuizzes += linkProgress.CompletedQuizzes;
                    totalPointsEarned += linkProgress.PointsEarned;
                    totalPossiblePoints += linkProgress.PossiblePoints;
                    totalTimeSpent += linkProgress.TimeSpentMinutes;

                    if (summary?.FirstAttemptAt != null)
                    {
                        if (firstActivity == null || summary.FirstAttemptAt < firstActivity)
                            firstActivity = summary.FirstAttemptAt;
                    }

                    if (summary?.LastAttemptAt != null)
                    {
                        if (lastActivity == null || summary.LastAttemptAt > lastActivity)
                            lastActivity = summary.LastAttemptAt;
                    }
                }

                var overallProgress = await CalculateStudentProgressAsync(studentId);
                var averageScore = totalPossiblePoints > 0 ? (double)totalPointsEarned / totalPossiblePoints * 100 : 0;

                return new StudentProgressDetailDTO
                {
                    StudentId = studentId,
                    StudentName = $"{student.FirstName} {student.LastName}",
                    OverallProgress = overallProgress,
                    TotalLinks = allLinks.Count,
                    CompletedLinks = linkProgressList.Count(lp => lp.IsCompleted),
                    TotalQuizzes = totalQuizzes,
                    CompletedQuizzes = completedQuizzes,
                    AverageScore = averageScore,
                    TotalPointsEarned = totalPointsEarned,
                    TotalPossiblePoints = totalPossiblePoints,
                    TotalTimeSpentMinutes = totalTimeSpent,
                    FirstActivityAt = firstActivity,
                    LastActivityAt = lastActivity,
                    LinkProgress = linkProgressList
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting progress detail for student {StudentId}", studentId);
                throw;
            }
        }

        public async Task UpdateStudentPerformanceSummaryAsync(Guid studentId, Guid linkId)
        {
            try
            {
                // Get all quiz performances for this student and link
                var performances = await _context.StudentQuizPerformances
                    .Where(sqp => sqp.StudentId == studentId && sqp.LinkId == linkId)
                    .ToListAsync();

                // Get the link and its quizzes
                var link = await _context.Links
                    .Include(l => l.Quizzes)
                    .FirstOrDefaultAsync(l => l.Id == linkId);

                if (link == null)
                    return;

                var totalQuizzes = link.Quizzes.Count;
                var completedQuizzes = performances.Count;
                var totalPointsEarned = performances.Sum(p => p.PointsEarned);
                var totalPossiblePoints = link.Quizzes.Sum(q => q.Points);
                var totalTimeSpent = performances.Sum(p => p.TimeSpentSeconds);
                var averageScore = totalPossiblePoints > 0 ? (double)totalPointsEarned / totalPossiblePoints * 100 : 0;
                var completionRate = totalQuizzes > 0 ? (double)completedQuizzes / totalQuizzes * 100 : 0;

                var firstAttempt = performances.Any() ? performances.Min(p => p.StartedAt) : (DateTime?)null;
                var lastAttempt = performances.Any() ? performances.Max(p => p.CompletedAt) : (DateTime?)null;

                // Find existing summary or create new one
                var summary = await _context.StudentPerformanceSummaries
                    .FirstOrDefaultAsync(sps => sps.StudentId == studentId && sps.LinkId == linkId);

                if (summary == null)
                {
                    summary = new StudentPerformanceSummary
                    {
                        Id = Guid.NewGuid(),
                        StudentId = studentId,
                        LinkId = linkId,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.StudentPerformanceSummaries.Add(summary);
                }

                // Update summary data
                summary.TotalQuizzes = totalQuizzes;
                summary.CompletedQuizzes = completedQuizzes;
                summary.TotalPointsEarned = totalPointsEarned;
                summary.TotalPossiblePoints = totalPossiblePoints;
                summary.CompletionRate = completionRate;
                summary.AverageScore = averageScore;
                summary.TotalTimeSpent = totalTimeSpent;
                summary.AverageTimePerQuiz = completedQuizzes > 0 ? totalTimeSpent / completedQuizzes : 0;
                summary.FirstAttemptAt = firstAttempt;
                summary.LastAttemptAt = lastAttempt;
                summary.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating performance summary for student {StudentId}, link {LinkId}", studentId, linkId);
                throw;
            }
        }

        public async Task<SupervisorProgressStatsDTO> GetSupervisorProgressStatsAsync(Guid supervisorId)
        {
            try
            {
                var students = await _context.Users
                    .Where(u => u.SupervisorId == supervisorId && u.Role == UserRole.Student)
                    .ToListAsync();

                var studentIds = students.Select(s => s.Id).ToList();
                var studentProgress = await CalculateStudentsProgressAsync(studentIds);

                var studentSummaries = new List<SupervisorStudentSummaryDTO>();
                var totalQuizzesCompleted = 0;
                var totalTimeSpent = 0;
                var totalScore = 0.0;
                var activeStudents = 0;

                foreach (var student in students)
                {
                    var progress = studentProgress.GetValueOrDefault(student.Id, 0.0);
                    var isActive = student.ApprovalStatus == ApprovalStatus.Approved;
                    
                    if (isActive)
                        activeStudents++;

                    // Get student's performance data
                    var performanceSummaries = await _context.StudentPerformanceSummaries
                        .Where(sps => sps.StudentId == student.Id)
                        .ToListAsync();

                    var completedQuizzes = performanceSummaries.Sum(s => s.CompletedQuizzes);
                    var averageScore = performanceSummaries.Any() ? performanceSummaries.Average(s => s.AverageScore) : 0;
                    var timeSpent = performanceSummaries.Sum(s => s.TotalTimeSpent);
                    var lastActivity = performanceSummaries.Any() ? performanceSummaries.Max(s => s.LastAttemptAt) : (DateTime?)null;

                    studentSummaries.Add(new SupervisorStudentSummaryDTO
                    {
                        StudentId = student.Id,
                        StudentName = $"{student.FirstName} {student.LastName}",
                        ProgressPercentage = progress,
                        CompletedQuizzes = completedQuizzes,
                        AverageScore = averageScore,
                        LastActivityAt = lastActivity,
                        IsActive = isActive
                    });

                    totalQuizzesCompleted += completedQuizzes;
                    totalTimeSpent += timeSpent;
                    totalScore += averageScore;
                }

                var averageProgress = studentProgress.Values.Any() ? studentProgress.Values.Average() : 0.0;
                var overallAverageScore = studentSummaries.Any() ? studentSummaries.Average(s => s.AverageScore) : 0.0;

                return new SupervisorProgressStatsDTO
                {
                    TotalStudents = students.Count,
                    ActiveStudents = activeStudents,
                    AverageProgress = averageProgress,
                    AverageScore = overallAverageScore,
                    TotalQuizzesCompleted = totalQuizzesCompleted,
                    TotalTimeSpentMinutes = totalTimeSpent / 60,
                    StudentSummaries = studentSummaries
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting progress stats for supervisor {SupervisorId}", supervisorId);
                throw;
            }
        }
    }
}
