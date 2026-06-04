using System.ComponentModel.DataAnnotations;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SchoolRegistrationDTO
    {
        [Required, StringLength(50)]
        public string SchoolName { get; set; }

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

        [Required, StringLength(20)]
        public Guid SubscriptionPackageId { get; set; }

        [Required]
        public List<CreateStudentBySchoolDTO> Students { get; set; }

        public PaymentProvider Provider { get; set; } = PaymentProvider.Stripe;

        [Required]
        public bool TermsAccepted { get; set; }
    }
}
