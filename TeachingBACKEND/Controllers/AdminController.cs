using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using System;

namespace TeachingBACKEND.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/admin/users")]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpPost("paginated")]
        public async Task<IActionResult> GetAllPaginated([FromBody] PaginationRequestDTO pagination)
        {
            if (pagination.PageNumber < 0 || pagination.PageSize < 0)
            {
                return BadRequest(new { error = "PageNumber and PageSize must be greater than zero." });
            }

            var result = await _adminUserService.GetAllUsers(pagination);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _adminUserService.GetUserDetailsById(id);
                return Ok(user); 
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AdminUserDetailsDTO dto)
        {
            try
            {
                dto.Id = id; 
                await _adminUserService.UpdateUser(dto);
                return Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _adminUserService.DeleteUserAsync(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
