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
        Task<string> Login(LoginDTO model);
        Task<string> VerifyEmail(Guid? token);
        Task<string> ResetPassword(Guid token, string newPassword);
        Task<string> RequestPasswordReset(string email);
    }
}
