namespace TeachingBACKEND.Domain.DTOs
{
    public class StudentQuizDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public int Points { get; set; }
        public List<StudentOptionDTO> Options { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public string? QuestionImageUrl { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public string QuizzTypeName { get; set; }
        public Guid? ParentQuizId { get; set; }
        public bool MultipleAnswer { get; set; }

        // DnD payloads — only the relevant one is populated based on quiz type
        public DragSpellStudentDTO? DndSpell { get; set; }
        public DragOrderStudentDTO? DndOrder { get; set; }
        public DragMatchStudentDTO? DndMatch { get; set; }
    }

    public class StudentOptionDTO
    {
        public string Id { get; set; }
        public string OptionText { get; set; }
        public string? OptionImageId { get; set; }
        public string? OptionImageUrl { get; set; }
        // Note: IsCorrect is intentionally excluded for security
    }
}
