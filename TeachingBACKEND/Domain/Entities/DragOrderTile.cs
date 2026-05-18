namespace TeachingBACKEND.Domain.Entities
{
    public class DragOrderTile
    {
        public Guid Id { get; set; }
        public Guid DragOrderPayloadId { get; set; }
        public DragOrderPayload Payload { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
    }
}
