﻿namespace TeachingBACKEND.Domain.DTOs
{
    public class QuizzDTO
    {
        public Guid Id { get; set; }
        public Guid LinkId { get; set; }
        public string Question { get; set; }
        public string Explanation { get; set; }
        public int Points { get; set; }
        public string Options { get; set; }
        public bool IsAnswered { get; set; }

    }
}
