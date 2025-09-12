using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetStudentDashboardAsync(Guid studentId);
    }
}
