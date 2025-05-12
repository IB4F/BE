using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IDetailsService
    {
        Task<IEnumerable<City>> GetCities();
        Task<IEnumerable<Class>> GetClasses();
    }
}
