using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IDetailsService
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Class>> GetClasses();
        Task<IEnumerable<Subjects>> GetSubjects();
        Task<List<RegistrationPlan>> GetAllPlansAsync();
        Task<RegistrationPlan?> GetPlanByIdAsync(Guid id);
    }
}
