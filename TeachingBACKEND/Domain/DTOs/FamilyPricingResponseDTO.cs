using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Domain.DTOs
{
    public class FamilyPricingResponseDTO
    {
        public Guid PackageId { get; set; }
        public string Name { get; set; }
        public PackageTier Tier { get; set; }
        public BillingInterval BillingInterval { get; set; }
        
        // Pricing Details
        public long BasePrice { get; set; }                 // Base price in cents
        public long PricePerMember { get; set; }            // Price per additional member in cents
        public int FamilyMembers { get; set; }              // Total family members
        public int AdditionalMembers { get; set; }          // Members beyond the base (usually 1)
        public long AdditionalCost { get; set; }            // Additional cost in cents
        public long TotalPrice { get; set; }                // Total price in cents
        
        // Limits
        public int MaxMembers { get; set; }
        public int MinMembers { get; set; }
        
        // Human-readable breakdown
        public string Breakdown { get; set; }
        
        // Formatted prices for display
        public string BasePriceFormatted { get; set; }
        public string PricePerMemberFormatted { get; set; }
        public string AdditionalCostFormatted { get; set; }
        public string TotalPriceFormatted { get; set; }
    }
}
