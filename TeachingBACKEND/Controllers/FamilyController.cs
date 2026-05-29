using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/family")]
    [Authorize(Roles = "Family")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _familyService;
        private readonly ILogger<FamilyController> _logger;

        public FamilyController(IFamilyService familyService, ILogger<FamilyController> logger)
        {
            _familyService = familyService;
            _logger = logger;
        }

        [HttpPost("children/bulk")]
        public async Task<IActionResult> CreateChildrenBulk([FromBody] CreateChildrenBulkRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var result = await _familyService.CreateChildrenBulkAsync(parentId, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating children bulk for parent {ParentId}", parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpPost("children")]
        public async Task<IActionResult> CreateChild([FromBody] CreateChildInputDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var result = await _familyService.CreateChildAsync(parentId, dto);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating child for parent {ParentId}", parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpGet("children")]
        public async Task<IActionResult> GetChildren()
        {
            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var children = await _familyService.GetChildrenAsync(parentId);
                return Ok(new { children });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting children for parent {ParentId}", parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpPatch("children/{childId}")]
        public async Task<IActionResult> UpdateChild(Guid childId, [FromBody] UpdateChildDto dto)
        {
            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var result = await _familyService.UpdateChildAsync(parentId, childId, dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating child {ChildId} for parent {ParentId}", childId, parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpPost("children/{childId}/password-reset")]
        public async Task<IActionResult> ResetChildPassword(Guid childId)
        {
            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var result = await _familyService.ResetChildPasswordAsync(parentId, childId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting password for child {ChildId} by parent {ParentId}", childId, parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpDelete("children/{childId}")]
        public async Task<IActionResult> DeleteChild(Guid childId)
        {
            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                await _familyService.DeleteChildAsync(parentId, childId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting child {ChildId} for parent {ParentId}", childId, parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        [HttpPost("children/{childId}/reactivate")]
        public async Task<IActionResult> ReactivateChild(Guid childId)
        {
            if (!TryGetParentId(out var parentId))
                return Unauthorized();

            try
            {
                var result = await _familyService.ReactivateChildAsync(parentId, childId);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reactivating child {ChildId} for parent {ParentId}", childId, parentId);
                return StatusCode(500, new { message = "An internal error occurred." });
            }
        }

        private bool TryGetParentId(out Guid parentId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out parentId);
        }
    }
}
