using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class SubscriptionAccessService : ISubscriptionAccessService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionAccessService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanUserAccessLearnHubAsync(Guid userId, Guid learnHubId)
        {
            // Get the LearnHub and its required tier (optimized single query)
            var learnHub = await _context.LearnHubs
                .AsNoTracking() // Performance optimization for read-only check
                .FirstOrDefaultAsync(lh => lh.Id == learnHubId);

            if (learnHub == null)
                return false;

            // Free LearnHubs are accessible to everyone
            if (learnHub.IsFree || learnHub.RequiredTier == null)
                return true;

            // Get user's access tier
            var userAccessTier = await GetUserAccessTierAsync(userId);
            
            // If user has no subscription, they can only access free LearnHubs
            if (userAccessTier == null)
                return false;

            // Check if user's tier is sufficient
            return userAccessTier >= learnHub.RequiredTier;
        }

        public async Task<PackageTier?> GetUserAccessTierAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.ActiveSubscription)
                    .ThenInclude(s => s.SubscriptionPackage)
                .Include(u => u.Supervisor)
                    .ThenInclude(s => s.ActiveSubscription)
                        .ThenInclude(s => s.SubscriptionPackage)
                .AsNoTracking() // Performance optimization for read-only check
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return null;

            // Check user's own subscription
            if (user.HasActiveSubscription && user.ActiveSubscription?.SubscriptionPackage != null)
            {
                return user.ActiveSubscription.SubscriptionPackage.Tier;
            }

            // Check supervisor's subscription (for supervised students)
            if (user.SupervisorId.HasValue && user.Supervisor?.HasActiveSubscription == true)
            {
                return user.Supervisor.ActiveSubscription?.SubscriptionPackage?.Tier;
            }

            // Check parent's subscription (for family members)
            if (user.ParentUserId.HasValue)
            {
                var parent = await _context.Users
                    .Include(p => p.ActiveSubscription)
                        .ThenInclude(s => s.SubscriptionPackage)
                    .AsNoTracking() // Performance optimization for read-only check
                    .FirstOrDefaultAsync(p => p.Id == user.ParentUserId);

                if (parent?.HasActiveSubscription == true)
                {
                    return parent.ActiveSubscription?.SubscriptionPackage?.Tier;
                }
            }

            return null;
        }

        public async Task<List<LearnHub>> GetAccessibleLearnHubsAsync(Guid userId)
        {
            var userAccessTier = await GetUserAccessTierAsync(userId);

            var query = _context.LearnHubs.AsNoTracking(); // Performance optimization

            // If user has no subscription, only show free LearnHubs
            if (userAccessTier == null)
            {
                return await query
                    .Where(lh => lh.IsFree || lh.RequiredTier == null)
                    .ToListAsync();
            }

            // Show LearnHubs that are free or within user's tier
            return await query
                .Where(lh => lh.IsFree || lh.RequiredTier == null || lh.RequiredTier <= userAccessTier)
                .ToListAsync();
        }

        public async Task<bool> NeedsUpgradeForLearnHubAsync(Guid userId, Guid learnHubId)
        {
            var learnHub = await _context.LearnHubs
                .AsNoTracking() // Performance optimization for read-only check
                .FirstOrDefaultAsync(lh => lh.Id == learnHubId);

            if (learnHub == null || learnHub.IsFree || learnHub.RequiredTier == null)
                return false;

            var userAccessTier = await GetUserAccessTierAsync(userId);
            
            if (userAccessTier == null)
                return true;

            return userAccessTier < learnHub.RequiredTier;
        }

        public async Task<PackageTier?> GetLearnHubRequiredTierAsync(Guid learnHubId)
        {
            var learnHub = await _context.LearnHubs
                .AsNoTracking() // Performance optimization for read-only check
                .FirstOrDefaultAsync(lh => lh.Id == learnHubId);

            return learnHub?.RequiredTier;
        }
    }
}
