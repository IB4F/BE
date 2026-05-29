using TeachingBACKEND.Domain.DTOs;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IFamilyService
    {
        Task<CreateChildrenBulkResponseDto> CreateChildrenBulkAsync(Guid parentId, CreateChildrenBulkRequestDto dto);
        Task<ChildCreatedDto> CreateChildAsync(Guid parentId, CreateChildInputDto dto);
        Task<List<ChildSummaryDto>> GetChildrenAsync(Guid parentId);
        Task<ChildSummaryDto> UpdateChildAsync(Guid parentId, Guid childId, UpdateChildDto dto);
        Task<ChildPasswordResetResponseDto> ResetChildPasswordAsync(Guid parentId, Guid childId);
        Task DeleteChildAsync(Guid parentId, Guid childId);
        Task<ChildSummaryDto> ReactivateChildAsync(Guid parentId, Guid childId);
    }
}
