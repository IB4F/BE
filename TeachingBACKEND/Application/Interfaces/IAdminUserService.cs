using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IAdminUserService
    {
        Task<List<UserListDTO>> GetAllUsersAsync();
        Task<AdminUserDetailsDTO> GetUserDetailsById(Guid id);
        Task UpdateUser(AdminUserDetailsDTO dto);
        Task DeleteUserAsync(Guid id);
    }
}
