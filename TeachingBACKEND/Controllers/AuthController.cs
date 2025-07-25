﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Application.Services;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;

        public AuthController(IUserService userService, ApplicationDbContext context, IPasswordService passwordService)
        {
            _userService = userService;
            _context = context;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Register a student
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegistrationDTO model)
        {

            try
            {
                var response = await _userService.RegisterStudent(model);
                return Created("", new { message = "Student registered successfully", userId = response.Id, sessionId = response.SessionId });
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
        [AllowAnonymous]
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


        [AllowAnonymous]
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] Guid token)
        {
            var result = await _passwordService.VerifyEmail(token);
            if (result == "Email verified successfully.")
            {
                return Ok(new { message = result });
            }
            return BadRequest(new { message = result });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                LoginResponseDTO response = await _userService.Login(model);
                return Ok(new 
                {
                    Message = "Login të suksesshëm!",
                    Data = response
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get user by ID")]
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
        [SwaggerOperation(Summary = "Update user by ID")]
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
        [SwaggerOperation(Summary = "Request Password Reset Step 1")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] ForgotPasswordDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _passwordService.RequestPasswordReset(model.Email);
            return Ok(new { message = "If an account with that email exists, a password reset link has been sent." });
        }


        //Reset Password(step 2)
        [HttpPost("reset")]
        [SwaggerOperation(Summary = "Password Reset Step 2")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _passwordService.ResetPassword(model.Token, model.NewPassword);
            if (result == "Invalid or expired token")
                return BadRequest(new { message = result });
            if (result == "New password must be different from the old one.")
                return BadRequest(new { message = result });
            return Ok(new { message = result });
        }



        [HttpPost("schools/set-password")]
        public async Task<IActionResult> SetSchoolPassword([FromBody] SetPasswordDTO model)
        {
            var result = await _passwordService.GeneratePasswordForApprovedSchool(model.SchoolId, model.Password);
            return Ok(new { message = result });

        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // read userId from JWT
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var msg = await _userService.Logout(userId);
            return Ok(new { message = msg });
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO model)
        {
            try
            {
                var tokens = await _passwordService.RefreshTokenAsync(model);
                return Ok(tokens);
            }
            catch (Exception ex)
            {

                return BadRequest(new {error = ex.Message});
            }
        }


        [Authorize]
        [HttpGet("me")]
        [SwaggerOperation(Summary = "Get Current User Details")]
        public async Task <IActionResult> GetCurrentUser()
        {
            try
            {
                var dto = await _userService.GetUserDetails(User);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Unauthorized(new {message = ex.Message});
                
            }
        }
    }
}
