namespace TeachingBACKEND.Domain.Entities
{
    public class UploadedFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
        public Guid UploadedBy { get; set; } // Admin user ID
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Audio,
        Image
    }
}
