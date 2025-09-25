using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ISubscriptionAccessService
    {
        /// <summary>
        /// Determines if a user can access a specific LearnHub based on their subscription tier
        /// </summary>
        /// <param name="userId">The user ID to check access for</param>
        /// <param name="learnHubId">The LearnHub ID to check access to</param>
        /// <returns>True if user can access the LearnHub, false otherwise</returns>
        Task<bool> CanUserAccessLearnHubAsync(Guid userId, Guid learnHubId);

        /// <summary>
        /// Gets the highest subscription tier a user has access to
        /// </summary>
        /// <param name="userId">The user ID to check</param>
        /// <returns>The highest PackageTier the user has access to, or null if no active subscription</returns>
        Task<PackageTier?> GetUserAccessTierAsync(Guid userId);

        /// <summary>
        /// Gets all LearnHubs a user can access based on their subscription tier
        /// </summary>
        /// <param name="userId">The user ID to check access for</param>
        /// <returns>List of LearnHubs the user can access</returns>
        Task<List<LearnHub>> GetAccessibleLearnHubsAsync(Guid userId);

        /// <summary>
        /// Checks if a user needs to upgrade their subscription to access a specific LearnHub
        /// </summary>
        /// <param name="userId">The user ID to check</param>
        /// <param name="learnHubId">The LearnHub ID to check access to</param>
        /// <returns>True if user needs to upgrade, false if they can access it or it's free</returns>
        Task<bool> NeedsUpgradeForLearnHubAsync(Guid userId, Guid learnHubId);

        /// <summary>
        /// Gets the required tier for a specific LearnHub
        /// </summary>
        /// <param name="learnHubId">The LearnHub ID</param>
        /// <returns>The required tier for the LearnHub, or null if it's free</returns>
        Task<PackageTier?> GetLearnHubRequiredTierAsync(Guid learnHubId);
    }
}
