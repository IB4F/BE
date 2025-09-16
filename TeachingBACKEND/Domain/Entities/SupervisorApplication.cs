using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.Entities
{
    public class SupervisorApplication
    {
        public Guid Id { get; set; }
        
        // Application Details
        public string SchoolName { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public string City { get; set; }
        public string? Address { get; set; }
        public string? AdditionalInfo { get; set; }
        
        // Application Status
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.Pending;
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        
        // Temporary Password (for approved applications)
        public string? TemporaryPassword { get; set; }
        
        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation Properties
        public Guid? ApprovedUserId { get; set; } // Link to User if approved
        public User? ApprovedUser { get; set; } // Navigation property
    }
}

