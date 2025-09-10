using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class SubscriptionPackageService : ISubscriptionPackageService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionPackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionPackage>> GetAllPackagesAsync()
        {
            return await _context.SubscriptionPackages
                .Where(p => p.IsActive)
                .OrderBy(p => p.UserType)
                .ThenBy(p => p.Tier)
                .ThenBy(p => p.BillingInterval)
                .ToListAsync();
        }

        public async Task<SubscriptionPackage?> GetPackageByIdAsync(Guid id)
        {
            return await _context.SubscriptionPackages
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<IEnumerable<SubscriptionPackage>> GetPackagesByUserTypeAsync(UserType userType)
        {
            return await _context.SubscriptionPackages
                .Where(p => p.UserType == userType && p.IsActive)
                .OrderBy(p => p.Tier)
                .ThenBy(p => p.BillingInterval)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubscriptionPackage>> GetPackagesByTierAsync(PackageTier tier)
        {
            return await _context.SubscriptionPackages
                .Where(p => p.Tier == tier && p.IsActive)
                .OrderBy(p => p.UserType)
                .ThenBy(p => p.BillingInterval)
                .ToListAsync();
        }

        public async Task<IEnumerable<SubscriptionPackage>> GetPackagesByBillingIntervalAsync(BillingInterval billingInterval)
        {
            return await _context.SubscriptionPackages
                .Where(p => p.BillingInterval == billingInterval && p.IsActive)
                .OrderBy(p => p.UserType)
                .ThenBy(p => p.Tier)
                .ToListAsync();
        }

        public async Task<SubscriptionPackage?> GetFamilyPackageAsync(PackageTier tier, BillingInterval billingInterval)
        {
            return await _context.SubscriptionPackages
                .FirstOrDefaultAsync(p => p.UserType == UserType.Family 
                    && p.Tier == tier 
                    && p.BillingInterval == billingInterval 
                    && p.IsActive);
        }
    }
}
