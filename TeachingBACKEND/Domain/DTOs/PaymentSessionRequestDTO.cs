namespace TeachingBACKEND.Domain.DTOs
{
    public class PaymentSessionRequestDTO
    {
        public string Email { get; set; }
        public string RegistrationType { get; set; }
        public Guid PlanId { get; set; }
        public int? FamilyMemberCount { get; set; } = 1;
        
        // Add registration data as metadata for webhook processing
        public string RegistrationData { get; set; } // JSON string of registration data
    }
}
