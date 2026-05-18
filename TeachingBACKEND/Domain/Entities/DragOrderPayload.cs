namespace TeachingBACKEND.Domain.Entities
{
    public class DragOrderPayload
    {
        public Guid Id { get; set; }
        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }
        public string CorrectOrder { get; set; } // comma-separated tile IDs in correct sequence
        public ICollection<DragOrderTile> Tiles { get; set; } = new List<DragOrderTile>();
    }
}
