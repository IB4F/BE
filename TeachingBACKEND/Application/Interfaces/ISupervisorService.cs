using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface ISupervisorService
    {
        // Supervisor Application Management
        Task<Guid> SubmitSupervisorApplication(SupervisorApplicationDTO model);
        Task<bool> ApproveSupervisor(SupervisorApprovalDTO model);
        Task<List<SupervisorApplicationDTO>> GetPendingSupervisorApplications();
        
        // Student Management
        Task<StudentCreatedResponseDTO> CreateStudent(CreateStudentBySupervisorDTO model, Guid supervisorId);
        Task<List<StudentCreatedResponseDTO>> GetSupervisedStudents(Guid supervisorId);
        Task<PaginatedResponseDTO<StudentCreatedResponseDTO>> GetSupervisedStudentsPaged(Guid supervisorId, int page, int pageSize);
        Task<bool> DeleteStudent(Guid studentId, Guid supervisorId);
        
        // Password Reset Management
        Task<bool> HandlePasswordResetRequest(Guid studentId, bool approve);
        Task<List<PasswordResetRequestDTO>> GetPendingPasswordResetRequests(Guid supervisorId);
        
        // Dashboard
    Task<SupervisorDashboardDTO> GetDashboardData(Guid supervisorId);
    Task<StudentProgressDetailDTO> GetStudentProgressDetail(Guid studentId);
        
        // Utility Methods
        Task<bool> IsSupervisorApproved(Guid supervisorId);
        Task<int> GetStudentCount(Guid supervisorId);
        Task<bool> CanCreateMoreStudents(Guid supervisorId);
    }
}

