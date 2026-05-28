namespace TeachingBACKEND.Domain.DTOs
{
    public class SupervisorDashboardDTO
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int NewStudents { get; set; }
        public double AverageProgress { get; set; }
        public int PendingPasswordResetRequests { get; set; }
        public List<double?> WeeklyProgressTrend { get; set; } = new();
        public List<SupervisorActivityDTO> RecentActivities { get; set; } = new();
        public List<StudentCredentialsDTO> Students { get; set; } = new();
    }

    public class StudentCredentialsDTO
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CurrentClass { get; set; }
        public string School { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public double ProgressPercentage { get; set; }
        public string Status { get; set; } // "new" | "aktiv" | "idle"
    }

    public class SupervisorActivityDTO
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string Type { get; set; } // "ok" | "warn" | "info" | "default"
        public string Description { get; set; }
        public string TimeAgo { get; set; }
    }
}

