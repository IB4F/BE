using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public AuthController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        /// <summary>
        /// Register a student
        /// </summary>
        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegistrationDTO model)
        {

            try
            {
                var response = await _userService.RegisterStudent(model);
                return Created("", new { message = "Student registered successfully", userId = response.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return BadRequest(new { error = ex.Message });
            }

        }

        /// <summary>
        /// Register a school
        /// </summary>
        [HttpPost("register-school")]
        public async Task<IActionResult> RegisterSchool([FromBody] SchoolRegistrationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _userService.RegisterSchool(model);
                return Created("", new { message = "School registered successfully", userId = response.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] Guid token)
        {
            var result = await _userService.VerifyEmail(token);
            if(result == "Email verified successfully.")
            {
                return Ok(new {message = result});
            }
            return BadRequest(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                string token = await _userService.Login(model);
                return Ok(new {Token = token});
            }
            catch (Exception ex)
            {

                return BadRequest(new {Message = ex.Message});  
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound(new { error = "User not found" });
                }

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.School = updatedUser.School;
                user.City = updatedUser.City;

                await _userService.UpdateUser(user);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //Request password reset(step 1)
        [HttpPost("request-reset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ForgotPasswordDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.RequestPasswordReset(model.Email);
            if (string.IsNullOrEmpty(result)) return NotFound("User with the specified email does not exist.");

            return Ok("Password reset link has been sent to your email.");
        }


        //Reset Password(step 2)
        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.ResetPassword(model.Token, model.NewPassword);
            if (string.IsNullOrEmpty(result)) return BadRequest("Invalid token or the token has expired");

            return Ok("Password has been successfully reset");
        }

    }
}
