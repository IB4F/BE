namespace TeachingBACKEND.Domain.DTOs
{
    public class PaginatedResultDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
