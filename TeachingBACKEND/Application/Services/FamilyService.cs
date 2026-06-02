using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.DTOs;
using TeachingBACKEND.Domain.Entities;
using TeachingBACKEND.Domain.Enums;

namespace TeachingBACKEND.Application.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<FamilyService> _logger;

        public FamilyService(
            ApplicationDbContext context,
            IPasswordService passwordService,
            INotificationService notificationService,
            ILogger<FamilyService> logger)
        {
            _context = context;
            _passwordService = passwordService;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task<CreateChildrenBulkResponseDto> CreateChildrenBulkAsync(Guid parentId, CreateChildrenBulkRequestDto dto)
        {
            var parent = await GetVerifiedParentAsync(parentId);

            await ValidateChildLimitAsync(parent, dto.Children.Count);

            var created = new List<ChildCreatedDto>();
            var credentials = new List<(string Name, string Email, string Password)>();

            foreach (var input in dto.Children)
            {
                var (child, password) = await CreateChildEntityAsync(parent, input);
                _context.Users.Add(child);
                created.Add(MapToCreatedDto(child, password, input.CurrentClass));
                credentials.Add(($"{child.FirstName} {child.LastName}", child.Email, password));
            }

            await _context.SaveChangesAsync();

            await _notificationService.SendNewChildrenCredentialsToParent(
                parent.Email,
                $"{parent.FirstName} {parent.LastName}",
                credentials);

            _logger.LogInformation("Created {Count} children for parent {ParentId}", created.Count, parentId);
            return new CreateChildrenBulkResponseDto { Children = created };
        }

        public async Task<ChildCreatedDto> CreateChildAsync(Guid parentId, CreateChildInputDto dto)
        {
            var parent = await GetVerifiedParentAsync(parentId);

            await ValidateChildLimitAsync(parent, 1);

            var (child, password) = await CreateChildEntityAsync(parent, dto);
            _context.Users.Add(child);
            await _context.SaveChangesAsync();

            await _notificationService.SendNewChildrenCredentialsToParent(
                parent.Email,
                $"{parent.FirstName} {parent.LastName}",
                [($"{child.FirstName} {child.LastName}", child.Email, password)]);

            _logger.LogInformation("Created child {ChildId} for parent {ParentId}", child.Id, parentId);
            return MapToCreatedDto(child, password, dto.CurrentClass);
        }

        public async Task<List<ChildSummaryDto>> GetChildrenAsync(Guid parentId)
        {
            return await _context.Users
                .Where(u => u.ParentUserId == parentId)
                .Select(u => new ChildSummaryDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    CurrentClass = u.CurrentClass,
                    LastLogin = u.LastLoginAt,
                    IsActive = u.IsActive
                })
                .ToListAsync();
        }

        public async Task<ChildSummaryDto> UpdateChildAsync(Guid parentId, Guid childId, UpdateChildDto dto)
        {
            var child = await GetVerifiedChildAsync(parentId, childId);

            if (!string.IsNullOrWhiteSpace(dto.FirstName))
                child.FirstName = dto.FirstName.Trim();
            if (!string.IsNullOrWhiteSpace(dto.LastName))
                child.LastName = dto.LastName.Trim();
            if (dto.CurrentClass != null)
                child.CurrentClass = dto.CurrentClass;

            await _context.SaveChangesAsync();

            return new ChildSummaryDto
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                Email = child.Email,
                CurrentClass = child.CurrentClass,
                LastLogin = child.LastLoginAt,
                IsActive = child.IsActive
            };
        }

        public async Task<ChildPasswordResetResponseDto> ResetChildPasswordAsync(Guid parentId, Guid childId)
        {
            var parent = await GetVerifiedParentAsync(parentId);
            var child = await GetVerifiedChildAsync(parentId, childId);

            var newPassword = _passwordService.GenerateRandomPassword();
            child.PasswordHash = _passwordService.HashPassword(newPassword);
            child.MustChangePasswordOnNextLogin = false;

            await _context.SaveChangesAsync();

            await _notificationService.SendChildPasswordResetToParent(
                parent.Email,
                $"{parent.FirstName} {parent.LastName}",
                $"{child.FirstName} {child.LastName}",
                child.Email,
                newPassword);

            _logger.LogInformation("Password reset for child {ChildId} by parent {ParentId}", childId, parentId);
            return new ChildPasswordResetResponseDto { NewPassword = newPassword };
        }

        public async Task DeleteChildAsync(Guid parentId, Guid childId)
        {
            var child = await GetVerifiedChildAsync(parentId, childId);

            var hasHistory = await _context.StudentQuizPerformances.AnyAsync(a => a.StudentId == childId)
                          || await _context.StudentQuizSessions.AnyAsync(s => s.StudentId == childId)
                          || await _context.StudentQuizResults.AnyAsync(r => r.StudentId == childId)
                          || await _context.StudentPerformanceSummaries.AnyAsync(p => p.StudentId == childId);

            if (hasHistory)
            {
                child.IsActive = false;
                _logger.LogInformation("Soft-deleted child {ChildId} (has history) for parent {ParentId}", childId, parentId);
            }
            else
            {
                _context.Users.Remove(child);
                _logger.LogInformation("Hard-deleted child {ChildId} (no history) for parent {ParentId}", childId, parentId);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ChildSummaryDto> ReactivateChildAsync(Guid parentId, Guid childId)
        {
            var parent = await GetVerifiedParentAsync(parentId);
            var child = await GetVerifiedChildAsync(parentId, childId);

            if (child.IsActive)
                throw new InvalidOperationException("This child account is already active.");

            await ValidateChildLimitAsync(parent, 1);

            child.IsActive = true;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Reactivated child {ChildId} for parent {ParentId}", childId, parentId);

            return new ChildSummaryDto
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                Email = child.Email,
                CurrentClass = child.CurrentClass,
                LastLogin = child.LastLoginAt,
                IsActive = child.IsActive
            };
        }

        private async Task<User> GetVerifiedParentAsync(Guid parentId)
        {
            var parent = await _context.Users.FirstOrDefaultAsync(u => u.Id == parentId && u.Role == UserRole.Family);
            if (parent == null)
                throw new UnauthorizedAccessException("Parent account not found.");
            return parent;
        }

        private async Task<User> GetVerifiedChildAsync(Guid parentId, Guid childId)
        {
            var child = await _context.Users.FirstOrDefaultAsync(u => u.Id == childId && u.ParentUserId == parentId);
            if (child == null)
                throw new KeyNotFoundException("Child not found or does not belong to this parent.");
            return child;
        }

        private async Task ValidateChildLimitAsync(User parent, int countToAdd)
        {
            var subscription = await _context.Subscriptions
                .Include(s => s.SubscriptionPackage)
                .FirstOrDefaultAsync(s => s.UserId == parent.Id && s.Status == SubscriptionStatus.Active);

            var maxAllowed = subscription?.SubscriptionPackage?.MaxFamilyMembers ?? 10;
            var current = await _context.Users.CountAsync(u => u.ParentUserId == parent.Id && u.IsActive);

            if (current + countToAdd > maxAllowed)
                throw new InvalidOperationException(
                    $"Cannot add {countToAdd} child(ren). Current: {current}, limit: {maxAllowed}.");
        }

        private async Task<(User child, string password)> CreateChildEntityAsync(User parent, CreateChildInputDto input)
        {
            var firstName = input.FirstName.Trim();
            var lastName = input.LastName.Trim();
            var email = await GenerateUniqueEmailAsync(firstName, lastName);
            var password = _passwordService.GenerateRandomPassword();

            var child = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _passwordService.HashPassword(password),
                Role = UserRole.Student,
                ApprovalStatus = ApprovalStatus.Approved,
                FirstName = firstName,
                LastName = lastName,
                CurrentClass = input.CurrentClass,
                ParentUserId = parent.Id,
                IsEmailVerified = true,
                IsActive = true,
                CreateAt = DateTime.UtcNow
            };

            return (child, password);
        }

        private static ChildCreatedDto MapToCreatedDto(User child, string password, string? currentClass) => new()
        {
            Id = child.Id,
            FirstName = child.FirstName,
            LastName = child.LastName,
            Email = child.Email,
            TemporaryPassword = password,
            CurrentClass = currentClass
        };

        private async Task<string> GenerateUniqueEmailAsync(string firstName, string lastName)
        {
            var baseEmail = $"{firstName.ToLower()}.{lastName.ToLower()}@bga.al";

            if (!await _context.Users.AnyAsync(u => u.Email.ToLower() == baseEmail))
                return baseEmail;

            for (int i = 2; i <= 999; i++)
            {
                var candidate = $"{firstName.ToLower()}.{lastName.ToLower()}{i}@bga.al";
                if (!await _context.Users.AnyAsync(u => u.Email.ToLower() == candidate))
                    return candidate;
            }

            throw new InvalidOperationException($"Unable to generate unique email for {firstName} {lastName}.");
        }
    }
}
