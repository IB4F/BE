using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

public interface ILearnHubService
{
    Task<Guid> PostLearnHub(LearnHubCreateDTO dto);
    Task<List<LearnHubDTO>> GetLearnHubs();
    Task<GetSingleLearnHub> GetSingleLearnHub(Guid id);
    Task<LearnHubDTO> UpdateLearnHub(Guid id, LearnHubCreateDTO dto);
    Task DeleteLearnHub(Guid id);
    Task<PaginatedResultDTO<PaginationLearnHubDTO>> GetPaginatedLearnHubs(PaginationRequestDTO dto);
    Task<List<LearnHubDTO>> GetFilteredLearnHubs(string classType, string subject);

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