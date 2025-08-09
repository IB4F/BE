using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetailsController : ControllerBase
{
    private readonly IDetailsService _detailService;
    private readonly ApplicationDbContext _context;


    public DetailsController(IDetailsService detailService, ApplicationDbContext context)
    {
        _detailService = detailService;
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet("get-cities")]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        var cityList = await _detailService.GetCities();

        if (!cityList.Any())
            return NotFound("No cities found.");

        return Ok(cityList);
    }

    [AllowAnonymous]
    [HttpGet("get-class")]
    public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
    {
        var classList = await _detailService.GetClasses();

        if (!classList.Any())
            return NotFound("No cities found.");

        return Ok(classList);
    }

    [AllowAnonymous]
    [HttpGet("get-subjects")]
    public async Task<ActionResult<IEnumerable<Subjects>>> GetSubjects()
    {
        var subjectsList = await _detailService.GetSubjects();

        if (!subjectsList.Any())
            return NotFound("No subjects found.");

        return Ok(subjectsList);
    }
    [HttpGet("get-plans")]
    public async Task<ActionResult<IEnumerable<RegistrationPlan>>> GetPlans()
    {
        var plans = await _detailService.GetAllPlansAsync();
        return Ok(plans);
    }

    [HttpGet("get-plans{id}")]
    public async Task<ActionResult<RegistrationPlan>> GetPlan(Guid id)
    {
        var plan = await _detailService.GetPlanByIdAsync(id);
        if (plan == null)
            return NotFound("Plan not found.");

        return Ok(plan);
    }


    [HttpGet("get-plans/{userType}")]
    public async Task<IActionResult> GetPlansByUserType(string userType)
    {
        var normalizedType = userType.ToLower();
        var allowedTypes = new[] { "student", "family", "supervisor" };

        if (!allowedTypes.Contains(normalizedType))
            return BadRequest("Invalid user type. Must be one of: student, family, supervisor.");

        var plans = await _context.RegistrationPlans
            .Where(p => p.UserType.ToLower() == normalizedType)
            .ToListAsync();

        return Ok(plans);
    }
}