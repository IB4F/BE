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
            var cities = await _context.Cities.ToListAsync();
            return cities.Any() == true ? cities : new List<City>();
        }


        public async Task<IEnumerable<Class>> GetClasses()
        {
            var classes = await _context.Classes
                .OrderBy(c => c.Name.Length)   
                .ThenBy(c => c.Name)   
                .ToListAsync();

            return classes.Any()
                 ? classes
                 : new List<Class>();
        }

    }
}




