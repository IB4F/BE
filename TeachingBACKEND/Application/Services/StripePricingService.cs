using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class StripePricingService
    {
        /// <summary>
        /// Gets the appropriate Stripe price ID for a family package based on family members
        /// </summary>
        /// <param name="package">The subscription package</param>
        /// <param name="familyMembers">Number of family members</param>
        /// <param name="billingInterval">Monthly or Yearly</param>
        /// <returns>Stripe price ID</returns>
        public string GetStripePriceId(SubscriptionPackage package, int familyMembers, BillingInterval billingInterval)
        {
            // For family packages, we use a single price ID with quantity
            // The price ID represents the cost per family member
            return billingInterval switch
            {
                BillingInterval.Month => package.StripeMonthlyPriceId,
                BillingInterval.Year => package.StripeYearlyPriceId,
                _ => throw new ArgumentException($"Unsupported billing interval: {billingInterval}")
            };
        }

        /// <summary>
        /// Gets the quantity for Stripe subscription (number of family members)
        /// </summary>
        /// <param name="package">The subscription package</param>
        /// <param name="familyMembers">Number of family members</param>
        /// <returns>Quantity for Stripe</returns>
        public int GetStripeQuantity(SubscriptionPackage package, int familyMembers)
        {
            if (package.UserType != UserType.Family)
            {
                return 1; // Non-family packages always have quantity 1
            }

            // Validate family members count
            if (familyMembers < package.MinFamilyMembers || familyMembers > package.MaxFamilyMembers)
            {
                throw new ArgumentException(
                    $"Family members must be between {package.MinFamilyMembers} and {package.MaxFamilyMembers}");
            }

            return familyMembers;
        }

        /// <summary>
        /// Creates Stripe subscription item options for family packages
        /// </summary>
        /// <param name="package">The subscription package</param>
        /// <param name="familyMembers">Number of family members</param>
        /// <param name="billingInterval">Monthly or Yearly</param>
        /// <returns>Stripe subscription item options</returns>
        public Stripe.SubscriptionItemOptions CreateSubscriptionItemOptions(
            SubscriptionPackage package, 
            int familyMembers, 
            BillingInterval billingInterval)
        {
            var priceId = GetStripePriceId(package, familyMembers, billingInterval);
            var quantity = GetStripeQuantity(package, familyMembers);

            return new Stripe.SubscriptionItemOptions
            {
                Price = priceId,
                Quantity = quantity,
                Metadata = new Dictionary<string, string>
                {
                    { "package_id", package.Id.ToString() },
                    { "package_name", package.Name },
                    { "family_members", familyMembers.ToString() },
                    { "billing_interval", billingInterval.ToString() }
                }
            };
        }

        /// <summary>
        /// Updates existing Stripe subscription for family size changes
        /// </summary>
        /// <param name="subscriptionId">Stripe subscription ID</param>
        /// <param name="package">The subscription package</param>
        /// <param name="newFamilyMembers">New number of family members</param>
        /// <param name="billingInterval">Monthly or Yearly</param>
        /// <returns>Updated Stripe subscription</returns>
        public async Task<Stripe.Subscription> UpdateFamilySubscriptionAsync(
            string subscriptionId,
            SubscriptionPackage package,
            int newFamilyMembers,
            BillingInterval billingInterval)
        {
            var service = new Stripe.SubscriptionService();
            var subscription = await service.GetAsync(subscriptionId);

            // Get the subscription item (assuming single item for family packages)
            var subscriptionItem = subscription.Items.Data.FirstOrDefault();
            if (subscriptionItem == null)
            {
                throw new InvalidOperationException("No subscription items found");
            }

            // Update the quantity
            var updateOptions = new Stripe.SubscriptionItemUpdateOptions
            {
                Quantity = GetStripeQuantity(package, newFamilyMembers),
                Metadata = new Dictionary<string, string>
                {
                    { "package_id", package.Id.ToString() },
                    { "package_name", package.Name },
                    { "family_members", newFamilyMembers.ToString() },
                    { "billing_interval", billingInterval.ToString() }
                }
            };

            var updatedItem = await new Stripe.SubscriptionItemService()
                .UpdateAsync(subscriptionItem.Id, updateOptions);

            return await service.GetAsync(subscriptionId);
        }

        /// <summary>
        /// Calculates the total cost for display purposes
        /// </summary>
        /// <param name="package">The subscription package</param>
        /// <param name="familyMembers">Number of family members</param>
        /// <param name="billingInterval">Monthly or Yearly</param>
        /// <returns>Total cost</returns>
        public decimal CalculateTotalCost(SubscriptionPackage package, int familyMembers, BillingInterval billingInterval)
        {
            if (package.UserType != UserType.Family)
            {
                return billingInterval switch
                {
                    BillingInterval.Month => package.MonthlyPrice,
                    BillingInterval.Year => package.YearlyPrice,
                    _ => 0
                };
            }

            // For family packages, calculate based on base price + additional members
            var basePrice = package.BasePrice ?? 0;
            var pricePerMember = package.PricePerAdditionalMember ?? 0;
            var additionalMembers = Math.Max(0, familyMembers - (package.MinFamilyMembers ?? 1));

            var totalPrice = basePrice + (additionalMembers * pricePerMember);

            return billingInterval switch
            {
                BillingInterval.Month => totalPrice,
                BillingInterval.Year => totalPrice * 12, // Assuming yearly is 12x monthly
                _ => 0
            };
        }
    }
}
