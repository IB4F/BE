using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IAdminUserService
    {
        Task<PaginatedResultDTO<UserListDTO>> GetAllUsers(PaginationRequestDTO dto);
        Task<AdminUserDetailsDTO> GetUserDetailsById(Guid id);
        Task UpdateUser(AdminUserDetailsDTO dto);
        Task DeleteUserAsync(Guid id);
    }
}
