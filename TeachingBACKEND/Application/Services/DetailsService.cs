using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class DetailsService : IDetailsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _cache;

        private static readonly TimeSpan LookupCacheDuration = TimeSpan.FromHours(1);
        private static readonly TimeSpan PlanCacheDuration = TimeSpan.FromMinutes(30);

        public DetailsService(ApplicationDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await _cache.GetOrCreateAsync("details:cities", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = LookupCacheDuration;
                return await _context.Cities.AsNoTracking().ToListAsync();
            }) ?? new List<City>();
        }

        public async Task<IEnumerable<Class>> GetClasses()
        {
            return await _cache.GetOrCreateAsync("details:classes", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = LookupCacheDuration;
                return await _context.Classes
                    .AsNoTracking()
                    .OrderBy(c => c.Name.Length)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }) ?? new List<Class>();
        }

        public async Task<IEnumerable<Subjects>> GetSubjects()
        {
            return await _cache.GetOrCreateAsync("details:subjects", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = LookupCacheDuration;
                return await _context.Subjects
                    .AsNoTracking()
                    .OrderBy(c => c.Name.Length)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }) ?? new List<Subjects>();
        }

        public async Task<IEnumerable<QuizType>> GetQuizTypes()
        {
            return await _cache.GetOrCreateAsync("details:quizTypes", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = LookupCacheDuration;
                return await _context.QuizTypes
                    .AsNoTracking()
                    .OrderBy(c => c.Name.Length)
                    .ThenBy(c => c.Name)
                    .ToListAsync();
            }) ?? new List<QuizType>();
        }

        public async Task<List<SubscriptionPackage>> GetAllPlansAsync()
        {
            return await _cache.GetOrCreateAsync("details:plans", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = PlanCacheDuration;
                return await _context.SubscriptionPackages.AsNoTracking().ToListAsync();
            }) ?? new List<SubscriptionPackage>();
        }

        public async Task<SubscriptionPackage?> GetPlanByIdAsync(Guid id)
        {
            var all = await GetAllPlansAsync();
            return all.FirstOrDefault(p => p.Id == id);
        }
    }
}
