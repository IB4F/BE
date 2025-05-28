using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearnHubsController : ControllerBase
    {
        private readonly ILearnHubService _learnHubService;

        public LearnHubsController(ILearnHubService learnHubService)
        {
            _learnHubService = learnHubService;
        }

        [HttpPost("Post-Learnhub")]
        public async Task<IActionResult> PostLearnHub(LearnHubCreateDTO learnHub)
        {
            var newLearnHub = await _learnHubService.PostLearnHub(learnHub);
            return Ok(newLearnHub);
        }

        [HttpGet("Get-List-Learnhubs")]
        public async Task<IActionResult> GetAllLearnHubs()
        {
            var learnHubs = await _learnHubService.GetLearnHubs();
            return Ok(learnHubs);
        }

        [HttpGet("Get-Single-Learnhub")]
        public async Task<IActionResult> GetLearnHub(Guid id)
        {
            var learnHubs = await _learnHubService.GetSingleLearnHub(id);
            return Ok(learnHubs);
        }

        [HttpPut("Update-Learnhub")]
        public async Task<IActionResult> UpdateLearnHub(Guid id, LearnHubCreateDTO dto)
        {
            var learnHubs = await _learnHubService.UpdateLearnHub(id, dto);
            return Ok(learnHubs);
        }

        [HttpDelete("Delete-Learnhub")]
        public async Task<IActionResult> DeleteLearnHub(Guid id)
        {
            await _learnHubService.DeleteLearnHub(id);
            return Ok("LearnHub deleted");
        }

       
       

    }
}
