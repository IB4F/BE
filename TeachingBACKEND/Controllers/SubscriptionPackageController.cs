using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Application.Services;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPackageController : ControllerBase
    {
        private readonly ISubscriptionPackageService _packageService;
        private readonly FamilyPricingService _familyPricingService;

        public SubscriptionPackageController(
            ISubscriptionPackageService packageService,
            FamilyPricingService familyPricingService)
        {
            _packageService = packageService;
            _familyPricingService = familyPricingService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllPackages()
        {
            var packages = await _packageService.GetAllPackagesAsync();
            var simplifiedPackages = packages
                .Where(p => p.IsActive)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

            return Ok(simplifiedPackages);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionPackage>> GetPackage(Guid id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package == null)
                return NotFound("Package not found.");

            return Ok(package);
        }

        [AllowAnonymous]
        [HttpGet("user-type/{userType}")]
        public async Task<ActionResult<IEnumerable<SubscriptionPackage>>> GetPackagesByUserType(string userType)
        {
            if (!Enum.TryParse<UserType>(userType, true, out var userTypeEnum))
                return BadRequest("Invalid user type. Must be one of: Student, Family, Supervisor.");

            var packages = await _packageService.GetPackagesByUserTypeAsync(userTypeEnum);
            return Ok(packages);
        }

        [AllowAnonymous]
        [HttpGet("tier/{tier}")]
        public async Task<ActionResult<IEnumerable<SubscriptionPackage>>> GetPackagesByTier(string tier)
        {
            if (!Enum.TryParse<PackageTier>(tier, true, out var tierEnum))
                return BadRequest("Invalid tier. Must be one of: Basic, Standard, Premium.");

            var packages = await _packageService.GetPackagesByTierAsync(tierEnum);
            return Ok(packages);
        }

        [AllowAnonymous]
        [HttpGet("billing-interval/{billingInterval}")]
        public async Task<ActionResult<IEnumerable<SubscriptionPackage>>> GetPackagesByBillingInterval(string billingInterval)
        {
            if (!Enum.TryParse<BillingInterval>(billingInterval, true, out var billingIntervalEnum))
                return BadRequest("Invalid billing interval. Must be one of: Day, Week, Month, Year.");

            var packages = await _packageService.GetPackagesByBillingIntervalAsync(billingIntervalEnum);
            return Ok(packages);
        }

        [AllowAnonymous]
        [HttpGet("family/{tier}/{billingInterval}")]
        public async Task<ActionResult<FamilyPricingResponseDTO>> GetFamilyPackageWithPricing(
            string tier, 
            string billingInterval, 
            [FromQuery] int familyMembers = 1)
        {
            if (!Enum.TryParse<PackageTier>(tier, true, out var tierEnum))
                return BadRequest("Invalid tier. Must be one of: Basic, Standard, Premium.");

            if (!Enum.TryParse<BillingInterval>(billingInterval, true, out var billingIntervalEnum))
                return BadRequest("Invalid billing interval. Must be one of: Day, Week, Month, Year.");

            var package = await _packageService.GetFamilyPackageAsync(tierEnum, billingIntervalEnum);
            if (package == null)
                return NotFound("Family package not found.");

            try
            {
                var pricing = _familyPricingService.CalculateFamilyPrice(package, familyMembers);
                return Ok(pricing);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("family/calculate-price")]
        public async Task<ActionResult<IEnumerable<FamilyPricingResponseDTO>>> CalculateFamilyPrice(
            [FromBody] FamilyPricingRequestDTO request)
        {
            try
            {
                // Get all family packages (both monthly and yearly)
                var familyPackages = (await _packageService.GetPackagesByUserTypeAsync(UserType.Family)).ToList();
                
                if (!familyPackages.Any())
                    return NotFound("No family packages found.");

                var pricingResults = new List<FamilyPricingResponseDTO>();

                foreach (var package in familyPackages)
                {
                    try
                    {
                        var pricing = _familyPricingService.CalculateFamilyPrice(package, request.FamilyMembers);
                        pricingResults.Add(pricing);
                    }
                    catch (ArgumentException)
                    {
                        // Skip packages that don't meet the family member requirements
                        continue;
                    }
                }

                if (!pricingResults.Any())
                    return BadRequest($"No family packages available for {request.FamilyMembers} family members.");

                return Ok(pricingResults);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error calculating family pricing: {ex.Message}");
            }
        }

        

        // NEW: Get available tiers for LearnHub creation
        [AllowAnonymous]
        [HttpGet("Get-Tiers")]
        public IActionResult GetTiers()
        {
            var tiers = new[]
            {
                new { Value = 1, Name = "Basic" },
                new { Value = 2, Name = "Standard" },
                new { Value = 3, Name = "Premium" }
            };

            return Ok(tiers);
        }
    }
}