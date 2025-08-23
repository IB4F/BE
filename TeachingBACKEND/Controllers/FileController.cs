using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeachingBACKEND.Application.Interfaces;
using TeachingBACKEND.Domain.Entities;

namespace TeachingBACKEND.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload-audio")]
        public async Task<IActionResult> UploadAudio(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file provided");

                // Get admin user ID from JWT token
                var adminUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var uploadedFile = await _fileService.UploadFileAsync(file, FileType.Audio, adminUserId);
                
                return Ok(new { 
                    fileId = uploadedFile.Id.ToString(),
                    fileName = uploadedFile.OriginalFileName,
                    fileUrl = uploadedFile.FileUrl
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the file");
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file provided");

                // Get admin user ID from JWT token
                var adminUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var uploadedFile = await _fileService.UploadFileAsync(file, FileType.Image, adminUserId);
                
                return Ok(new { 
                    fileId = uploadedFile.Id.ToString(),
                    fileName = uploadedFile.OriginalFileName,
                    fileUrl = uploadedFile.FileUrl
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while uploading the file");
            }
        }

        [HttpDelete("{fileId}")]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            try
            {
                var deleted = await _fileService.DeleteFileAsync(fileId);
                if (!deleted)
                    return NotFound("File not found");

                return Ok("File deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the file");
            }
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            try
            {
                var file = await _fileService.GetFileByIdAsync(fileId);
                if (file == null)
                    return NotFound("File not found");

                return Ok(new { 
                    fileId = file.Id.ToString(),
                    fileName = file.OriginalFileName,
                    fileUrl = file.FileUrl,
                    contentType = file.ContentType,
                    fileSize = file.FileSize,
                    uploadedAt = file.UploadedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the file");
            }
        }


    }
}
