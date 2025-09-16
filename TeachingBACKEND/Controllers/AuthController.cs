using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using System.Text.Json;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Application.Services;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ISubscriptionService subscriptionService, ApplicationDbContext context, IPasswordService passwordService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _context = context;
            _passwordService = passwordService;
            _logger = logger;
        }

        /// <summary>
        /// Initiate student registration (subscription-first flow)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRegistrationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Validate the subscription package
                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == model.SubscriptionPackageId);

                if (package == null)
                {
                    return BadRequest(new { error = "Subscription package not found." });
                }

                if (package.UserType != UserType.Student)
                {
                    return BadRequest(new { error = "Selected package is not a student package." });
                }

                var subscriptionRequest = new SubscriptionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "student",
                    SubscriptionPackageId = model.SubscriptionPackageId,
                    RegistrationData = JsonSerializer.Serialize(model),
                    BillingInterval = BillingInterval.Month // Default to monthly, can be made configurable
                };

                var sessionId = await _subscriptionService.CreateSubscriptionAsync(subscriptionRequest);
                return Ok(new { 
                    message = "Student subscription initiated. Please complete payment to start your subscription.", 
                    sessionId = sessionId,
                    paymentUrl = $"https://checkout.stripe.com/pay/{sessionId}", // Frontend should redirect to this
                    packageName = package.Name
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Initiate school registration (subscription-first flow)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register-school")]
        public async Task<IActionResult> RegisterSchool([FromBody] SchoolRegistrationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Validate the subscription package
                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == model.SubscriptionPackageId);

                if (package == null)
                {
                    return BadRequest(new { error = "Subscription package not found." });
                }

                if (package.UserType != UserType.Supervisor)
                {
                    return BadRequest(new { error = "Selected package is not a school/supervisor package." });
                }

                var subscriptionRequest = new SubscriptionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "school",
                    SubscriptionPackageId = model.SubscriptionPackageId,
                    RegistrationData = JsonSerializer.Serialize(model),
                    BillingInterval = BillingInterval.Month // Default to monthly, can be made configurable
                };

                var sessionId = await _subscriptionService.CreateSubscriptionAsync(subscriptionRequest);
                return Ok(new {
                    message = "School subscription initiated. Please complete payment to start your subscription.",
                    sessionId = sessionId,
                    paymentUrl = $"https://checkout.stripe.com/pay/{sessionId}", // Frontend should redirect to this
                    note = "After payment, school and students will be created. Each student will receive a randomly generated password via email.",
                    packageName = package.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        /// <summary>
        /// Initiate family registration (subscription-first flow)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register-family")]
        public async Task<IActionResult> RegisterFamily([FromBody] FamilyRegistrationDTO model)
        {
            using var activity = _logger.BeginScope("RegisterFamily");
            
            try
            {
                _logger.LogInformation("Starting family registration for email: {Email} with {FamilyMemberCount} family members", 
                    model.Email, model.FamilyMembers?.Count ?? 0);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model validation failed for family registration. Errors: {@ModelState}", ModelState);
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Model validation passed for family registration");

                // Validate family member count (max 10)
                var totalFamilyMembers = (model.FamilyMembers?.Count ?? 0) + 1; // +1 for main user
                _logger.LogInformation("Total family members to register: {TotalFamilyMembers}", totalFamilyMembers);
                
                if (totalFamilyMembers > 10)
                {
                    _logger.LogWarning("Family member count exceeded maximum. Requested: {TotalFamilyMembers}, Max: 10", totalFamilyMembers);
                    return BadRequest(new { 
                        error = "Maximum 10 family members allowed. Please reduce the number of family members.",
                        currentCount = totalFamilyMembers,
                        maxAllowed = 10
                    });
                }

                if (totalFamilyMembers < 1)
                {
                    _logger.LogWarning("No family members provided. Total count: {TotalFamilyMembers}", totalFamilyMembers);
                    return BadRequest(new { 
                        error = "At least 1 family member is required.",
                        currentCount = totalFamilyMembers
                    });
                }

                // Log family member details
                if (model.FamilyMembers != null && model.FamilyMembers.Any())
                {
                    _logger.LogInformation("Family members to be created: {@FamilyMembers}", 
                        model.FamilyMembers.Select(fm => new { fm.FirstName, fm.LastName, fm.CurrentClass }));
                }

                // Get the subscription package to validate it's a family package
                _logger.LogInformation("Looking up subscription package with ID: {PackageId}", model.SubscriptionPackageId);
                var package = await _context.SubscriptionPackages
                    .FirstOrDefaultAsync(p => p.Id == model.SubscriptionPackageId);

                if (package == null)
                {
                    _logger.LogWarning("Subscription package not found with ID: {PackageId}", model.SubscriptionPackageId);
                    return BadRequest(new { error = "Subscription package not found." });
                }

                _logger.LogInformation("Found subscription package: {PackageName}, UserType: {UserType}, MinFamilyMembers: {MinFamilyMembers}, MaxFamilyMembers: {MaxFamilyMembers}", 
                    package.Name, package.UserType, package.MinFamilyMembers, package.MaxFamilyMembers);

                if (package.UserType != UserType.Family)
                {
                    _logger.LogWarning("Invalid package type. Expected: Family, Actual: {UserType}", package.UserType);
                    return BadRequest(new { error = "Selected package is not a family package." });
                }

                // Validate family member count against package limits
                if (totalFamilyMembers < package.MinFamilyMembers || totalFamilyMembers > package.MaxFamilyMembers)
                {
                    _logger.LogWarning("Family member count outside package limits. Requested: {TotalFamilyMembers}, Min: {MinFamilyMembers}, Max: {MaxFamilyMembers}", 
                        totalFamilyMembers, package.MinFamilyMembers, package.MaxFamilyMembers);
                    return BadRequest(new { 
                        error = $"Family members must be between {package.MinFamilyMembers} and {package.MaxFamilyMembers} for this package.",
                        currentCount = totalFamilyMembers,
                        minAllowed = package.MinFamilyMembers,
                        maxAllowed = package.MaxFamilyMembers
                    });
                }

                _logger.LogInformation("Creating subscription request for family registration");

                var subscriptionRequest = new SubscriptionRequestDTO
                {
                    Email = model.Email,
                    RegistrationType = "family",
                    SubscriptionPackageId = model.SubscriptionPackageId,
                    RegistrationData = JsonSerializer.Serialize(model),
                    BillingInterval = BillingInterval.Month, // Default to monthly, can be made configurable
                    FamilyMemberCount = totalFamilyMembers
                };

                _logger.LogInformation("Calling CreateSubscriptionAsync for family registration");

                var sessionId = await _subscriptionService.CreateSubscriptionAsync(subscriptionRequest);
                
                _logger.LogInformation("Family registration initiated successfully. Session ID: {SessionId}", sessionId);
                
                return Ok(new {
                    message = "Family subscription initiated. Please complete payment to start your subscription.",
                    sessionId = sessionId,
                    paymentUrl = $"https://checkout.stripe.com/pay/{sessionId}", // Frontend should redirect to this
                    note = "After payment, family members will be created and verification email will be sent.",
                    familyMemberCount = totalFamilyMembers,
                    packageName = package.Name
                }); 
            }
            catch (Exception ex)
            {
                // Log the full exception details for debugging
                _logger.LogError(ex, "Error in register-family endpoint for email: {Email}. " +
                    "Family members count: {FamilyMemberCount}. Package ID: {PackageId}. " +
                    "Exception: {ExceptionMessage}. Inner Exception: {InnerExceptionMessage}. " +
                    "Stack Trace: {StackTrace}", 
                    model.Email, 
                    model.FamilyMembers?.Count ?? 0, 
                    model.SubscriptionPackageId,
                    ex.Message,
                    ex.InnerException?.Message ?? "None",
                    ex.StackTrace);
                
                // Return more detailed error information
                var errorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += $" Inner Exception: {ex.InnerException.Message}";
                }
                
                return BadRequest(new { 
                    message = "An error occurred while saving the entity changes. See the inner exception for details.",
                    details = errorMessage,
                    stackTrace = ex.StackTrace
                }); 
            }
        }
        
        [AllowAnonymous]
        [HttpGet("available-classes")]
        public async Task<IActionResult> GetAvailableClasses()
        {
            try
            {
                var classes = await _userService.GetAvailableClasses();
                return Ok(classes);
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

            var message = await _passwordService.RequestPasswordReset(model.Email);
            return Ok(new { message = message });
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

        /// <summary>
        /// Change password for authenticated user
        /// </summary>
        [Authorize]
        [HttpPost("change-password")]
        [SwaggerOperation(Summary = "Change Password for Authenticated User")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Get user ID from JWT token
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
                var result = await _passwordService.ChangePassword(userId, model.CurrentPassword, model.NewPassword);
                
                if (result == "Password changed successfully.")
                {
                    return Ok(new { message = result });
                }
                
                return BadRequest(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password for user");
                return BadRequest(new { message = "An error occurred while changing the password." });
            }
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

        [AllowAnonymous]
        [HttpGet("verify-FamilyEmail")]
        public async Task<IActionResult> VerifyFamilyEmail([FromQuery] Guid token)
        {
            var result = await _userService.VerifyFamilyEmailAsync(token);
            if (result)
            {
                return Ok(new { message = "Email verified successfully." });
            }
            return BadRequest(new { message = "Invalid or already verified token." });
        }

        [AllowAnonymous]
        [HttpPost("resend-verification")]
        public async Task<IActionResult> ResendVerification([FromBody] string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user == null)
                return NotFound(new { message = "User not found." });

            if (user.IsEmailVerified)
                return BadRequest(new { message = "Email is already verified." });

            var newToken = _passwordService.GenerateVerificationToken();
            user.EmailVerificationToken = newToken;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Verification resent" });
        }
    }
}
