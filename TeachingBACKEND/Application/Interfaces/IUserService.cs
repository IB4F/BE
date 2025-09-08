using System.Security.Claims;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IUserService
    {
        // Note: Registration methods removed - registration now handled by AuthController using SubscriptionService
        //Task<UserResponseDTO> CreateStudentBySchool(CreateStudentBySchoolDTO model, Guid schoolId);
        Task<List<UserResponseDTO>> GetStudentsBySchool(Guid schoolId);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid Id);
        Task UpdateUser(User user);
        Task<LoginResponseDTO> Login(LoginDTO model);
        Task<string> Logout(Guid userId);
        Task<UserDetails> GetUserDetails(ClaimsPrincipal user);
        Task<bool> VerifyFamilyEmailAsync(Guid token);
        Task<List<object>> GetAvailableClasses();
    }
}
