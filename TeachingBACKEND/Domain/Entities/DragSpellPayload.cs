namespace TeachingBACKEND.Domain.Entities
{
    public class DragSpellPayload
    {
        public Guid Id { get; set; }
        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }
        public string Word { get; set; }
        public string Letters { get; set; } // comma-separated letter bank
        public string Hint { get; set; }
        public Guid? ImageFileId { get; set; }
        public UploadedFile ImageFile { get; set; }
    }
}
