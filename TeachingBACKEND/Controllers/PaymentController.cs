using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ApplicationDbContext _context;

        public PaymentController(IPaymentService paymentService, ApplicationDbContext context)
        {
            _paymentService = paymentService;
            _context = context;
        }

        [Authorize]
        [HttpPost("checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentSessionRequestDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            var sessionId = await _paymentService.CreateCheckoutSessionAsync(dto, userId);
            return Ok(new { sessionId });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            try
            {
                await _paymentService.HandleStripeWebhookAsync(Request);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        ///<summary>
        ///Get payment Plans
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllPlans()
        {
            var plans = await _context.RegistrationPlans
                .Select(p => new
                {
                    p.Id,
                    Name = p.RegistrationPlanName,
                    p.Type,
                    p.Price
                }).ToListAsync();

            return Ok(plans);
        }

    }
}
