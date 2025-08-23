using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Data;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Application.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public FileService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<UploadedFile> UploadFileAsync(IFormFile file, FileType fileType, Guid uploadedBy)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");

            // Validate file type
            var allowedExtensions = fileType == FileType.Audio 
                ? new[] { ".mp3", ".wav", ".m4a", ".aac" }
                : new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException($"Invalid file type. Allowed types: {string.Join(", ", allowedExtensions)}");

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            
            // Determine upload directory based on file type
            var uploadDir = fileType == FileType.Audio ? "audio" : "images";
            var uploadPath = Path.Combine(_environment.WebRootPath, uploadDir);
            
            // Create directory if it doesn't exist
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Create file record in database
            var fileRecord = new UploadedFile
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                OriginalFileName = file.FileName,
                FileUrl = $"/{uploadDir}/{fileName}",
                ContentType = file.ContentType,
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = uploadedBy,
                FileType = fileType
            };

            _context.Files.Add(fileRecord);
            await _context.SaveChangesAsync();

            return fileRecord;
        }

        public async Task<UploadedFile> GetFileByIdAsync(Guid id)
        {
            return await _context.Files.FindAsync(id);
        }

        public async Task<bool> DeleteFileAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            if (file == null)
                return false;

            // Check if file is referenced by any quizzes or options
            var isReferencedByQuizzes = await _context.Quizzes
                .AnyAsync(q => q.QuestionAudioId == id || q.ExplanationAudioId == id);

            var isReferencedByOptions = await _context.Options
                .AnyAsync(o => o.OptionImageId == id);

            if (isReferencedByQuizzes || isReferencedByOptions)
            {
                throw new InvalidOperationException("Cannot delete file because it is referenced by quizzes or options. Please remove the references first.");
            }

            // Delete physical file
            var filePath = Path.Combine(_environment.WebRootPath, file.FileUrl.TrimStart('/'));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Delete database record
            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetFileUrlAsync(Guid id)
        {
            var file = await _context.Files.FindAsync(id);
            return file?.FileUrl;
        }
    }
}
