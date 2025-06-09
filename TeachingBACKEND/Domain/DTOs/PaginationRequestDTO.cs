namespace TeachingBACKEND.Domain.DTOs
{
    public class PaginationRequestDTO
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search {  get; set; }
    }
}
