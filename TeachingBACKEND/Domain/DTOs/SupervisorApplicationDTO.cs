namespace TeachingBACKEND.Domain.DTOs
{
    public class SupervisorApplicationDTO
    {
        public Guid SupervisorId { get; set; }
        public string SchoolName { get; set; }
        public string ContactPersonFirstName { get; set; }
        public string ContactPersonLastName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}

