﻿namespace TeachingBACKEND.Domain.Entities
{
    public class Option
    {
        public Guid Id { get; set; }
        public Guid QuizzId { get; set; }
        public Quizz Quizz { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}