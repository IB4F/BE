using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Education.Classes;
using System.Text.RegularExpressions;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class DetailsService : IDetailsService
    {
        private readonly ApplicationDbContext _context;

        public DetailsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<City>> GetCities()
        {
            var cities = await _context.Cities
                .AsNoTracking() // Performance optimization for read-only data
                .ToListAsync();
            return cities;
        }
        public async Task<IEnumerable<Class>> GetClasses()
        {
            var classes = await _context.Classes
                .AsNoTracking() // Performance optimization for read-only data
                .OrderBy(c => c.Name.Length)   
                .ThenBy(c => c.Name)   
                .ToListAsync();

            return classes;
        }  
        public async Task<IEnumerable<Subjects>> GetSubjects()
        {
            var subjects = await _context.Subjects
                .AsNoTracking() // Performance optimization for read-only data
                .OrderBy(c => c.Name.Length)   
                .ThenBy(c => c.Name)   
                .ToListAsync();

            return subjects;
        }
        public async Task<IEnumerable<QuizType>> GetQuizTypes()
        {
            var quizTypes = await _context.QuizTypes
                .AsNoTracking() // Performance optimization for read-only data
                .OrderBy(c => c.Name.Length)   
                .ThenBy(c => c.Name)   
                .ToListAsync();

            return quizTypes;
        }
        public async Task<List<SubscriptionPackage>> GetAllPlansAsync()
        {
            return await _context.SubscriptionPackages
                .AsNoTracking() // Performance optimization for read-only data
                .ToListAsync();
        }

        public async Task<SubscriptionPackage?> GetPlanByIdAsync(Guid id)
        {
            return await _context.SubscriptionPackages.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}




