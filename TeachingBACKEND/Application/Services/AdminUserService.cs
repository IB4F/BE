using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Enums;
using System.Linq;

namespace TeachingBACKEND.Application.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly ApplicationDbContext _context;

        public AdminUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResultDTO<UserListDTO>> GetAllUsers(PaginationRequestDTO dto)
        {
            var query = _context.Users.AsQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(dto.Search))
            {
                var search = dto.Search.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search));
            }

            query = query.OrderByDescending(u => u.CreateAt);
            var totalCount = await query.CountAsync();

            var users = await query
            .Skip((dto.PageNumber) * dto.PageSize)
            .Take(dto.PageSize)
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

            return new PaginatedResultDTO<UserListDTO>
            {
                Items = users,
                TotalCount = totalCount
            };
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
