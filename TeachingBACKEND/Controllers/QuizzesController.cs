﻿using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly ILearnHubService _learnHubService;

    public QuizzesController(ILearnHubService learnHubService)
    {
        _learnHubService = learnHubService;
    }

    [HttpPost("Post-Quiz")]
    public async Task<IActionResult> PostQuizz(Guid linkId, [FromBody] CreateQuizzDTO dto)
    {
        var id = await _learnHubService.PostQuizz(linkId, dto);
        return Ok(id);
    }

    [HttpGet("Get-List-Quizzes")]
    public async Task<IActionResult> GetQuizzesByLinkId(Guid linkId)
    {
        var result = await _learnHubService.GetQuizzesByLinkId(linkId);
        return Ok(result);
    }

    [HttpGet("Get-Single-Quiz")]
    public async Task<IActionResult> GetQuizz([FromQuery] Guid id)
    {
        var result = await _learnHubService.GetQuizzByIdDTOAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPut("Update-Quiz")]
    public async Task<IActionResult> UpdateQuizz([FromQuery] Guid id, [FromBody] CreateQuizzDTO dto)
    {
        var updated = await _learnHubService.UpdateQuizz(id, dto);
        return Ok(updated);
    }

    [HttpDelete("Delete-Quiz")]
    public async Task<IActionResult> DeleteQuizz([FromQuery] Guid id)
    {
        await _learnHubService.DeleteQuizz(id);
        return Ok("Quiz deleted");
    }

    [HttpPost("Get-Paginated-Quizzes")]
    public async Task<IActionResult> GetPaginatedQuizzes(Guid linkId, [FromBody] PaginationRequestDTO dto)
    {
        if (dto.PageNumber < 0 || dto.PageSize <= 0)
            return BadRequest(new { error = "PageNumber must be >= 0 and PageSize must be > 0." });

        var result = await _learnHubService.GetPaginatedQuizzesAsync(linkId, dto);
        return Ok(result);
    }

}