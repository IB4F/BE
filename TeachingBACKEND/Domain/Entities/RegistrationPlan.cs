namespace TeachingBACKEND.Domain.Entities
{
    public class RegistrationPlan
    {
        public Guid Id { get; set; }
        public string RegistrationPlanName { get; set; }
        public string Type { get; set; }
        public long Price { get; set; }
        public string StripeProductName { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public bool IsFamilyPlan { get; set; }
        public string UserType { get; set; }
        public int MaxUsers { get; set; }
    }
}
