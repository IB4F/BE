using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

public class LearnHubService : ILearnHubService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LearnHubService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // ----------------------------
    // LearnHub CRUD
    // ----------------------------

    public async Task<Guid> PostLearnHub(LearnHubCreateDTO dto)
    {
        if (dto == null)
        {
            throw new Exception("DTO is null");
        }

        var postLearnHub = new LearnHub
        {
            Title = dto.Title,
            Description = dto.Description,
            Subject = dto.Subject,
            ClassType = dto.ClassType,
            IsFree = dto.IsFree,
            //Difficulty = dto.Difficulty,
            CreatedAt = DateTime.UtcNow,
            Links = dto.Links?.Select(l => new Link
            {
                Title = l.Title,
                Progress = l.Progress
            }).ToList() ?? new List<Link>() { }
        };

        _context.LearnHubs.Add(postLearnHub);
        await _context.SaveChangesAsync();
        return postLearnHub.Id;
    }
    public async Task<List<LearnHubDTO>> GetLearnHubs()
    {
        var learnHubs = await _context.LearnHubs.ToListAsync();
        return _mapper.Map<List<LearnHubDTO>>(learnHubs);
    }

    public async Task<LearnHubDTO> GetSingleLearnHub(Guid id)
    {
        var learnHub = await _context.LearnHubs.FindAsync(id);
        if (learnHub == null) return null;

        return _mapper.Map<LearnHubDTO>(learnHub);
    }

    public async Task<LearnHubDTO> UpdateLearnHub(Guid id, LearnHubCreateDTO dto)
    {
        var learnHub = await _context.LearnHubs.FindAsync(id);
        if (learnHub == null)
            throw new Exception("LearnHub not found");

        learnHub.Title = dto.Title;
        learnHub.Description = dto.Description;
        learnHub.Subject = dto.Subject;
        learnHub.ClassType = dto.ClassType;
        learnHub.IsFree = dto.IsFree;

        await _context.SaveChangesAsync();

        return _mapper.Map<LearnHubDTO>(learnHub);
    }

    public async Task DeleteLearnHub(Guid id)
    {
        var learnHub = await _context.LearnHubs.FindAsync(id);
        if (learnHub == null)
            throw new Exception("LearnHub not found");

        _context.LearnHubs.Remove(learnHub);
        await _context.SaveChangesAsync();
    }
    public async Task<PaginatedResultDTO<PaginationLearnHubDTO>> GetPaginatedLearnHubs(PaginationRequestDTO dto)
    {
        var query = _context.LearnHubs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            var search = dto.Search.ToLower();
            query = query.Where(lh =>
                lh.Title.ToLower().Contains(search) ||
                lh.Subject.ToLower().Contains(search));
        }

        query = query.OrderByDescending(lh => lh.CreatedAt);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(dto.PageNumber * dto.PageSize)
            .Take(dto.PageSize)
            .ToListAsync();

        var dtoItems = _mapper.Map<List<PaginationLearnHubDTO>>(items);

        return new PaginatedResultDTO<PaginationLearnHubDTO>
        {
            Items = dtoItems,
            TotalCount = totalCount
        };
    }

    public async Task<List<LearnHubDTO>> GetFilteredLearnHubs(string classType, string subject)
    {
        var learnHubs = await _context.LearnHubs
            .Where(lh => (string.IsNullOrEmpty(classType) || lh.ClassType.ToLower() == classType.ToLower()) &&
                         (string.IsNullOrEmpty(subject) || lh.Subject.ToLower() == subject.ToLower()))
            .ToListAsync();

        return _mapper.Map<List<LearnHubDTO>>(learnHubs);
    }


    // ----------------------------
    // Link CRUD
    // ----------------------------

    public async Task<Guid> PostLink(Guid learnHubId, CreateLinkDTO dto)
    {
        var learnHub = await _context.LearnHubs.FindAsync(learnHubId);
        if (learnHub == null)
            throw new Exception("LearnHub not found");


        var newLink = new Link
        {
            LearnHubId = learnHubId,
            Title = dto.Title,
            Progress = dto.Progress,
        };

        _context.Links.Add(newLink);
        await _context.SaveChangesAsync();
        return newLink.Id;
    }
    public async Task<List<Link>> GetAllLinksAsync()
    {
        return await _context.Links.ToListAsync();
    }
    public async Task<Link> GetLinkByIdAsync(Guid id)
    {
        return await _context.Links.FindAsync(id);
    }
    public async Task<Link> UpdateLink(Guid id, CreateLinkDTO dto)
    {
        var link = await _context.Links.FindAsync(id);
        if (link == null)
            throw new Exception("Link not found");

        link.Title = dto.Title;
        link.Progress = dto.Progress;

        await _context.SaveChangesAsync();
        return link;
    }
    public async Task DeleteLink(Guid id)
    {
        var link = await _context.Links.FindAsync(id);
        if (link == null)
            throw new Exception("Link not found");

        _context.Links.Remove(link);
        await _context.SaveChangesAsync();
    }

    // ----------------------------
    // Quizz CRUD
    // ----------------------------

    public async Task<Guid> PostQuizz(Guid linkId,CreateQuizzDTO dto)
    {

        var link = await _context.Links.FindAsync(linkId);
        if (link == null)
            throw new Exception("Link not found");

        var newQuizz = new Quizz
        {
            LinkId = linkId,
            Question = dto.Question,
            Explanation = dto.Explanation,
            Points = dto.Points,
            Options = dto.Options
        };

        _context.Quizzes.Add(newQuizz);
        await _context.SaveChangesAsync();
        return newQuizz.Id;
    }
    public async Task<List<GetQuizzDTO>> GetAllQuizzesDTOAsync()
    {
        return await _context.Quizzes
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options,
            })
            .ToListAsync();
    }
    public async Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id)
    {
        return await _context.Quizzes
            .Where(q => q.Id == id)
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options,
            })
            .FirstOrDefaultAsync();
    }
    public async Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto)
    {
        var quizz = await _context.Quizzes.FindAsync(id);
        if (quizz == null)
            throw new Exception("Quizz not found");

        quizz.Question = dto.Question;
        quizz.Explanation = dto.Explanation;
        quizz.Points = dto.Points;
        quizz.Options = dto.Options;


        await _context.SaveChangesAsync();
        return quizz;
    }
    public async Task DeleteQuizz(Guid id)
    {
        var quizz = await _context.Quizzes.FindAsync(id);
        if (quizz == null)
            throw new Exception("Quizz not found");

        _context.Quizzes.Remove(quizz);
        await _context.SaveChangesAsync();
    }
    public async Task<List<GetQuizzDTO>> GetPaginatedQuizzesAsync(PaginationRequestDTO dto)
    {
        return await _context.Quizzes
            .OrderByDescending(q => q.CreatedAt)
            .Skip((dto.PageNumber - 1) * dto.PageSize)
            .Take(dto.PageSize)
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options
            })
            .ToListAsync();
    }


}
