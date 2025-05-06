namespace TeachingBACKEND.Domain.DTOs
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string School { get; set; }
        public string City { get; set; }
    }
}
