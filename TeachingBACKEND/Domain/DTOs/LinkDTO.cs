namespace TeachingBACKEND.Domain.DTOs
{
    public class LinkDTO
    {
        public Guid Id { get; set; }  
        public string Title { get; set; }
        public double Progress { get; set; }
        
        public int QuizzesCount { get; set; }
    }
}
