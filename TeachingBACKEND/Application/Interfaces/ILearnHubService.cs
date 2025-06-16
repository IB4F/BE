using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

public interface ILearnHubService
{
    Task<Guid> PostLearnHub(LearnHubCreateDTO dto);
    Task<List<LearnHub>> GetLearnHubs();
    Task<LearnHub> GetSingleLearnHub(Guid id);
    Task<LearnHub> UpdateLearnHub(Guid id, LearnHubCreateDTO dto);
    Task DeleteLearnHub(Guid id);
    Task<PaginatedResultDTO<LearnHub>> GetPaginatedLearnHubs(PaginationRequestDTO dto);

    // Link
    Task<Guid> PostLink(Guid learnHubId, CreateLinkDTO dto);
    Task<List<Link>> GetAllLinksAsync();
    Task<Link> GetLinkByIdAsync(Guid id);
    Task<Link> UpdateLink(Guid id, CreateLinkDTO dto);
    Task DeleteLink(Guid id);

    // Quiz
    Task<Guid> PostQuizz(Guid linkId, CreateQuizzDTO dto);
    Task<List<GetQuizzDTO>> GetAllQuizzesDTOAsync();
    Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id);
    Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto);
    Task DeleteQuizz(Guid id);
    Task<List<GetQuizzDTO>> GetPaginatedQuizzesAsync(PaginationRequestDTO dto);
}