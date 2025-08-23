namespace TeachingBACKEND.Domain.DTOs
{
    public class OptionDTO
    {
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public string? OptionImageId { get; set; }
        public string? OptionImageUrl { get; set; }
    }
}
