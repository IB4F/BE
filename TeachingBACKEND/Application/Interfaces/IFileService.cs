using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Interfaces
{
    public interface IFileService
    {
        Task<UploadedFile> UploadFileAsync(IFormFile file, FileType fileType, Guid uploadedBy);
        Task<UploadedFile> GetFileByIdAsync(Guid id);
        Task<bool> DeleteFileAsync(Guid id);
        Task<string> GetFileUrlAsync(Guid id);
    }
}
