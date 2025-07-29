using System.Security.Claims;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterStudent(StudentRegistrationDTO model);
        Task<UserResponseDTO> RegisterSchool(SchoolRegistrationDTO model);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid Id);
        Task UpdateUser(User user);
        Task<LoginResponseDTO> Login(LoginDTO model);
        Task<string> Logout(Guid userId);
        Task<UserDetails> GetUserDetails(ClaimsPrincipal user);
        Task<UserResponseDTO> RegisterFamily(FamilyRegistrationDTO model);
    }
}
