using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly ApplicationDbContext _context;

        public AdminUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserListDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Role = u.Role.ToString(),
                    School = u.School,
                    City = u.City
                })
                .ToListAsync();
        }

        public async Task<AdminUserDetailsDTO> GetUserDetailsById(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new AdminUserDetailsDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                School = user.School,
                City = user.City,
                ApprovalStatus = user.ApprovalStatus.ToString()
            };
        }

        public async Task UpdateUser(AdminUserDetailsDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.Id);
            if (user == null)
                throw new Exception("User not found");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Role = Enum.Parse<UserRole>(dto.Role);
            user.School = dto.School;
            user.City = dto.City;
            user.ApprovalStatus = Enum.Parse<ApprovalStatus>(dto.ApprovalStatus);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) throw new Exception("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
