using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ISubscriptionPackageService
    {
        Task<IEnumerable<SubscriptionPackage>> GetAllPackagesAsync();
        Task<SubscriptionPackage?> GetPackageByIdAsync(Guid id);
        Task<IEnumerable<SubscriptionPackage>> GetPackagesByUserTypeAsync(UserType userType);
        Task<IEnumerable<SubscriptionPackage>> GetPackagesByTierAsync(PackageTier tier);
        Task<IEnumerable<SubscriptionPackage>> GetPackagesByBillingIntervalAsync(BillingInterval billingInterval);
        Task<SubscriptionPackage?> GetFamilyPackageAsync(PackageTier tier, BillingInterval billingInterval);
    }
}
