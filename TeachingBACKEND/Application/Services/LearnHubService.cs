using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            throw new Exception("DTO is null");

        var className = await _context.Classes
            .Where(c => c.Id == Guid.Parse(dto.ClassType))
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        if (className == null)
            throw new Exception("Class not found");

        var subjectName = await _context.Subjects
            .Where(s => s.Id == Guid.Parse(dto.Subject))
            .Select(s => s.Name)
            .FirstOrDefaultAsync();

        if (subjectName == null)
            throw new Exception("Subject not found");

        var postLearnHub = new LearnHub
        {
            Title = dto.Title,
            Description = dto.Description,
            ClassType = className,
            Subject = subjectName,
            Difficulty = dto.Difficulty,
            IsFree = dto.IsFree,
            CreatedAt = DateTime.UtcNow,
            Links = dto.Links?.Select(l => new Link
            {
                Title = l.Title,
                Progress = l.Progress
            }).ToList() ?? new List<Link>()
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

    public async Task<GetSingleLearnHub> GetSingleLearnHub(Guid id)
    {
        var learnHub = await _context.LearnHubs
            .Include(l => l.Links)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (learnHub == null) return null;

        var dto = new GetSingleLearnHub
        {
            Title = learnHub.Title,
            Description = learnHub.Description,
            ClassType = learnHub.ClassType,
            Subject = learnHub.Subject,
            IsFree = learnHub.IsFree,
            CreatedAt = learnHub.CreatedAt,
            Difficulty = learnHub.Difficulty,
            Links = learnHub.Links?.Select(link => new LinkDTO
            {
                Id = link.Id,
                Title = link.Title,
                Progress = link.Progress
            }).ToList()
        };

        return dto;
    }


    public async Task<LearnHubDTO> UpdateLearnHub(Guid id, LearnHubCreateDTO dto)
    {
        var learnHub = await _context.LearnHubs
            .Include(l => l.Links)
            .FirstOrDefaultAsync(lh => lh.Id == id);

        if (learnHub == null)
            throw new Exception("LearnHub not found");

        // Update main fields
        learnHub.Title = dto.Title;
        learnHub.Description = dto.Description;
        learnHub.Subject = dto.Subject;
        learnHub.ClassType = dto.ClassType;
        learnHub.IsFree = dto.IsFree;

        _context.Links.RemoveRange(learnHub.Links);
        learnHub.Links.Clear();

        var newLinks = dto.Links.Select(linkDto => new Link
        {
            LearnHubId = learnHub.Id,
            Title = linkDto.Title,
            Progress = linkDto.Progress
        }).ToList();

        learnHub.Links.AddRange(newLinks);

        await _context.SaveChangesAsync();

        var learnHubDto = new LearnHubDTO
        {
            Title = learnHub.Title,
            Description = learnHub.Description,
            Subject = learnHub.Subject,
            ClassType = learnHub.ClassType,
            IsFree = learnHub.IsFree,
            CreatedAt = learnHub.CreatedAt,
            Links = learnHub.Links.Select(link => new LinkDTO
            {
                Id = link.Id,
                Title = link.Title,
                Progress = link.Progress
            }).ToList()
        };

        return learnHubDto;
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
        var query = _context.LearnHubs
            .Include(lh => lh.Links)
            .AsQueryable();

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
            .Select(lh => new PaginationLearnHubDTO
            {
                Id = lh.Id,
                Title = lh.Title,
                Description = lh.Description,
                ClassType = lh.ClassType,
                Subject = lh.Subject,
                IsFree = lh.IsFree,
                CreatedAt = lh.CreatedAt,
                Links = lh.Links.Select(link => new LinkDTO
                {
                    Id = link.Id,
                    Title = link.Title,
                    Progress = link.Progress
                }).ToList()
            })
            .ToListAsync();

        return new PaginatedResultDTO<PaginationLearnHubDTO>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<List<FilteredLearnHubDTO>> GetFilteredLearnHubs(string classType, string subject)
    {
        var learnHubs = await _context.LearnHubs
            .Where(lh => (string.IsNullOrEmpty(classType) || lh.ClassType.ToLower() == classType.ToLower()) &&
                         (string.IsNullOrEmpty(subject) || lh.Subject.ToLower() == subject.ToLower()))
            .Include(lh => lh.Links)
              .ThenInclude(link => link.Quizzes)
              .ThenInclude(q => q.Options)
            .Select(lh => new FilteredLearnHubDTO
            {
                Title = lh.Title,
                Description = lh.Description,
                ClassType = lh.ClassType,
                Subject = lh.Subject,
                IsFree = lh.IsFree,
                Difficulty = lh.Difficulty,
                CreatedAt = lh.CreatedAt,
                Links = lh.Links.Select(link => new LinkDTO
                {
                    Id = link.Id,
                    Title = link.Title
                }).ToList()
            })
            .ToListAsync();

        return learnHubs;
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
            Progress = dto.Progress
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

    public async Task<Guid> PostQuizz(Guid linkId, CreateQuizzDTO dto)
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
            CreatedAt = DateTime.UtcNow,
            Options = dto.Options.Select(o => new Option
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect
            }).ToList()
        };

        _context.Quizzes.Add(newQuizz);
        await _context.SaveChangesAsync();
        return newQuizz.Id;
    }

    public async Task<List<GetQuizzDTO>> GetAllQuizzesDTOAsync()
    {
        return await _context.Quizzes
            .Include(q => q.Options) 
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options.Select(o => new OptionDTO
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList()
            })
            .ToListAsync();
    }


    public async Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id)
    {
        return await _context.Quizzes
            .Include(q => q.Options)
            .Where(q => q.Id == id)
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options.Select(o => new OptionDTO
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }


    public async Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto)
    {
        var quizz = await _context.Quizzes
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quizz == null)
            throw new Exception("Quizz not found");

        quizz.Question = dto.Question;
        quizz.Explanation = dto.Explanation;
        quizz.Points = dto.Points;

        // Remove old options
        _context.Options.RemoveRange(quizz.Options);

        // Add new options
        quizz.Options = dto.Options.Select(o => new Option
        {
            OptionText = o.OptionText,
            IsCorrect = o.IsCorrect
        }).ToList();

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
            .Include(q => q.Options)
            .OrderByDescending(q => q.CreatedAt)
            .Skip(dto.PageNumber * dto.PageSize)
            .Take(dto.PageSize)
            .Select(q => new GetQuizzDTO
            {
                Question = q.Question,
                Explanation = q.Explanation,
                Points = q.Points,
                Options = q.Options.Select(o => new OptionDTO
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList()
            })
            .ToListAsync();
    }

}