using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentRegistrationDTO
    {
        [Required, StringLength(50)]
        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        [JsonPropertyName("LastName")]
        public string LastName { get; set; }

        [Required, EmailAddress]
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }


        [Required, StringLength(50)]
        [JsonPropertyName("School")]
        public string School { get; set; }

        [Required, StringLength(20)]
        [JsonPropertyName("CurrentClass")]
        public string CurrentClass { get; set; }

        [Required, MinLength(6)]
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }
}
