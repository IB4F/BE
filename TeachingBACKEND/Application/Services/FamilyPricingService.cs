using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class FamilyPricingService
    {
        public FamilyPricingResponseDTO CalculateFamilyPrice(
            SubscriptionPackage package, 
            int familyMembers)
        {
            if (package.UserType != UserType.Family)
                throw new ArgumentException("Package must be a family package");

            if (package.BasePrice == null || package.PricePerAdditionalMember == null)
                throw new ArgumentException("Family package must have base price and price per additional member");

            var basePrice = package.BasePrice.Value;
            var pricePerMember = package.PricePerAdditionalMember.Value;
            var minMembers = package.MinFamilyMembers ?? 1;
            var maxMembers = package.MaxFamilyMembers ?? 10;

            // Validate family size
            if (familyMembers < minMembers)
                throw new ArgumentException($"Minimum family size is {minMembers}");

            if (familyMembers > maxMembers)
                throw new ArgumentException($"Maximum family size is {maxMembers}");

            // Calculate pricing
            var additionalMembers = Math.Max(0, familyMembers - minMembers);
            var additionalCost = additionalMembers * pricePerMember;
            var totalPrice = basePrice + additionalCost;

            // Create response
            var response = new FamilyPricingResponseDTO
            {
                PackageId = package.Id,
                Name = package.Name,
                Tier = package.Tier,
                BillingInterval = package.BillingInterval,
                BasePrice = basePrice,
                PricePerMember = pricePerMember,
                FamilyMembers = familyMembers,
                AdditionalMembers = additionalMembers,
                AdditionalCost = additionalCost,
                TotalPrice = totalPrice,
                MaxMembers = maxMembers,
                MinMembers = minMembers,
                Breakdown = GenerateBreakdown(basePrice, pricePerMember, familyMembers, additionalMembers, package.BillingInterval),
                BasePriceFormatted = FormatPrice(basePrice),
                PricePerMemberFormatted = FormatPrice(pricePerMember),
                AdditionalCostFormatted = FormatPrice(additionalCost),
                TotalPriceFormatted = FormatPrice(totalPrice)
            };

            return response;
        }

        private string GenerateBreakdown(long basePrice, long pricePerMember, int familyMembers, int additionalMembers, BillingInterval billingInterval)
        {
            var basePriceFormatted = FormatPrice(basePrice);
            var pricePerMemberFormatted = FormatPrice(pricePerMember);
            var additionalCostFormatted = FormatPrice(additionalMembers * pricePerMember);
            var totalPriceFormatted = FormatPrice(basePrice + (additionalMembers * pricePerMember));

            var interval = billingInterval == BillingInterval.Month ? "month" : "year";

            if (additionalMembers == 0)
            {
                return $"Base price: {basePriceFormatted}/{interval} for {familyMembers} family member{(familyMembers > 1 ? "s" : "")} = {totalPriceFormatted}/{interval}";
            }

            return $"Base: {basePriceFormatted} + {additionalMembers} additional member{(additionalMembers > 1 ? "s" : "")} × {pricePerMemberFormatted} = {totalPriceFormatted}/{interval}";
        }

        private string FormatPrice(long priceInCents)
        {
            return $"€{(priceInCents / 100.0):F2}";
        }
    }
}
