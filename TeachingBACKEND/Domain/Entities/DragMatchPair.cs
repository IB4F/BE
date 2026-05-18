namespace TeachingBACKEND.Domain.Entities
{
    public class DragMatchPair
    {
        public Guid Id { get; set; }
        public Guid DragMatchPayloadId { get; set; }
        public DragMatchPayload Payload { get; set; }
        public string Word { get; set; }
        public Guid? ImageFileId { get; set; }
        public UploadedFile ImageFile { get; set; }
    }
}
