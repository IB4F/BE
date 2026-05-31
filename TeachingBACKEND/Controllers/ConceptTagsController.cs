using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Api.Controllers;

[ApiController]
[Route("api/ConceptTags")]
public class ConceptTagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ConceptTagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _context.ConceptTags
            .AsNoTracking()
            .OrderBy(ct => ct.Name)
            .Select(ct => new ConceptTagDTO { Id = ct.Id, Name = ct.Name })
            .ToListAsync();

        return Ok(tags);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateConceptTagDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tag = new ConceptTag
        {
            Id = Guid.NewGuid(),
            Name = dto.Name.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _context.ConceptTags.Add(tag);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new ConceptTagDTO { Id = tag.Id, Name = tag.Name });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateConceptTagDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tag = await _context.ConceptTags.FindAsync(id);
        if (tag == null)
            return NotFound();

        tag.Name = dto.Name.Trim();
        await _context.SaveChangesAsync();

        return Ok(new ConceptTagDTO { Id = tag.Id, Name = tag.Name });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var tag = await _context.ConceptTags.FindAsync(id);
        if (tag == null)
            return NotFound();

        var usedInQuizzes = await _context.Quizzes
            .AnyAsync(q => q.ConceptTagId == id);

        var usedInMastery = await _context.UserConceptMastery
            .AnyAsync(m => m.ConceptTagId == id);

        if (usedInQuizzes || usedInMastery)
            return BadRequest(new { message = "Etiketa nuk mund të fshihet sepse është duke u përdorur në kuize ekzistuese. Hiq etiketën nga të gjitha kuizet para se ta fshish." });

        _context.ConceptTags.Remove(tag);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
