using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinksController : ControllerBase
    {
        private readonly ILearnHubService _learnHubService;

        public LinksController(ILearnHubService learnHubService)
        {
            _learnHubService = learnHubService;
        }

        [HttpPost("{learnHubId}/Post--Link")]
        public async Task<IActionResult> PostLinkToLearnHub(Guid learnHubId, [FromBody] CreateLinkDTO dto)
        {
            var linkId = await _learnHubService.PostLink(learnHubId, dto);
            return Ok(new { Id = linkId });
        }


        [HttpGet("Get-List-Links")]
        public async Task<IActionResult> GetAllLinks()
        {
            var result = await _learnHubService.GetAllLinksAsync();
            return Ok(result);
        }

        [HttpGet("Get-Single-Link")]
        public async Task<IActionResult> GetLink([FromQuery] Guid id)
        {
            var result = await _learnHubService.GetLinkByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("Update-Link")]
        public async Task<IActionResult> UpdateLink([FromQuery] Guid id, [FromBody] CreateLinkDTO dto)
        {
            var updated = await _learnHubService.UpdateLink(id, dto);
            return Ok(updated);
        }

        [HttpDelete("Delete-Link")]
        public async Task<IActionResult> DeleteLink([FromQuery] Guid id)
        {
            await _learnHubService.DeleteLink(id);
            return Ok(new { message = "Link u fshi" }); 
        }

    }
}
