using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IAdminUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserById(Guid id);
        Task UpdateUser(User user);
        Task DeleteUserAsync(Guid id);
    }
}
