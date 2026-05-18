namespace TeachingBACKEND.Domain.Entities
{
    public class DragMatchPayload
    {
        public Guid Id { get; set; }
        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }
        public ICollection<DragMatchPair> Pairs { get; set; } = new List<DragMatchPair>();
    }
}
