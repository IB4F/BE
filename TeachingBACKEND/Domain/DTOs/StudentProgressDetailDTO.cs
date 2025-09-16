namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentProgressDetailDTO
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public double OverallProgress { get; set; }
        public int TotalLinks { get; set; }
        public int CompletedLinks { get; set; }
        public int TotalQuizzes { get; set; }
        public int CompletedQuizzes { get; set; }
        public double AverageScore { get; set; }
        public int TotalPointsEarned { get; set; }
        public int TotalPossiblePoints { get; set; }
        public int TotalTimeSpentMinutes { get; set; }
        public DateTime? FirstActivityAt { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public List<LinkProgressDTO> LinkProgress { get; set; } = new List<LinkProgressDTO>();
        public string? GeneratedPassword { get; set; } // Only included if user hasn't logged in yet
    }

    public class LinkProgressDTO
    {
        public Guid LinkId { get; set; }
        public string LinkTitle { get; set; }
        public Guid LearnHubId { get; set; }
        public string LearnHubTitle { get; set; }
        public double ProgressPercentage { get; set; }
        public int TotalQuizzes { get; set; }
        public int CompletedQuizzes { get; set; }
        public double AverageScore { get; set; }
        public int PointsEarned { get; set; }
        public int PossiblePoints { get; set; }
        public int TimeSpentMinutes { get; set; }
        public DateTime? FirstAttemptAt { get; set; }
        public DateTime? LastAttemptAt { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class SupervisorProgressStatsDTO
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public double AverageProgress { get; set; }
        public double AverageScore { get; set; }
        public int TotalQuizzesCompleted { get; set; }
        public int TotalTimeSpentMinutes { get; set; }
        public List<SupervisorStudentSummaryDTO> StudentSummaries { get; set; } = new List<SupervisorStudentSummaryDTO>();
    }

    // Separate class for supervisor dashboard student summaries to avoid conflict
    public class SupervisorStudentSummaryDTO
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public double ProgressPercentage { get; set; }
        public int CompletedQuizzes { get; set; }
        public double AverageScore { get; set; }
        public DateTime? LastActivityAt { get; set; }
        public bool IsActive { get; set; }
    }
}