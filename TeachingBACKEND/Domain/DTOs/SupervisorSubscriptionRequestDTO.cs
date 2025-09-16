using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class SupervisorSubscriptionRequestDTO
    {
        public Guid SupervisorApplicationId { get; set; }
        public Guid SubscriptionPackageId { get; set; }
        public BillingInterval BillingInterval { get; set; }
    }
}
