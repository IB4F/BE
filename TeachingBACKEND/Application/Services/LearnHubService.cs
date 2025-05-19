using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public interface ILearnHubService
    {
        Task<LearnHub> GetLearnHubByIdAsync(Guid id);
        Task<List<LearnHub>> GetAllFreeLearnHubAsync();
        Task<LearnHub> CreateLearnHubAsync(CreateLearnHubDTO learnHubDto);
    }


    public class LearnHubService : ILearnHubService
    {
        private readonly ApplicationDbContext _context;
        public LearnHubService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LearnHub> GetLearnHubByIdAsync(Guid id)
        {
            return await _context.LearnHubs
                .Include(lh => lh.Links)
                .ThenInclude(link => link.Quizzes)
                .FirstOrDefaultAsync(lh => lh.Id == id);
        }


        public async Task<List<LearnHub>> GetAllFreeLearnHubAsync()
        {
            return await _context.LearnHubs.Where(lh => lh.IsFree).ToListAsync();
        }

        public async Task<LearnHub> CreateLearnHubAsync(CreateLearnHubDTO learnHubDto)
        {
            var learnHub = new LearnHub
            {
                Id = Guid.NewGuid(),
                Title = learnHubDto.Title,
                Description = learnHubDto.Description,
                ClassType = learnHubDto.ClassType,
                Subject = learnHubDto.Subject,
                IsFree = learnHubDto.IsFree,
                Links = learnHubDto.Links.Select(linkDto => new Link
                {
                    Id = Guid.NewGuid(),
                    Title = linkDto.Title,
                    Progress = linkDto.Progress,
                    Quizzes = linkDto.Quizzes.Select(quizDto => new Quizz
                    {
                        Id = Guid.NewGuid(),
                        Question = quizDto.Question,
                        Explanation = quizDto.Explanation,
                        Points = quizDto.Points,
                        Options = quizDto.Options
                    }).ToList()
                }).ToList()
            };

            _context.LearnHubs.Add(learnHub);
            await _context.SaveChangesAsync();

            return learnHub;
        }
    }
}
