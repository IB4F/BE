namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentCreatedResponseDTO
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GeneratedEmail { get; set; } // firstname.lastname@bga.al
        public string GeneratedPassword { get; set; } // Password i gjeneruar automatikisht
        public string CurrentClass { get; set; }
        public string School { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

