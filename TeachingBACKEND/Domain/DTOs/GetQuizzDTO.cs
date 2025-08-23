namespace TeachingBACKEND.Domain.DTOs
{
    public class GetQuizzDTO
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public List<OptionDTO> Options { get; set; }
        public bool IsAnswered { get; set; }
        public string? QuestionAudioId { get; set; }
        public string? ExplanationAudioId { get; set; }
        public string? QuestionAudioUrl { get; set; }
        public string? ExplanationAudioUrl { get; set; }
        public string QuizType { get; set; }
    }
}
