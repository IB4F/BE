namespace TeachingBACKEND.Domain.DTOs
{
    public class PaginatedResponseDTO<T>
    {
        public List<T> Data { get; set; }
        public PaginationDTO Pagination { get; set; }
    }
}

