namespace TeachingBACKEND.Domain.DTOs
{
    public class CreateQuizzDTO
    {
        public string Question { get; set; }
        public string? QuestionAudioId { get; set; }
        public string? QuestionImageId { get; set; }
        public string Explanation { get; set; }
        public string? ExplanationAudioId { get; set; }
        public string? ExplanationImageId { get; set; }
        public int Points { get; set; }
        public List<OptionDTO> Options { get; set; }
        public string QuizType { get; set; }
        public string? ParentQuizId { get; set; }

        // DnD payloads — only one will be set per request, matching the quiz type
        public DragSpellInputDTO? DndSpell { get; set; }
        public DragOrderInputDTO? DndOrder { get; set; }
        public DragMatchInputDTO? DndMatch { get; set; }
    }
}
