using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IStudentProgressService
    {
        /// <summary>
        /// Calculates the overall progress percentage for a student
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>Progress percentage (0-100)</returns>
        Task<double> CalculateStudentProgressAsync(Guid studentId);

        /// <summary>
        /// Calculates progress for multiple students (for supervisor dashboard)
        /// </summary>
        /// <param name="studentIds">List of student IDs</param>
        /// <returns>Dictionary of student ID to progress percentage</returns>
        Task<Dictionary<Guid, double>> CalculateStudentsProgressAsync(IEnumerable<Guid> studentIds);

        /// <summary>
        /// Gets detailed progress breakdown for a student
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <returns>Detailed progress information</returns>
        Task<StudentProgressDetailDTO> GetStudentProgressDetailAsync(Guid studentId);

        /// <summary>
        /// Updates or creates performance summary for a student after quiz completion
        /// </summary>
        /// <param name="studentId">The student's ID</param>
        /// <param name="linkId">The link ID</param>
        /// <returns>Task</returns>
        Task UpdateStudentPerformanceSummaryAsync(Guid studentId, Guid linkId);

        /// <summary>
        /// Gets progress statistics for a supervisor's students
        /// </summary>
        /// <param name="supervisorId">The supervisor's ID</param>
        /// <returns>Progress statistics</returns>
        Task<SupervisorProgressStatsDTO> GetSupervisorProgressStatsAsync(Guid supervisorId);
    }
}
