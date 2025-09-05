using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.DTOs.Quizzes;
using TeachingBACKEND.Domain.Entities;

public interface ILearnHubService
{
    Task<Guid> PostLearnHub(LearnHubCreateDTO dto);
    Task<List<LearnHubDTO>> GetLearnHubs();
    Task<GetSingleLearnHub> GetSingleLearnHub(Guid id);
    Task<LearnHubDTO> UpdateLearnHub(Guid id, LearnHubCreateDTO dto);
    Task DeleteLearnHub(Guid id);
    Task<PaginatedResultDTO<PaginationLearnHubDTO>> GetPaginatedLearnHubs(PaginationRequestDTO dto);
    Task<List<FilteredLearnHubDTO>> GetFilteredLearnHubs(string classType, string subject, bool isAuthenticated = false, Guid? userId = null);
    Task MigrateLearnHubClassTypes();

    // Link
    Task<Guid> PostLink(Guid learnHubId, CreateLinkDTO dto);
    Task<List<Link>> GetAllLinksAsync();
    Task<Link> GetLinkByIdAsync(Guid id);
    Task<Link> UpdateLink(Guid id, CreateLinkDTO dto);
    Task DeleteLink(Guid id);

    // Quiz
    Task<Guid> PostQuizz(Guid linkId, CreateQuizzDTO dto);
    Task<List<QuizDTO>> GetQuizzesByLinkId(Guid linkId);
    Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id);
    Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto);
    Task DeleteQuizz(Guid id);
    Task<PaginatedResultDTO<SimpleQuizDTO>> GetPaginatedQuizzesAsync(Guid linkId,PaginationRequestDTO dto);
    Task<List<QuizType>> GetQuizTypesAsync();
    
    // Parent-Child Quiz methods
    Task<List<QuizDTO>> GetParentQuizzesByLinkId(Guid linkId);
    Task<List<ChildQuizDTO>> GetChildQuizzesByParentId(Guid parentQuizId);
    
    // Student Quiz methods
    Task<List<StudentQuizDTO>> GetStudentQuizzesByLinkId(Guid linkId);
    Task<StudentQuizListResponseDTO> GetStudentQuizzesWithProgress(Guid linkId, Guid studentId);
    Task<StudentQuizSimpleResponseDTO> GetStudentQuizzesSimple(Guid linkId, Guid studentId); // New simplified method
    Task<StudentQuizDTO?> GetStudentQuizById(Guid quizId);
    Task<StudentQuizStartResponseDTO> StartStudentQuiz(Guid quizId, Guid studentId);
    Task<StudentQuizSubmissionResponseDTO> SubmitStudentAnswer(StudentQuizSubmissionDTO submission, Guid studentId);
}