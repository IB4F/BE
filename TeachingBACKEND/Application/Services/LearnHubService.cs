using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.DTOs.Quizzes;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using TeachingBACKEND.Application.Interfaces;

public class LearnHubService : ILearnHubService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileService _fileService;
    private readonly ILogger<LearnHubService> _logger;

    public LearnHubService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IFileService fileService, ILogger<LearnHubService> logger)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _fileService = fileService;
        _logger = logger;
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

        // NEW: Validate tier assignment
        if (!dto.IsFree && !dto.RequiredTier.HasValue)
        {
            throw new Exception("Required tier is needed for paid LearnHubs");
        }

        var postLearnHub = new LearnHub
        {
            Title = dto.Title,
            Description = dto.Description,
            ClassType = dto.ClassType, // Store the class ID directly
            Subject = dto.Subject, // Store the subject ID directly
            Difficulty = dto.Difficulty,
            IsFree = dto.IsFree,
            RequiredTier = dto.IsFree ? null : dto.RequiredTier, // NEW: Set required tier
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
            RequiredTier = (int?)learnHub.RequiredTier, // NEW: Required tier value
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

        // NEW: Validate tier assignment
        if (!dto.IsFree && !dto.RequiredTier.HasValue)
        {
            throw new Exception("Required tier is needed for paid LearnHubs");
        }

        // Update main fields
        learnHub.Title = dto.Title;
        learnHub.Description = dto.Description;
        learnHub.Subject = dto.Subject; // Store subject ID
        learnHub.ClassType = dto.ClassType; // Store class ID
        learnHub.IsFree = dto.IsFree;
        learnHub.RequiredTier = dto.IsFree ? null : dto.RequiredTier; // NEW: Update required tier

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
        var learnHubs = await _context.LearnHubs
            .AsNoTracking() // Performance optimization for migration bulk updates
            .ToListAsync();
        
        // Bulk load class and subject entities for better performance
        var allClasses = await _context.Classes.ToDictionaryAsync(c => c.Name, c => c.Id.ToString());
        var allSubjects = await _context.Subjects.ToDictionaryAsync(s => s.Name, s => s.Id.ToString());
        
        foreach (var learnHub in learnHubs)
        {
            // Check if ClassType is a class name (not a GUID)
            if (!Guid.TryParse(learnHub.ClassType, out _) && allClasses.TryGetValue(learnHub.ClassType, out var classId))
            {
                learnHub.ClassType = classId;
            }

            // Check if Subject is a subject name (not a GUID)
            if (!Guid.TryParse(learnHub.Subject, out _) && allSubjects.TryGetValue(learnHub.Subject, out var subjectId))
            {
                learnHub.Subject = subjectId;
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

        var linkIds = learnHub.Links.Select(l => l.Id).ToList();
        if (linkIds.Any())
        {
            var hasProgress = await _context.StudentPerformanceSummaries
                .AnyAsync(s => linkIds.Contains(s.LinkId));
            if (!hasProgress)
                hasProgress = await _context.StudentQuizPerformances
                    .AnyAsync(p => linkIds.Contains(p.LinkId));
            if (hasProgress)
                throw new InvalidOperationException("Ky modul nuk mund të fshihet sepse ka progres të nxënësve të regjistruar. Hiqni të gjitha të dhënat e nxënësve para se ta fshini.");
        }
        
        var filesToDelete = new List<Guid>();
        
        foreach (var link in learnHub.Links)
        {
            foreach (var quiz in link.Quizzes)
            {
                if (quiz.QuestionAudioId.HasValue)
                    filesToDelete.Add(quiz.QuestionAudioId.Value);

                if (quiz.QuestionImageId.HasValue)
                    filesToDelete.Add(quiz.QuestionImageId.Value);

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

                    if (childQuiz.QuestionImageId.HasValue)
                        filesToDelete.Add(childQuiz.QuestionImageId.Value);

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
                RequiredTier = lh.RequiredTier,
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
            .ToListAsync();

        var result = new List<FilteredLearnHubDTO>();

        foreach (var lh in learnHubs)
        {
            var learnHubDto = new FilteredLearnHubDTO
            {
                Id = lh.Id,
                Title = lh.Title,
                Description = lh.Description,
                ClassType = classEntity.Name,
                Subject = subjectEntity.Name,
                IsFree = lh.IsFree,
                RequiredTier = lh.RequiredTier?.ToString(), // NEW: Required tier name
                Difficulty = lh.Difficulty,
                CreatedAt = lh.CreatedAt,
                Links = new List<LinkDTO>()
            };

            foreach (var link in lh.Links)
            {
                var linkDto = new LinkDTO
                {
                    Id = link.Id,
                    Title = link.Title,
                    QuizzesCount = link.Quizzes.Count(q => q.ParentQuizId == null)
                };

                // Only add Status property for authenticated users
                if (isAuthenticated && userId.HasValue)
                {
                    var linkStatus = await CalculateLinkProgress(userId.Value, link.Id);
                    linkDto.Status = linkStatus;
                }

                learnHubDto.Links.Add(linkDto);
            }

            result.Add(learnHubDto);
        }

        return result;
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

        var hasProgress = await _context.StudentPerformanceSummaries
            .AnyAsync(s => s.LinkId == id);
        if (!hasProgress)
            hasProgress = await _context.StudentQuizPerformances
                .AnyAsync(p => p.LinkId == id);
        if (hasProgress)
            throw new InvalidOperationException("Ky seksion nuk mund të fshihet sepse ka progres të nxënësve të regjistruar. Hiqni të gjitha të dhënat e nxënësve para se ta fshini.");

        // Collect all file IDs to delete before removing the link
        var filesToDelete = new List<Guid>();
        
        foreach (var quiz in link.Quizzes)
        {
            if (quiz.QuestionAudioId.HasValue)
                filesToDelete.Add(quiz.QuestionAudioId.Value);

            if (quiz.QuestionImageId.HasValue)
                filesToDelete.Add(quiz.QuestionImageId.Value);

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

        ValidateQuizDto(quizType.Name, dto);

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
            QuestionImageId = !string.IsNullOrEmpty(dto.QuestionImageId) ? Guid.Parse(dto.QuestionImageId) : null,
            ExplanationAudioId = !string.IsNullOrEmpty(dto.ExplanationAudioId) ? Guid.Parse(dto.ExplanationAudioId) : null,
            ExplanationImageId = !string.IsNullOrEmpty(dto.ExplanationImageId) ? Guid.Parse(dto.ExplanationImageId) : null,
            ParentQuizId = parentQuizId,
            ConceptTagId = dto.ConceptTagId,
            Options = dto.Options.Select(o => new Option
            {
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                OptionImageId = !string.IsNullOrEmpty(o.OptionImageId) ? Guid.Parse(o.OptionImageId) : null
            }).ToList()
        };

        _context.Quizzes.Add(newQuizz);
        await _context.SaveChangesAsync();

        await SaveDndPayloadAsync(newQuizz.Id, quizType.Name, dto);

        return newQuizz.Id;
    }

    private static void ValidateQuizDto(string quizTypeName, CreateQuizzDTO dto)
    {
        var isDragSpell = quizTypeName == "DragSpell";
        var isDragOrder = quizTypeName == "DragOrder";
        var isDragMatch = quizTypeName == "DragMatch";
        var isDnD = isDragSpell || isDragOrder || isDragMatch;

        if (isDnD)
        {
            if (isDragSpell)
            {
                if (dto.DndSpell == null)
                    throw new QuizValidationException("dndSpell është i detyrueshëm për tipin DragSpell");
                if (dto.DndOrder != null || dto.DndMatch != null)
                    throw new QuizValidationException("Vetëm dndSpell duhet të jetë i pranishëm për tipin DragSpell");

                var spell = dto.DndSpell;
                if (string.IsNullOrWhiteSpace(spell.Word))
                    throw new QuizValidationException("dndSpell.word është i detyrueshëm për tipin DragSpell");
                if (spell.Letters == null || spell.Letters.Count < 2)
                    throw new QuizValidationException("dndSpell.letters duhet të ketë të paktën 2 elemente");
                if (spell.Letters.Any(l => string.IsNullOrWhiteSpace(l) || l.Trim().Length != 1))
                    throw new QuizValidationException("dndSpell.letters çdo element duhet të jetë një karakter i vetëm");

                // Verify letters contain all characters needed to form the word
                var wordUpper = spell.Word.Trim().ToUpperInvariant();
                var letterList = spell.Letters.Select(l => l.Trim().ToUpperInvariant()[0]).ToList();
                var wordFreq = wordUpper.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                var letterFreq = letterList.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                foreach (var kvp in wordFreq)
                {
                    if (!letterFreq.TryGetValue(kvp.Key, out int count) || count < kvp.Value)
                        throw new QuizValidationException("dndSpell.letters nuk përmbajnë të gjitha karakteret e nevojshme për të formuar fjalën");
                }
            }
            else if (isDragOrder)
            {
                if (dto.DndOrder == null)
                    throw new QuizValidationException("dndOrder është i detyrueshëm për tipin DragOrder");
                if (dto.DndSpell != null || dto.DndMatch != null)
                    throw new QuizValidationException("Vetëm dndOrder duhet të jetë i pranishëm për tipin DragOrder");

                var order = dto.DndOrder;
                if (order.Tiles == null || order.Tiles.Count < 2)
                    throw new QuizValidationException("dndOrder.tiles duhet të ketë të paktën 2 elementë");
                if (order.Tiles.Any(t => string.IsNullOrWhiteSpace(t.Text)))
                    throw new QuizValidationException("dndOrder.tiles çdo pllakë duhet të ketë tekst");
                if (order.CorrectOrder == null || order.CorrectOrder.Count == 0)
                    throw new QuizValidationException("dndOrder.correctOrder është i detyrueshëm");
                if (order.CorrectOrder.Count != order.Tiles.Count)
                    throw new QuizValidationException("dndOrder.correctOrder duhet të ketë të njëjtin numër elementësh si tiles");
                if (order.CorrectOrder.Any(i => i < 0 || i >= order.Tiles.Count))
                    throw new QuizValidationException("dndOrder.correctOrder ka indekse jo të vlefshëm");
                if (order.CorrectOrder.Distinct().Count() != order.CorrectOrder.Count)
                    throw new QuizValidationException("dndOrder.correctOrder ka indekse të përsëritur");
            }
            else // isDragMatch
            {
                if (dto.DndMatch == null)
                    throw new QuizValidationException("dndMatch është i detyrueshëm për tipin DragMatch");
                if (dto.DndSpell != null || dto.DndOrder != null)
                    throw new QuizValidationException("Vetëm dndMatch duhet të jetë i pranishëm për tipin DragMatch");

                var match = dto.DndMatch;
                if (match.Pairs == null || match.Pairs.Count < 2)
                    throw new QuizValidationException("dndMatch.pairs duhet të ketë të paktën 2 çifte");
                if (match.Pairs.Any(p => string.IsNullOrWhiteSpace(p.Word)))
                    throw new QuizValidationException("dndMatch.pairs çdo çift duhet të ketë fjalën");

                var words = match.Pairs.Select(p => p.Word.Trim().ToLowerInvariant()).ToList();
                if (words.Distinct().Count() != words.Count)
                    throw new QuizValidationException("dndMatch.pairs fjalët nuk duhet të përsëriten");
            }
        }
        else
        {
            // Non-DnD: validate options
            if (dto.Options == null || dto.Options.Count < 2)
                throw new QuizValidationException("Kërkohen të paktën 2 opsione");
            if (!dto.Options.Any(o => o.IsCorrect))
                throw new QuizValidationException("Të paktën një opsion duhet të jetë i saktë");
            if (dto.Options.Any(o => string.IsNullOrWhiteSpace(o.OptionText)))
                throw new QuizValidationException("Çdo opsion duhet të ketë tekst");
        }
    }

    private async Task SaveDndPayloadAsync(Guid quizzId, string quizTypeName, CreateQuizzDTO dto)
    {
        if (quizTypeName == "DragSpell" && dto.DndSpell != null)
        {
            var payload = new DragSpellPayload
            {
                Id = Guid.NewGuid(),
                QuizzId = quizzId,
                Word = dto.DndSpell.Word,
                Letters = string.Join(",", dto.DndSpell.Letters),
                Hint = dto.DndSpell.Hint,
                ImageFileId = !string.IsNullOrEmpty(dto.DndSpell.ImageFileId) ? Guid.Parse(dto.DndSpell.ImageFileId) : null
            };
            _context.DragSpellPayloads.Add(payload);
            await _context.SaveChangesAsync();
        }
        else if (quizTypeName == "DragOrder" && dto.DndOrder != null)
        {
            var payload = new DragOrderPayload
            {
                Id = Guid.NewGuid(),
                QuizzId = quizzId,
                CorrectOrder = string.Empty
            };
            _context.DragOrderPayloads.Add(payload);
            await _context.SaveChangesAsync();

            var tiles = dto.DndOrder.Tiles.Select((t, i) => new DragOrderTile
            {
                Id = Guid.NewGuid(),
                DragOrderPayloadId = payload.Id,
                Text = t.Text,
                SortOrder = i
            }).ToList();
            _context.DragOrderTiles.AddRange(tiles);

            // Map 0-based indices from the admin DTO to stable tile UUIDs
            payload.CorrectOrder = string.Join(",", dto.DndOrder.CorrectOrder.Select(idx => tiles[idx].Id.ToString()));
            await _context.SaveChangesAsync();
        }
        else if (quizTypeName == "DragMatch" && dto.DndMatch != null)
        {
            var payload = new DragMatchPayload
            {
                Id = Guid.NewGuid(),
                QuizzId = quizzId
            };
            _context.DragMatchPayloads.Add(payload);
            await _context.SaveChangesAsync();

            var pairs = dto.DndMatch.Pairs.Select(p => new DragMatchPair
            {
                Id = Guid.NewGuid(),
                DragMatchPayloadId = payload.Id,
                Word = p.Word,
                ImageFileId = !string.IsNullOrEmpty(p.ImageFileId) ? Guid.Parse(p.ImageFileId) : null
            }).ToList();
            _context.DragMatchPairs.AddRange(pairs);
            await _context.SaveChangesAsync();
        }
    }

    private async Task DeleteDndPayloadAsync(Guid quizzId)
    {
        var spell = await _context.DragSpellPayloads.FirstOrDefaultAsync(p => p.QuizzId == quizzId);
        if (spell != null)
        {
            _context.DragSpellPayloads.Remove(spell);
            await _context.SaveChangesAsync();
        }

        var order = await _context.DragOrderPayloads
            .Include(p => p.Tiles)
            .FirstOrDefaultAsync(p => p.QuizzId == quizzId);
        if (order != null)
        {
            _context.DragOrderPayloads.Remove(order);
            await _context.SaveChangesAsync();
        }

        var match = await _context.DragMatchPayloads
            .Include(p => p.Pairs)
            .FirstOrDefaultAsync(p => p.QuizzId == quizzId);
        if (match != null)
        {
            _context.DragMatchPayloads.Remove(match);
            await _context.SaveChangesAsync();
        }
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
            .Include(q => q.QuestionImage)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.ExplanationImage)
            .Include(q => q.QuizzType)
            .Include(q => q.DragSpellPayload)
                .ThenInclude(p => p.ImageFile)
            .Include(q => q.DragOrderPayload)
                .ThenInclude(p => p.Tiles)
            .Include(q => q.DragMatchPayload)
                .ThenInclude(p => p.Pairs)
                    .ThenInclude(pair => pair.ImageFile)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.Options)
                    .ThenInclude(o => o.OptionImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuestionAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuestionImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.ExplanationAudio)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.ExplanationImage)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.QuizzType)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.DragSpellPayload)
                    .ThenInclude(p => p.ImageFile)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.DragOrderPayload)
                    .ThenInclude(p => p.Tiles)
            .Include(q => q.ChildQuizzes)
                .ThenInclude(cq => cq.DragMatchPayload)
                    .ThenInclude(p => p.Pairs)
                        .ThenInclude(pair => pair.ImageFile)
            .AsNoTracking()
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
            QuestionImageId = quiz.QuestionImageId.HasValue ? quiz.QuestionImageId.Value.ToString() : null,
            ExplanationAudioId = quiz.ExplanationAudioId.HasValue ? quiz.ExplanationAudioId.Value.ToString() : null,
            ExplanationImageId = quiz.ExplanationImageId.HasValue ? quiz.ExplanationImageId.Value.ToString() : null,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            QuestionImageUrl = GetFullUrl(quiz.QuestionImage?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            ExplanationImageUrl = GetFullUrl(quiz.ExplanationImage?.FileUrl),
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
            ChildQuizzes = quiz.ParentQuizId == null ? quiz.ChildQuizzes.Select(cq => (object)MapToChildGetQuizzDTO(cq)).ToList() : new List<object>(),
            DndSpell = MapDndSpellAdmin(quiz.DragSpellPayload),
            DndOrder = MapDndOrderAdmin(quiz.DragOrderPayload),
            DndMatch = MapDndMatchAdmin(quiz.DragMatchPayload)
        };
    }

    private DragSpellAdminDTO? MapDndSpellAdmin(DragSpellPayload? payload)
    {
        if (payload == null) return null;
        return new DragSpellAdminDTO
        {
            Word = payload.Word,
            Letters = payload.Letters.Split(',').ToList(),
            Hint = payload.Hint,
            ImageFileId = payload.ImageFileId?.ToString(),
            ImageUrl = GetFullUrl(payload.ImageFile?.FileUrl)
        };
    }

    private DragOrderAdminDTO? MapDndOrderAdmin(DragOrderPayload? payload)
    {
        if (payload == null) return null;
        var tileById = payload.Tiles.ToDictionary(t => t.Id.ToString());
        return new DragOrderAdminDTO
        {
            Tiles = payload.Tiles.OrderBy(t => t.SortOrder).Select(t => new DragOrderTileDTO
            {
                Id = t.Id.ToString(),
                Text = t.Text
            }).ToList(),
            CorrectOrder = payload.CorrectOrder.Split(',')
                .Where(id => !string.IsNullOrEmpty(id))
                .ToList()
        };
    }

    private DragMatchAdminDTO? MapDndMatchAdmin(DragMatchPayload? payload)
    {
        if (payload == null) return null;
        return new DragMatchAdminDTO
        {
            Pairs = payload.Pairs.Select(p => new DragMatchPairAdminDTO
            {
                Id = p.Id.ToString(),
                Word = p.Word,
                ImageFileId = p.ImageFileId?.ToString(),
                ImageUrl = GetFullUrl(p.ImageFile?.FileUrl)
            }).ToList()
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
            QuestionImageId = quiz.QuestionImageId.HasValue ? quiz.QuestionImageId.Value.ToString() : null,
            ExplanationAudioId = quiz.ExplanationAudioId.HasValue ? quiz.ExplanationAudioId.Value.ToString() : null,
            ExplanationImageId = quiz.ExplanationImageId.HasValue ? quiz.ExplanationImageId.Value.ToString() : null,
            QuestionAudioUrl = GetFullUrl(quiz.QuestionAudio?.FileUrl),
            QuestionImageUrl = GetFullUrl(quiz.QuestionImage?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            ExplanationImageUrl = GetFullUrl(quiz.ExplanationImage?.FileUrl),
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
            DndSpell = MapDndSpellAdmin(quiz.DragSpellPayload),
            DndOrder = MapDndOrderAdmin(quiz.DragOrderPayload),
            DndMatch = MapDndMatchAdmin(quiz.DragMatchPayload)
        };
    }
    public async Task<Quizz> UpdateQuizz(Guid id, CreateQuizzDTO dto)
    {
        var quizz = await _context.Quizzes
            .Include(q => q.Options)
            .Include(q => q.QuizzType)
            .Include(q => q.DragSpellPayload)
            .Include(q => q.DragOrderPayload).ThenInclude(p => p.Tiles)
            .Include(q => q.DragMatchPayload).ThenInclude(p => p.Pairs)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quizz == null)
            throw new Exception("Quizz not found");

        // Validate quiz type
        if (!Guid.TryParse(dto.QuizType, out Guid quizTypeId))
            throw new Exception("Invalid quiz type ID");

        var quizType = await _context.QuizTypes.FindAsync(quizTypeId);
        if (quizType == null)
            throw new Exception("Quiz type not found");

        // Collect old DnD image IDs now, before any payload is deleted, so they can be
        // cleaned up from storage after the transaction commits
        var oldDndImageIds = await CollectDndImageFileIds(quizz.Id);

        // Type-change cleanup: if the quiz type is changing, mark the old DnD payload for
        // deletion so it is removed together with the rest of the update in one transaction
        var oldTypeName = quizz.QuizzType.Name;
        var newTypeName = quizType.Name;

        if (oldTypeName != newTypeName)
        {
            if (oldTypeName == "DragSpell" && quizz.DragSpellPayload != null)
                _context.DragSpellPayloads.Remove(quizz.DragSpellPayload);
            else if (oldTypeName == "DragOrder" && quizz.DragOrderPayload != null)
                _context.DragOrderPayloads.Remove(quizz.DragOrderPayload);
            else if (oldTypeName == "DragMatch" && quizz.DragMatchPayload != null)
                _context.DragMatchPayloads.Remove(quizz.DragMatchPayload);
        }

        ValidateQuizDto(newTypeName, dto);

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
        var oldQuestionImageId = quizz.QuestionImageId;
        var oldExplanationAudioId = quizz.ExplanationAudioId;
        var oldExplanationImageId = quizz.ExplanationImageId;
        var oldOptionImageIds = quizz.Options
            .Where(o => o.OptionImageId.HasValue)
            .Select(o => o.OptionImageId.Value)
            .ToList();

        quizz.Question = dto.Question;
        quizz.Explanation = dto.Explanation;
        quizz.Points = dto.Points;
        quizz.QuizzTypeId = quizTypeId;
        quizz.QuestionAudioId = !string.IsNullOrEmpty(dto.QuestionAudioId) ? Guid.Parse(dto.QuestionAudioId) : null;
        quizz.QuestionImageId = !string.IsNullOrEmpty(dto.QuestionImageId) ? Guid.Parse(dto.QuestionImageId) : null;
        quizz.ExplanationAudioId = !string.IsNullOrEmpty(dto.ExplanationAudioId) ? Guid.Parse(dto.ExplanationAudioId) : null;
        quizz.ExplanationImageId = !string.IsNullOrEmpty(dto.ExplanationImageId) ? Guid.Parse(dto.ExplanationImageId) : null;
        quizz.ParentQuizId = parentQuizId;
        quizz.ConceptTagId = dto.ConceptTagId;

        // Remove old options
        _context.Options.RemoveRange(quizz.Options);

        // Add new options
        quizz.Options = dto.Options.Select(o => new Option
        {
            OptionText = o.OptionText,
            IsCorrect = o.IsCorrect,
            OptionImageId = !string.IsNullOrEmpty(o.OptionImageId) ? Guid.Parse(o.OptionImageId) : null
        }).ToList();

        await using var transaction = await _context.Database.BeginTransactionAsync();
        await _context.SaveChangesAsync();
        await DeleteDndPayloadAsync(quizz.Id);
        await SaveDndPayloadAsync(quizz.Id, newTypeName, dto);
        await transaction.CommitAsync();

        // Clean up old files that are no longer referenced (runs after commit so storage
        // cleanup never races with a potential transaction rollback)
        await CleanupUnusedFiles(oldQuestionAudioId, oldQuestionImageId, oldExplanationAudioId, oldExplanationImageId, oldOptionImageIds, dto, oldDndImageIds);

        return quizz;
    }

    private async Task<List<Guid>> CollectDndImageFileIds(Guid quizzId)
    {
        var ids = new List<Guid>();

        var spell = await _context.DragSpellPayloads.FirstOrDefaultAsync(p => p.QuizzId == quizzId);
        if (spell?.ImageFileId.HasValue == true)
            ids.Add(spell.ImageFileId.Value);

        var match = await _context.DragMatchPayloads
            .Include(p => p.Pairs)
            .FirstOrDefaultAsync(p => p.QuizzId == quizzId);
        if (match != null)
            ids.AddRange(match.Pairs.Where(p => p.ImageFileId.HasValue).Select(p => p.ImageFileId.Value));

        return ids;
    }

    private async Task CleanupUnusedFiles(Guid? oldQuestionAudioId, Guid? oldQuestionImageId, Guid? oldExplanationAudioId, Guid? oldExplanationImageId, List<Guid> oldOptionImageIds, CreateQuizzDTO newDto, List<Guid>? oldDndImageIds = null)
    {
        var filesToDelete = new List<Guid>();

        if (oldQuestionAudioId.HasValue &&
            (string.IsNullOrEmpty(newDto.QuestionAudioId) || Guid.Parse(newDto.QuestionAudioId) != oldQuestionAudioId.Value))
            filesToDelete.Add(oldQuestionAudioId.Value);

        if (oldQuestionImageId.HasValue &&
            (string.IsNullOrEmpty(newDto.QuestionImageId) || Guid.Parse(newDto.QuestionImageId) != oldQuestionImageId.Value))
            filesToDelete.Add(oldQuestionImageId.Value);

        if (oldExplanationAudioId.HasValue &&
            (string.IsNullOrEmpty(newDto.ExplanationAudioId) || Guid.Parse(newDto.ExplanationAudioId) != oldExplanationAudioId.Value))
            filesToDelete.Add(oldExplanationAudioId.Value);

        if (oldExplanationImageId.HasValue &&
            (string.IsNullOrEmpty(newDto.ExplanationImageId) || Guid.Parse(newDto.ExplanationImageId) != oldExplanationImageId.Value))
            filesToDelete.Add(oldExplanationImageId.Value);

        var newOptionImageIds = newDto.Options
            .Where(o => !string.IsNullOrEmpty(o.OptionImageId))
            .Select(o => Guid.Parse(o.OptionImageId))
            .ToList();

        foreach (var oldImageId in oldOptionImageIds)
        {
            if (!newOptionImageIds.Contains(oldImageId))
                filesToDelete.Add(oldImageId);
        }

        // Collect new DnD image IDs so we can keep any that are reused
        var newDndImageIds = new List<Guid>();
        if (newDto.DndSpell?.ImageFileId != null)
            newDndImageIds.Add(Guid.Parse(newDto.DndSpell.ImageFileId));
        if (newDto.DndMatch?.Pairs != null)
            newDndImageIds.AddRange(newDto.DndMatch.Pairs
                .Where(p => !string.IsNullOrEmpty(p.ImageFileId))
                .Select(p => Guid.Parse(p.ImageFileId)));

        if (oldDndImageIds != null)
        {
            foreach (var oldId in oldDndImageIds)
            {
                if (!newDndImageIds.Contains(oldId))
                    filesToDelete.Add(oldId);
            }
        }

        var anyOrphaned = false;
        foreach (var fileId in filesToDelete)
        {
            if (!await TryDeleteOrOrphanAsync(fileId, "quiz-update"))
                anyOrphaned = true;
        }
        if (anyOrphaned)
            await _context.SaveChangesAsync();
    }

    // Attempts to delete a file from storage. On failure, logs an OrphanedFile record so the
    // nightly cleanup job can retry. Returns true if the file was deleted (or already gone).
    private async Task<bool> TryDeleteOrOrphanAsync(Guid fileId, string reason)
    {
        try
        {
            await _fileService.DeleteFileAsync(fileId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete file {FileId} ({Reason}), logging as orphaned", fileId, reason);
            _context.OrphanedFiles.Add(new OrphanedFile
            {
                Id = Guid.NewGuid(),
                FileId = fileId,
                CreatedAt = DateTime.UtcNow,
                Reason = reason
            });
            return false;
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

        // Collect all IDs (parent + children) to check student data
        var allQuizIds = quizz.ChildQuizzes.Select(c => c.Id).ToList();
        allQuizIds.Add(id);

        var hasAttempts = await _context.StudentQuizPerformances
            .AnyAsync(p => allQuizIds.Contains(p.QuizId));
        if (!hasAttempts)
            hasAttempts = await _context.StudentQuizResults
                .AnyAsync(r => allQuizIds.Contains(r.QuizId));
        if (hasAttempts)
            throw new InvalidOperationException("Ky kuiz nuk mund të fshihet sepse ka tentativa të nxënësve të regjistruara. Hiqni të dhënat e nxënësve para se ta fshini.");

        // Collect all file IDs to delete (including child quizzes)
        var filesToDelete = new List<Guid>();

        if (quizz.QuestionAudioId.HasValue)
            filesToDelete.Add(quizz.QuestionAudioId.Value);

        if (quizz.QuestionImageId.HasValue)
            filesToDelete.Add(quizz.QuestionImageId.Value);

        if (quizz.ExplanationAudioId.HasValue)
            filesToDelete.Add(quizz.ExplanationAudioId.Value);

        if (quizz.ExplanationImageId.HasValue)
            filesToDelete.Add(quizz.ExplanationImageId.Value);

        foreach (var option in quizz.Options)
        {
            if (option.OptionImageId.HasValue)
                filesToDelete.Add(option.OptionImageId.Value);
        }

        // Collect DnD image files for parent quiz
        filesToDelete.AddRange(await CollectDndImageFileIds(quizz.Id));

        // Collect files from child quizzes
        foreach (var childQuiz in quizz.ChildQuizzes)
        {
            if (childQuiz.QuestionAudioId.HasValue)
                filesToDelete.Add(childQuiz.QuestionAudioId.Value);

            if (childQuiz.QuestionImageId.HasValue)
                filesToDelete.Add(childQuiz.QuestionImageId.Value);

            if (childQuiz.ExplanationAudioId.HasValue)
                filesToDelete.Add(childQuiz.ExplanationAudioId.Value);

            if (childQuiz.ExplanationImageId.HasValue)
                filesToDelete.Add(childQuiz.ExplanationImageId.Value);

            foreach (var option in childQuiz.Options)
            {
                if (option.OptionImageId.HasValue)
                    filesToDelete.Add(option.OptionImageId.Value);
            }

            filesToDelete.AddRange(await CollectDndImageFileIds(childQuiz.Id));
        }

        // Remove child quizzes first (since we use NoAction instead of Cascade)
        foreach (var childQuiz in quizz.ChildQuizzes.ToList())
        {
            _context.Quizzes.Remove(childQuiz);
        }
        
        // Remove the parent quiz
        _context.Quizzes.Remove(quizz);
        await _context.SaveChangesAsync();

        // Delete associated files; failures are logged as OrphanedFiles for the cleanup job
        var anyOrphaned = false;
        foreach (var fileId in filesToDelete)
        {
            if (!await TryDeleteOrOrphanAsync(fileId, "quiz-deleted"))
                anyOrphaned = true;
        }
        if (anyOrphaned)
            await _context.SaveChangesAsync();
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
                ParentQuizId = q.ParentQuizId,
                Points = q.Points
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
            .Include(q => q.QuestionImage)
            .Include(q => q.ExplanationAudio)
            .Include(q => q.ExplanationImage)
            .Include(q => q.QuizzType)
            .Include(q => q.DragSpellPayload)
                .ThenInclude(p => p.ImageFile)
            .Include(q => q.DragOrderPayload)
                .ThenInclude(p => p.Tiles)
            .Include(q => q.DragMatchPayload)
                .ThenInclude(p => p.Pairs)
                    .ThenInclude(pair => pair.ImageFile)
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
            QuestionImageUrl = GetFullUrl(quiz.QuestionImage?.FileUrl),
            ExplanationAudioUrl = GetFullUrl(quiz.ExplanationAudio?.FileUrl),
            ExplanationImageUrl = GetFullUrl(quiz.ExplanationImage?.FileUrl),
            QuestionAudioId = quiz.QuestionAudioId.HasValue ? quiz.QuestionAudioId.Value.ToString() : null,
            QuestionImageId = quiz.QuestionImageId.HasValue ? quiz.QuestionImageId.Value.ToString() : null,
            ExplanationAudioId = quiz.ExplanationAudioId.HasValue ? quiz.ExplanationAudioId.Value.ToString() : null,
            ExplanationImageId = quiz.ExplanationImageId.HasValue ? quiz.ExplanationImageId.Value.ToString() : null,
            QuizType = quiz.QuizzTypeId.ToString("D"),
            ParentQuizId = quiz.ParentQuizId,
            DndSpell = MapDndSpellAdmin(quiz.DragSpellPayload),
            DndOrder = MapDndOrderAdmin(quiz.DragOrderPayload),
            DndMatch = MapDndMatchAdmin(quiz.DragMatchPayload),
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
        var completedQuizzes = await CountCompletedParentQuizzesInLearnHub(studentId, linkId, parentQuizzes, studentPerformances);
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
        var completedQuizzes = await CountCompletedParentQuizzesInLearnHub(studentId, linkId, parentQuizzes, studentPerformances);
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

    private async Task<int> CountCompletedParentQuizzesInLearnHub(Guid studentId, Guid linkId, List<Quizz> parentQuizzes, List<StudentQuizPerformance> performances)
    {
        int completedCount = 0;

        foreach (var parentQuiz in parentQuizzes)
        {
            // Check if parent quiz has been answered directly
            var parentPerformance = performances.FirstOrDefault(p => p.QuizId == parentQuiz.Id);
            if (parentPerformance != null)
            {
                completedCount++;
                continue;
            }

            // Check if parent quiz has child quizzes and all child quizzes have been answered
            var childQuizzes = await _context.Quizzes
                .Where(q => q.ParentQuizId == parentQuiz.Id)
                .ToListAsync();

            if (childQuizzes.Any())
            {
                // Get all child quiz performances for this student
                var childQuizPerformances = await _context.StudentQuizPerformances
                    .Where(sqp => sqp.StudentId == studentId && 
                                 childQuizzes.Select(cq => cq.Id).Contains(sqp.QuizId) &&
                                 sqp.LinkId == linkId)
                    .ToListAsync();

                // Check if all child quizzes have been answered (regardless of correctness)
                var allChildQuizzesAnswered = childQuizzes.All(childQuiz => 
                    childQuizPerformances.Any(perf => perf.QuizId == childQuiz.Id));

                if (allChildQuizzesAnswered)
                {
                    completedCount++;
                }
            }
        }

        return completedCount;
    }
}