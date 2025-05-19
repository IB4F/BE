using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Services;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;


namespace TeachingBACKEND.Controllers
{
    [Authorize]
    public class LearnHubController : ControllerBase
    {
        private readonly ILearnHubService _learnHubService;

        public LearnHubController(ILearnHubService learnHubService)
        {
            _learnHubService = learnHubService;
        }


        //Admin: Create a new Learn Hub
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/admin/learnhub")]
        public async Task<IActionResult> Create([FromBody] CreateLearnHubDTO model)
        {
            if (ModelState.IsValid)
            {
                await _learnHubService.CreateLearnHubAsync(model);
                return Ok(new { message = "LearnHub created successfully" });
            }

            return BadRequest();
        }


        // Student/Supervisor: View Learn Hub details
        [HttpGet]
        [Authorize(Roles = "Student, Supervisor")]
        [Route("api/learnhub/{id}")]
        public async Task<IActionResult> ViewLearnHub(Guid id)
        {
            var learnHub = await _learnHubService.GetLearnHubByIdAsync(id);
            if (learnHub == null) return NotFound(new { message = "LearnHub not found" });
            return Ok(learnHub);
        }


        // Public: View all free Learn Hubs
        [HttpGet]
        [Route("api/learnhub/free")]
        public async Task<IActionResult> ViewFreeLearnHubs()
        {
            var freeLearnHubs = await _learnHubService.GetAllFreeLearnHubAsync();
            return Ok(freeLearnHubs);
        }
    }
}
