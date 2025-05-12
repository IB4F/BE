using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailsService _detailService;

        public DetailsController(IDetailsService detailService)
        {
            _detailService = detailService;
        }

        [HttpGet("get-cities")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            var cityList = await _detailService.GetCities();

            if (!cityList.Any())
                return NotFound("No cities found.");

            return Ok(cityList);
        }

        [HttpGet("get-class")]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            var classList = await _detailService.GetClasses();

            if (!classList.Any())
                return NotFound("No cities found.");

            return Ok(classList);
        }
    }

}
