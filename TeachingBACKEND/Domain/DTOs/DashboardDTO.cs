namespace TeachingBACKEND.Domain.DTOs
{
    public class DashboardDTO
    {
        public DashboardStatsDTO Stats { get; set; }
        public List<LearnHubProgressDTO> LatestLearnHubs { get; set; }
        public List<WeeklyActivityDayDTO> WeeklyActivity { get; set; }
    }

    public class DashboardStatsDTO
    {
        public double TotalProgress { get; set; }
        public int PointsCollected { get; set; }
        public int CompletedLearnHubs { get; set; }
        public int TotalLearnHubs { get; set; }
        public double? AverageAccuracy { get; set; }
        public int TodayExercisesCompleted { get; set; }
        public int DailyGoal { get; set; }
    }

    public class WeeklyActivityDayDTO
    {
        public int DayIndex { get; set; }
        public string Date { get; set; }
        public int ExercisesCompleted { get; set; }
        public int PointsEarned { get; set; }
    }

    public class LearnHubProgressDTO
    {
        public Guid Id { get; set; } // LearnHub ID
        public string Title { get; set; }
        public double ProgressPercentage { get; set; } // Individual LearnHub progress (e.g., 75%, 40%, 90%)
        public string LastExercise { get; set; } // Last exercise completed (e.g., "Ngjyrat dhe Psikologjia")
        public Guid? LastExerciseLinkId { get; set; } // Link ID where the last completed quiz is located
        public DateTime? LastActivityAt { get; set; } // When the student last worked on this LearnHub
        public int PointsEarned { get; set; } // Points earned in this specific LearnHub
        public int TotalPossiblePoints { get; set; } // Total possible points for this LearnHub
    }
}
