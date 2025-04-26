using System.ComponentModel.DataAnnotations;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SchoolRegistrationDTO
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Required, StringLength(50)]
        public string Profession { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }

        [Required, StringLength(10)]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string School { get; set; }

    }
}
