using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IQuizzService
    {
        Task<IEnumerable<Quizz>> GetAllAsync();
        Task<Quizz?> GetByIdAsync(Guid id);
        Task<Quizz> CreateAsync(Quizz model);
        Task<bool> UpdateAsync(Guid id, Quizz model);
        Task<bool> DeleteAsync(Guid id);
    }
}
