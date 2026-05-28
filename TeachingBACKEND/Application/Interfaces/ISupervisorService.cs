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
        Task<UpdatedStudentDTO> UpdateStudentAsync(Guid studentId, Guid supervisorId, UpdateStudentBySupervisorDTO dto);
        
        // Password Reset Management
        Task<PasswordResetApprovalResultDTO> HandlePasswordResetRequest(Guid studentId, bool approve, Guid supervisorId);
        Task<List<PasswordResetRequestDTO>> GetPendingPasswordResetRequests(Guid supervisorId);
        
        // Dashboard
    Task<SupervisorDashboardDTO> GetDashboardData(Guid supervisorId);
    Task<StudentProgressDetailDTO> GetStudentProgressDetail(Guid studentId);
        
        // Utility Methods
        Task<bool> IsSupervisorApproved(Guid supervisorId);
        Task<int> GetStudentCount(Guid supervisorId);
        Task<bool> CanCreateMoreStudents(Guid supervisorId);
        Task<Domain.Entities.User> CreateSupervisorFromApprovedApplicationAsync(Guid supervisorApplicationId);
    }
}

