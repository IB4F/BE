namespace TeachingBACKEND.Domain.DTOs
{
    public class SupervisorDashboardDTO
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public double AverageProgress { get; set; }
        public int PendingPasswordResetRequests { get; set; }
        public List<StudentCredentialsDTO> Students { get; set; } = new List<StudentCredentialsDTO>();
    }

    public class StudentCredentialsDTO
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } // firstname.lastname@bga.al
        public string Password { get; set; } // Password i gjeneruar
        public string CurrentClass { get; set; }
        public string School { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public double ProgressPercentage { get; set; }
    }
}

