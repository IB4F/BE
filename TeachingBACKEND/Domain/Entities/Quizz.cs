﻿namespace TeachingBACKEND.Domain.Entities
{
    public class Quizz
    {
        public Guid Id { get; set; }
        public Guid LinkId { get; set; }
        public Link Link { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public bool IsAnswered { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
