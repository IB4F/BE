namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizSubmissionDTO
    {
        public Guid QuizId { get; set; }
        public string? AnswerId { get; set; }
        public List<string>? AnswerIds { get; set; }
        public DateTime? StartedAt { get; set; }

        // DnD answers — only one will be set per request, matching the quiz type
        public List<string>? OrderedLetters { get; set; }    // DragSpell
        public List<string>? OrderedTileIds { get; set; }    // DragOrder
        public List<DragMatchSubmissionDTO>? Matches { get; set; } // DragMatch
    }
}
