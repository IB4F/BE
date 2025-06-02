namespace TeachingBACKEND.Domain.DTOs
{
    public class PaymentSessionRequestDTO
    {
        public string Email { get; set; }
        public string RegistrationType { get; set; }
        public Guid PlanId {get;set;}
    }
}
