using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] PaymentSessionRequestDTO dto)
        {
            var sessionId = await _paymentService.CreateCheckoutSessionAsync(dto);
            return Ok (new {sessionId });
        }

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



    }
}
