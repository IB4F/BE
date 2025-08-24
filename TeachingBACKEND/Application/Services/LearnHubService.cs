using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.DTOs.Quizzes;
using TeachingBACKEND.Domain.Entities;
using Microsoft.AspNetCore.Http;
using TeachingBACKEND.Application.Interfaces;

public class LearnHubService : ILearnHubService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileService _fileService;

    public LearnHubService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IFileService fileService)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _fileService = fileService;
    }

    private string GetFullUrl(string relativeUrl)
    {
        if (string.IsNullOrEmpty(relativeUrl))
            return null;

        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
            return relativeUrl;

        var baseUrl = $"{request.Scheme}://{request.Host}";
        return $"{baseUrl}{relativeUrl}";
    }

    // ----------------------------
    // LearnHub CRUD
    // ----------------------------

    public async Task<Guid> PostLearnHub(LearnHubCreateDTO dto)
    {
        if (dto == null)
            throw new Exception("DTO is null");
        
        var classExists = await _context.Classes
            .AnyAsync(c => c.Id == Guid.Parse(dto.ClassType));

        if (!classExists)
            throw new Exception("Class not found");
        
        var subjectExists = await _context.Subjects
            .AnyAsync(s => s.Id == Guid.Parse(dto.Subject));

        if (!subjectExists)
            throw new Exception("Subject not found");

        var postLearnHub = new LearnHub
        {
            Title = dto.Title,
            Description = dto.Description,
            ClassType = dto.ClassType, // Store the class ID directly
            Subject = dto.Subject, // Store the subject ID directly
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
                .ThenInclude(link => link.Quizzes)
            .FirstOrDefaultAsync(lh => lh.Id == id);

        if (learnHub == null)
            throw new Exception("LearnHub not found");
        
        var classExists = await _context.Classes
            .AnyAsync(c => c.Id == Guid.Parse(dto.ClassType));

        if (!classExists)
            throw new Exception("Class not found");
        
        var subjectExists = await _context.Subjects
            .AnyAsync(s => s.Id == Guid.Parse(dto.Subject));

        if (!subjectExists)
            throw new Exception("Subject not found");

        // Update main fields
        learnHub.Title = dto.Title;
        learnHub.Description = dto.Description;
        learnHub.Subject = dto.Subject; // Store subject ID
        learnHub.ClassType = dto.ClassType; // Store class ID
        learnHub.IsFree = dto.IsFree;

        // Smart link update - preserve existing links and their quizzes
        var existingLinks = learnHub.Links.ToList();
        var newLinkDtos = dto.Links.ToList();

        // Update existing links that match by index (assuming order matters)
        for (int i = 0; i < Math.Min(existingLinks.Count, newLinkDtos.Count); i++)
        {
            var existingLink = existingLinks[i];
            var newLinkDto = newLinkDtos[i];
            
            existingLink.Title = newLinkDto.Title;
            existingLink.Progress = newLinkDto.Progress;
        }

        // Remove extra existing links (if new DTO has fewer links)
        if (existingLinks.Count > newLinkDtos.Count)
        {
            var linksToRemove = existingLinks.Skip(newLinkDtos.Count).ToList();
            _context.Links.RemoveRange(linksToRemove);
            foreach (var link in linksToRemove)
            {
                learnHub.Links.Remove(link);
            }
        }

        // Add new links (if new DTO has more links)
        if (newLinkDtos.Count > existingLinks.Count)
        {
            var newLinks = newLinkDtos.Skip(existingLinks.Count).Select(linkDto => new Link
            {
                LearnHubId = learnHub.Id,
                Title = linkDto.Title,
                Progress = linkDto.Progress
            }).ToList();

            learnHub.Links.AddRange(newLinks);
        }

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
    
    public async Task MigrateLearnHubClassTypes()
    {
        var learnHubs = await _context.LearnHubs.ToListAsync();
        
        foreach (var learnHub in learnHubs)
        {
            // Check if ClassType is a class name (not a GUID)
            if (!Guid.TryParse(learnHub.ClassType, out _))
            {
                // Find the class by name
                var classEntity = await _context.Classes
                    .FirstOrDefaultAsync(c => c.Name == learnHub.ClassType);
                
                if (classEntity != null)
                {
                    learnHub.ClassType = classEntity.Id.ToString();
                }
            }

            // Check if Subject is a subject name (not a GUID)
            if (!Guid.TryParse(learnHub.Subject, out _))
            {
                // Find the subject by name
                var subjectEntity = await _context.Subjects
                    .FirstOrDefaultAsync(s => s.Name == learnHub.Subject);
                
                if (subjectEntity != null)
                {
                    learnHub.Subject = subjectEntity.Id.ToString();
                }
            }
        }

        await _context.SaveChangesAsync();
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
                    Title = link.Title,
                    Progress = link.Progress,
                    QuizzesCount = link.Quizzes.Count()
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
        var link = await _context.Links
            .Include(l => l.Quizzes)
                .ThenInclude(q => q.Options)
            .FirstOrDefaultAsync(l => l.Id == id);
            
        if (link == null)
            throw new Exception("Link not found");

        // Collect all file IDs to delete before removing the link
        var filesToDelete = new List<Guid>();
        
        foreach (var quiz in link.Quizzes)
        {
            if (quiz.QuestionAudioId.HasValue)
                filesToDelete.Add(quiz.QuestionAudioId.Value);
                
            if (quiz.ExplanationAudioId.HasValue)
                filesToDelete.Add(quiz.ExplanationAudioId.Value);
                
            foreach (var option in quiz.Options)
            {
                if (option.OptionImageId.HasValue)
                    filesToDelete.Add(option.OptionImageId.Value);
            }
        }

        // Remove the link (this will cascade delete quizzes and options)
        _context.Links.Remove(link);
        await _context.SaveChangesAsync();

        // Delete associated files
        foreach (var fileId in filesToDelete)
        {
            try
            {
                await _fileService.DeleteFileAsync(fileId);
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the deletion
                Console.WriteLine($"Failed to delete file {fileId} during link deletion: {ex.Message}");
            }
        }
    }

    // ----------------------------
    // Quizz CRUD
    // ----------------------------

    public async Task<Guid> PostQuizz(Guid linkId, CreateQuizzDTO dto)
    {
        var link = await _context.Links.FindAsync(linkId);
        if (link == null)
            throw new Exception("Link not found");

        // Validate quiz type
        if (!Guid.TryParse(dto.QuizType, out Guid quizTypeId))
            throw new Exception("Invalid quiz type ID");

        var quizType = await _context.QuizTypes.FindAsync(quizTypeId);
        if (quizType == null)
            throw new Exception("Quiz type not found");

        var newQuizz = new Quizz
        {
            LinkId = linkId,
            QuizzTypeId = quizTypeId,
            Question = dto.Question,
            Explanation = dto.Explanation,
            Points = dto.Points,
            CreatedAt = DateTime.UtcNow,
            QuestionAudioId = !string.IsNullOrEmpty(dto.QuestionAudioId) ? Guid.Parse(dto.QuestionAudioId) : null,
            ExplanationAudioId = !string.IsNullOrEmpty(dto.ExplanationAudioId) ? Guid.Parse(dto.ExplanationAudioId) : null,
            Options = dto.Options.Select(o => new Option
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = !string.IsNullOrEmpty(o.OptionImageId) ? Guid.Parse(o.OptionImageId) : null
            }).ToList()
        };

        _context.Quizzes.Add(newQuizz);
        await _context.SaveChangesAsync();
        return newQuizz.Id;
    }
    public async Task<List<QuizDTO>> GetQuizzesByLinkId(Guid linkId)
    {
        var quizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId)
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .ToListAsync();

        return quizzes.Select(q => new QuizDTO
        {
            Id = q.Id,
            Question = q.Question,
            Points = q.Points,
            IsAnswered = q.IsAnswered,
            QuestionAudioUrl = GetFullUrl(q.QuestionAudio?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(q.ExplanationAudio?.FileUrl),
            QuizzTypeName = q.QuizzType.Name,
            Options = q.Options.Select(o => new OptionTextDTO
            {
                OptionText = o.OptionText,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList()
        }).ToList();
    }
    public async Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quiz == null)
            return null;

        return new GetQuizzDTO
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Explanation = quiz.Explanation,
            Points = quiz.Points,
            QuestionAudioId = quiz.QuestionAudioId.HasValue ? quiz.QuestionAudioId.Value.ToString() : null,
            ExplanationAudioId = quiz.ExplanationAudioId.HasValue ? quiz.ExplanationAudioId.Value.ToString() : null,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            QuizType = quiz.QuizzTypeId.ToString("D"),
            Options = quiz.Options.Select(o => new OptionDTO
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList(),
            IsAnswered = quiz.IsAnswered
        };
    }
    public async Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto)
    {
        var quizz = await _context.Quizzes
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quizz == null)
            throw new Exception("Quizz not found");

        // Validate quiz type
        if (!Guid.TryParse(dto.QuizType, out Guid quizTypeId))
            throw new Exception("Invalid quiz type ID");

        var quizType = await _context.QuizTypes.FindAsync(quizTypeId);
        if (quizType == null)
            throw new Exception("Quiz type not found");

        // Store old file IDs for cleanup
        var oldQuestionAudioId = quizz.QuestionAudioId;
        var oldExplanationAudioId = quizz.ExplanationAudioId;
        var oldOptionImageIds = quizz.Options
            .Where(o => o.OptionImageId.HasValue)
            .Select(o => o.OptionImageId.Value)
            .ToList();

        quizz.Question = dto.Question;
        quizz.Explanation = dto.Explanation;
        quizz.Points = dto.Points;
        quizz.QuizzTypeId = quizTypeId;
        quizz.QuestionAudioId = !string.IsNullOrEmpty(dto.QuestionAudioId) ? Guid.Parse(dto.QuestionAudioId) : null;
        quizz.ExplanationAudioId = !string.IsNullOrEmpty(dto.ExplanationAudioId) ? Guid.Parse(dto.ExplanationAudioId) : null;

        // Remove old options
        _context.Options.RemoveRange(quizz.Options);

        // Add new options
        quizz.Options = dto.Options.Select(o => new Option
        {
            OptionText = o.OptionText,
            IsCorrect = o.IsCorrect,
            OptionImageId = !string.IsNullOrEmpty(o.OptionImageId) ? Guid.Parse(o.OptionImageId) : null
        }).ToList();

        await _context.SaveChangesAsync();

        // Clean up old files that are no longer referenced
        await CleanupUnusedFiles(oldQuestionAudioId, oldExplanationAudioId, oldOptionImageIds, dto);

        return quizz;
    }

    private async Task CleanupUnusedFiles(Guid? oldQuestionAudioId, Guid? oldExplanationAudioId, List<Guid> oldOptionImageIds, CreateQuizzDTO newDto)
    {
        var filesToDelete = new List<Guid>();

        // Check if question audio was replaced
        if (oldQuestionAudioId.HasValue && 
            (string.IsNullOrEmpty(newDto.QuestionAudioId) || Guid.Parse(newDto.QuestionAudioId) != oldQuestionAudioId.Value))
        {
            filesToDelete.Add(oldQuestionAudioId.Value);
        }

        // Check if explanation audio was replaced
        if (oldExplanationAudioId.HasValue && 
            (string.IsNullOrEmpty(newDto.ExplanationAudioId) || Guid.Parse(newDto.ExplanationAudioId) != oldExplanationAudioId.Value))
        {
            filesToDelete.Add(oldExplanationAudioId.Value);
        }

        // Check option images that were replaced
        var newOptionImageIds = newDto.Options
            .Where(o => !string.IsNullOrEmpty(o.OptionImageId))
            .Select(o => Guid.Parse(o.OptionImageId))
            .ToList();

        foreach (var oldImageId in oldOptionImageIds)
        {
            if (!newOptionImageIds.Contains(oldImageId))
            {
                filesToDelete.Add(oldImageId);
            }
        }

        // Delete the unused files
        foreach (var fileId in filesToDelete)
        {
            try
            {
                await _fileService.DeleteFileAsync(fileId);
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the update
                // You might want to add proper logging here
                Console.WriteLine($"Failed to delete file {fileId}: {ex.Message}");
            }
        }
    }
    public async Task DeleteQuizz(Guid id)
    {
        var quizz = await _context.Quizzes
            .Include(q => q.Options)
            .FirstOrDefaultAsync(q => q.Id == id);
            
        if (quizz == null)
            throw new Exception("Quizz not found");

        // Collect all file IDs to delete
        var filesToDelete = new List<Guid>();
        
        if (quizz.QuestionAudioId.HasValue)
            filesToDelete.Add(quizz.QuestionAudioId.Value);
            
        if (quizz.ExplanationAudioId.HasValue)
            filesToDelete.Add(quizz.ExplanationAudioId.Value);
            
        foreach (var option in quizz.Options)
        {
            if (option.OptionImageId.HasValue)
                filesToDelete.Add(option.OptionImageId.Value);
        }

        // Remove the quiz first
        _context.Quizzes.Remove(quizz);
        await _context.SaveChangesAsync();

        // Delete associated files
        foreach (var fileId in filesToDelete)
        {
            try
            {
                await _fileService.DeleteFileAsync(fileId);
            }
            catch (Exception ex)
            {
                // Log the error but don't fail the deletion
                Console.WriteLine($"Failed to delete file {fileId} during quiz deletion: {ex.Message}");
            }
        }
    }
    public async Task<PaginatedResultDTO<SimpleQuizDTO>> GetPaginatedQuizzesAsync(Guid linkId, PaginationRequestDTO dto)
    {
        var query = _context.Quizzes
            .Where(q => q.LinkId == linkId)
            .Include(q => q.QuizzType)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Search))
        {
            var search = dto.Search.ToLower();
            query = query.Where(q =>
                q.Question.ToLower().Contains(search) ||
                q.Explanation.ToLower().Contains(search));
        }

        query = query.OrderByDescending(q => q.CreatedAt);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip(dto.PageNumber * dto.PageSize)
            .Take(dto.PageSize)
            .Select(q => new SimpleQuizDTO
            {
                Id = q.Id,
                Question = q.Question,
                QuizType = q.QuizzTypeId.ToString("D")
            })
            .ToListAsync();

        return new PaginatedResultDTO<SimpleQuizDTO>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public async Task<List<QuizType>> GetQuizTypesAsync()
    {
        return await _context.QuizTypes.ToListAsync();
    }
}