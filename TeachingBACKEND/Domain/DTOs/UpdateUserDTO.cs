namespace TeachingBACKEND.Domain.DTOs
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? School { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Profession { get; set; }
        public string? CurrentClass { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}