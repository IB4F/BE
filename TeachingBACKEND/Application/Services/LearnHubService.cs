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
                Title = link.Title
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
                Title = linkDto.Title
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
                Title = link.Title
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
        var learnHub = await _context.LearnHubs
            .Include(lh => lh.Links)
                .ThenInclude(l => l.Quizzes)
                    .ThenInclude(q => q.Options)
            .Include(lh => lh.Links)
                .ThenInclude(l => l.Quizzes)
                    .ThenInclude(q => q.ChildQuizzes)
                        .ThenInclude(cq => cq.Options)
            .FirstOrDefaultAsync(lh => lh.Id == id);
            
        if (learnHub == null)
            throw new Exception("LearnHub not found");
        
        var filesToDelete = new List<Guid>();
        
        foreach (var link in learnHub.Links)
        {
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

                // Collect files from child quizzes
                foreach (var childQuiz in quiz.ChildQuizzes)
                {
                    if (childQuiz.QuestionAudioId.HasValue)
                        filesToDelete.Add(childQuiz.QuestionAudioId.Value);
                        
                    if (childQuiz.ExplanationAudioId.HasValue)
                        filesToDelete.Add(childQuiz.ExplanationAudioId.Value);
                        
                    foreach (var option in childQuiz.Options)
                    {
                        if (option.OptionImageId.HasValue)
                            filesToDelete.Add(option.OptionImageId.Value);
                    }
                }
            }
        }

        // Remove the learnHub (this will cascade delete links, quizzes, and options)
        _context.LearnHubs.Remove(learnHub);
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
                Console.WriteLine($"Failed to delete file {fileId} during learnHub deletion: {ex.Message}");
            }
        }
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
                    Title = link.Title
                }).ToList()
            })
            .ToListAsync();

        return new PaginatedResultDTO<PaginationLearnHubDTO>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
    public async Task<List<FilteredLearnHubDTO>> GetFilteredLearnHubs(string classType, string subject, bool isAuthenticated = false, Guid? userId = null)
    {
        // Handle null or empty parameters
        if (string.IsNullOrWhiteSpace(classType) || string.IsNullOrWhiteSpace(subject))
        {
            return new List<FilteredLearnHubDTO>();
        }

        // Find the class and subject by name to get their GUIDs
        var classEntity = await _context.Classes
            .FirstOrDefaultAsync(c => c.Name.ToLower() == classType.ToLower());
        
        var subjectEntity = await _context.Subjects
            .FirstOrDefaultAsync(s => s.Name.ToLower() == subject.ToLower());

        // If class or subject not found, return empty list
        if (classEntity == null || subjectEntity == null)
        {
            return new List<FilteredLearnHubDTO>();
        }

        var learnHubs = await _context.LearnHubs
            .Where(lh => lh.ClassType == classEntity.Id.ToString() &&
                         lh.Subject == subjectEntity.Id.ToString() &&
                         (isAuthenticated || lh.IsFree)) // Show all for authenticated users, only free for anonymous
            .Include(lh => lh.Links)
            .ThenInclude(link => link.Quizzes)
            .Select(lh => new FilteredLearnHubDTO
            {
                Title = lh.Title,
                Description = lh.Description,
                ClassType = classEntity.Name,
                Subject = subjectEntity.Name,
                IsFree = lh.IsFree,
                Difficulty = lh.Difficulty,
                CreatedAt = lh.CreatedAt,
                Links = lh.Links.Select(link => new LinkDTO
                {
                    Id = link.Id,
                    Title = link.Title,
                    QuizzesCount = link.Quizzes.Count(q => q.ParentQuizId == null),
                    Status = null // Will be calculated below
                }).ToList()
            })
            .ToListAsync();

        // Calculate progress status for authenticated users
        if (isAuthenticated && userId.HasValue)
        {
            foreach (var learnHub in learnHubs)
            {
                foreach (var link in learnHub.Links)
                {
                    var linkStatus = await CalculateLinkProgress(userId.Value, link.Id);
                    link.Status = linkStatus;
                }
            }
        }

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
            Title = dto.Title
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

        // Validate parent quiz if provided
        Guid? parentQuizId = null;
        if (!string.IsNullOrEmpty(dto.ParentQuizId))
        {
            if (!Guid.TryParse(dto.ParentQuizId, out Guid parsedParentId))
                throw new Exception("Invalid parent quiz ID");

            var parentQuiz = await _context.Quizzes
                .Include(q => q.ChildQuizzes)
                .FirstOrDefaultAsync(q => q.Id == parsedParentId && q.LinkId == linkId);

            if (parentQuiz == null)
                throw new Exception("Parent quiz not found");

            // Check if parent already has 3 children
            if (parentQuiz.ChildQuizzes.Count >= 3)
                throw new Exception("Parent quiz already has maximum number of child quizzes (3)");

            parentQuizId = parsedParentId;
        }

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
            ParentQuizId = parentQuizId,
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
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.Options)
                    .ThenInclude(o => o.OptionImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuestionAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.ExplanationAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuizzType)
            .ToListAsync();

        return quizzes.Select(q => MapToQuizDTO(q)).ToList();
    }

    private QuizDTO MapToQuizDTO(Quizz quiz)
    {
        return new QuizDTO
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Points = quiz.Points,
            IsAnswered = quiz.IsAnswered,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            QuizzTypeName = quiz.QuizzType.Name,
            ParentQuizId = quiz.ParentQuizId,
            Options = quiz.Options.Select(o => new OptionTextDTO
            {
                OptionText = o.OptionText,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList(),
            ChildQuizzes = quiz.ChildQuizzes.Select(cq => MapToQuizDTO(cq)).ToList()
        };
    }
    public async Task<GetQuizzDTO?> GetQuizzByIdDTOAsync(Guid id)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.Options)
                    .ThenInclude(o => o.OptionImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuestionAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.ExplanationAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuizzType)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quiz == null)
            return null;

        return MapToGetQuizzDTO(quiz);
    }

    private GetQuizzDTO MapToGetQuizzDTO(Quizz quiz)
    {
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
            ParentQuizId = quiz.ParentQuizId,
            Options = quiz.Options.Select(o => new OptionDTO
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList(),
            IsAnswered = quiz.IsAnswered,
            ChildQuizzes = quiz.ParentQuizId == null ? quiz.ChildQuizzes.Select(cq => (object)MapToChildGetQuizzDTO(cq)).ToList() : new List<object>()
        };
    }

    private ChildGetQuizzDTO MapToChildGetQuizzDTO(Quizz quiz)
    {
        return new ChildGetQuizzDTO
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
            ParentQuizId = quiz.ParentQuizId,
            Options = quiz.Options.Select(o => new OptionDTO
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList(),
            IsAnswered = quiz.IsAnswered
            // No ChildQuizzes property - child quizzes don't have children
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

        // Validate parent quiz if provided
        Guid? parentQuizId = null;
        if (!string.IsNullOrEmpty(dto.ParentQuizId))
        {
            if (!Guid.TryParse(dto.ParentQuizId, out Guid parsedParentId))
                throw new Exception("Invalid parent quiz ID");

            // Prevent circular reference - quiz cannot be its own parent
            if (parsedParentId == id)
                throw new Exception("Quiz cannot be its own parent");

            var parentQuiz = await _context.Quizzes
                .Include(q => q.ChildQuizzes)
                .FirstOrDefaultAsync(q => q.Id == parsedParentId && q.LinkId == quizz.LinkId);

            if (parentQuiz == null)
                throw new Exception("Parent quiz not found");

            // Check if parent already has 3 children (excluding current quiz if it's already a child)
            var existingChildCount = parentQuiz.ChildQuizzes.Count;
            if (quizz.ParentQuizId != parsedParentId) // If changing parent
            {
                if (existingChildCount >= 3)
                    throw new Exception("Parent quiz already has maximum number of child quizzes (3)");
            }

            parentQuizId = parsedParentId;
        }

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
        quizz.ParentQuizId = parentQuizId;

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
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.Options)
            .FirstOrDefaultAsync(q => q.Id == id);
            
        if (quizz == null)
            throw new Exception("Quizz not found");

        // Collect all file IDs to delete (including child quizzes)
        var filesToDelete = new List<Guid>();
        
        // Collect files from parent quiz
        if (quizz.QuestionAudioId.HasValue)
            filesToDelete.Add(quizz.QuestionAudioId.Value);
            
        if (quizz.ExplanationAudioId.HasValue)
            filesToDelete.Add(quizz.ExplanationAudioId.Value);
            
        foreach (var option in quizz.Options)
        {
            if (option.OptionImageId.HasValue)
                filesToDelete.Add(option.OptionImageId.Value);
        }

        // Collect files from child quizzes
        foreach (var childQuiz in quizz.ChildQuizzes)
        {
            if (childQuiz.QuestionAudioId.HasValue)
                filesToDelete.Add(childQuiz.QuestionAudioId.Value);
                
            if (childQuiz.ExplanationAudioId.HasValue)
                filesToDelete.Add(childQuiz.ExplanationAudioId.Value);
                
            foreach (var option in childQuiz.Options)
            {
                if (option.OptionImageId.HasValue)
                    filesToDelete.Add(option.OptionImageId.Value);
            }
        }

        // Remove child quizzes first (since we use NoAction instead of Cascade)
        foreach (var childQuiz in quizz.ChildQuizzes.ToList())
        {
            _context.Quizzes.Remove(childQuiz);
        }
        
        // Remove the parent quiz
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
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null) // Only parent quizzes (no parent)
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
                QuizType = q.QuizzTypeId.ToString("D"),
                ParentQuizId = q.ParentQuizId // Add this temporarily for debugging
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

    // Parent-Child Quiz methods
    public async Task<List<QuizDTO>> GetParentQuizzesByLinkId(Guid linkId)
    {
        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null) // Only quizzes without parents
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.Options)
                    .ThenInclude(o => o.OptionImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuestionAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.ExplanationAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuizzType)
            .ToListAsync();

        return parentQuizzes.Select(q => MapToQuizDTO(q)).ToList();
    }

    public async Task<List<ChildQuizDTO>> GetChildQuizzesByParentId(Guid parentQuizId)
    {
        var childQuizzes = await _context.Quizzes
            .Where(q => q.ParentQuizId == parentQuizId)
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .ToListAsync();

        return childQuizzes.Select(q => MapToChildQuizDTO(q)).ToList();
    }

    private ChildQuizDTO MapToChildQuizDTO(Quizz quiz)
    {
        return new ChildQuizDTO
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Explanation = quiz.Explanation,
            Points = quiz.Points,
            IsAnswered = quiz.IsAnswered,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            QuestionAudioId = quiz.QuestionAudioId.HasValue ? quiz.QuestionAudioId.Value.ToString() : null,
            ExplanationAudioId = quiz.ExplanationAudioId.HasValue ? quiz.ExplanationAudioId.Value.ToString() : null,
            QuizType = quiz.QuizzTypeId.ToString("D"),
            ParentQuizId = quiz.ParentQuizId,
            Options = quiz.Options.Select(o => new OptionTextDTO
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = o.OptionImageId.HasValue ? o.OptionImageId.Value.ToString() : null,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
            }).ToList()
        };
    }

    // Student Quiz methods
    public async Task<List<StudentQuizDTO>> GetStudentQuizzesByLinkId(Guid linkId)
    {
        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null) // Only parent quizzes
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .ToListAsync();

        return parentQuizzes.Select(q => MapToStudentQuizDTO(q)).ToList();
    }

    public async Task<StudentQuizDTO?> GetStudentQuizById(Guid quizId)
    {
        var quiz = await _context.Quizzes
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .FirstOrDefaultAsync(q => q.Id == quizId);

        return quiz != null ? MapToStudentQuizDTO(quiz) : null;
    }

    public async Task<StudentQuizListResponseDTO> GetStudentQuizzesWithProgress(Guid linkId, Guid studentId)
    {
        // Get all parent quizzes for the link
        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null)
            .Include(q => q.Options)
                .ThenInclude(o => o.OptionImage)
            .Include(q => q.QuestionAudio)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.QuizzType)
            .OrderBy(q => q.CreatedAt)
            .ToListAsync();

        // Get student's performance for this link
        var studentPerformances = await _context.StudentQuizPerformances
            .Where(sqp => sqp.StudentId == studentId && sqp.LinkId == linkId)
            .ToListAsync();

        // Calculate progress
        var totalQuizzes = parentQuizzes.Count;
        var completedQuizzes = studentPerformances.Count(sqp => sqp.IsCorrect);
        var totalPointsEarned = studentPerformances.Sum(sqp => sqp.PointsEarned);
        var lastCompletedAttempt = studentPerformances
            .Where(sqp => sqp.IsCorrect)
            .OrderByDescending(sqp => sqp.CompletedAt)
            .FirstOrDefault();

        var progress = new StudentProgressSummaryDTO
        {
            TotalQuizzes = totalQuizzes,
            CompletedQuizzes = completedQuizzes,
            CurrentQuizIndex = completedQuizzes,
            TotalPointsEarned = totalPointsEarned,
            LastCompletedQuizId = lastCompletedAttempt?.QuizId,
            LastCompletedAt = lastCompletedAttempt?.CompletedAt
        };

        // Map quizzes with progress information
        var quizzesWithProgress = parentQuizzes.Select(quiz =>
        {
            var performance = studentPerformances.FirstOrDefault(sqp => sqp.QuizId == quiz.Id);
            
            return new StudentQuizProgressDTO
            {
                Id = quiz.Id,
                Question = quiz.Question,
                Points = quiz.Points,
                IsCompleted = performance?.IsCorrect ?? false,
                PointsEarned = performance?.PointsEarned ?? 0,
                StartedAt = performance?.StartedAt,
                CompletedAt = performance?.CompletedAt,
                TimeSpentSeconds = performance?.TimeSpentSeconds,
                QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
                ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
                QuizzTypeName = quiz.QuizzType.Name,
                ParentQuizId = quiz.ParentQuizId,
                Options = quiz.Options.Select(o => new StudentOptionDTO
                {
                    Id = o.Id.ToString(),
                    OptionText = o.OptionText,
                    OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
                }).ToList()
            };
        }).ToList();

        return new StudentQuizListResponseDTO
        {
            Progress = progress,
            Quizzes = quizzesWithProgress
        };
    }

    public async Task<StudentQuizSimpleResponseDTO> GetStudentQuizzesSimple(Guid linkId, Guid studentId)
    {
        // Get all parent quizzes for the link
        var parentQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null)
            .OrderBy(q => q.CreatedAt)
            .ToListAsync();

        // Get ALL quizzes (parent + child) for the link to calculate total possible points
        var allQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId)
            .ToListAsync();

        // Get student's performance for this link
        var studentPerformances = await _context.StudentQuizPerformances
            .Where(sqp => sqp.StudentId == studentId && sqp.LinkId == linkId)
            .ToListAsync();

        // Calculate progress
        var totalQuizzes = parentQuizzes.Count;
        var completedQuizzes = studentPerformances.Count(sqp => sqp.IsCorrect);
        var totalPointsEarned = studentPerformances.Sum(sqp => sqp.PointsEarned);
        var totalPossiblePoints = allQuizzes.Sum(q => q.Points); // Total points from ALL quizzes
        var lastCompletedAttempt = studentPerformances
            .Where(sqp => sqp.IsCorrect)
            .OrderByDescending(sqp => sqp.CompletedAt)
            .FirstOrDefault();

        var progress = new StudentProgressSummaryDTO
        {
            TotalQuizzes = totalQuizzes,
            CompletedQuizzes = completedQuizzes,
            CurrentQuizIndex = completedQuizzes,
            TotalPointsEarned = totalPointsEarned,
            TotalPossiblePoints = totalPossiblePoints, // Include total possible points
            LastCompletedQuizId = lastCompletedAttempt?.QuizId,
            LastCompletedAt = lastCompletedAttempt?.CompletedAt
        };

        // Only return parent quiz IDs
        var parentQuizIds = parentQuizzes.Select(q => q.Id).ToList();

        return new StudentQuizSimpleResponseDTO
        {
            ParentQuizIds = parentQuizIds,
            Progress = progress
        };
    }

    public async Task<StudentQuizStartResponseDTO> StartStudentQuiz(Guid quizId, Guid studentId)
    {
        // This method is deprecated - use StudentPerformanceService.StartQuizSession instead
        throw new NotImplementedException("This method is deprecated. Use StudentPerformanceService.StartQuizSession instead.");
    }

    public async Task<StudentQuizSubmissionResponseDTO> SubmitStudentAnswer(StudentQuizSubmissionDTO submission, Guid studentId)
    {
        // This method is deprecated - use StudentPerformanceService.SubmitAnswer instead
        throw new NotImplementedException("This method is deprecated. Use StudentPerformanceService.SubmitAnswer instead.");
    }

    private StudentQuizDTO MapToStudentQuizDTO(Quizz quiz)
    {
        return new StudentQuizDTO
        {
            Id = quiz.Id,
            Question = quiz.Question,
            Points = quiz.Points,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            QuizzTypeName = quiz.QuizzType.Name,
            ParentQuizId = quiz.ParentQuizId,
            Options = quiz.Options.Select(o => new StudentOptionDTO
            {
                Id = o.Id.ToString(),
                OptionText = o.OptionText,
                OptionImageUrl = GetFullUrl(o.OptionImage?.FileUrl)
                // Note: IsCorrect is intentionally excluded for security
            }).ToList()
        };
    }

    private async Task<string> CalculateLinkProgress(Guid userId, Guid linkId)
    {
        // Get total quizzes for this link
        var totalQuizzes = await _context.Quizzes
            .Where(q => q.LinkId == linkId && q.ParentQuizId == null)
            .CountAsync();

        if (totalQuizzes == 0)
        {
            return "Not Started";
        }

        // Check if user has any quiz attempts for this link
        var hasAnyAttempts = await _context.StudentQuizPerformances
            .AnyAsync(sp => sp.StudentId == userId && sp.LinkId == linkId);

        if (!hasAnyAttempts)
        {
            return "Not Started";
        }

        // Get completed quizzes by this user
        var completedQuizzes = await _context.StudentQuizPerformances
            .Where(sp => sp.StudentId == userId && sp.LinkId == linkId && sp.IsCompleted)
            .CountAsync();

        if (completedQuizzes == 0)
        {
            return "Not Started";
        }
        else if (completedQuizzes >= totalQuizzes)
        {
            return "Completed";
        }
        else
        {
            return "In Progress";
        }
    }
}