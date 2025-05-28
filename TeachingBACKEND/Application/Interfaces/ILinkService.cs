using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ILinkService
    {
        Task<IEnumerable<Link>> GetAllAsync();
        Task<Link?> GetByIdAsync(Guid id);
        Task<Link> CreateAsync(Link model);
        Task<bool> UpdateAsync(Guid id, Link model);
        Task<bool> DeleteAsync(Guid id);
    }
}
