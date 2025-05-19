namespace TeachingBACKEND.Domain.DTOs
{
        public class CreateLearnHubDTO
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ClassType { get; set; }
            public string Subject { get; set; }
            public bool IsFree { get; set; }
            public List<CreateLinkDTO> Links { get; set; }  // Links data for LearnHub
        }

        public class CreateLinkDTO
        {
            public string Title { get; set; }
            public double Progress { get; set; }
            public List<CreateQuizDTO> Quizzes { get; set; }  // Quizzes data for Link
        }

        public class CreateQuizDTO
        {
            public string Question { get; set; }
            public string Explanation { get; set; }
            public int Points { get; set; }
            public string Options { get; set; }
       }  
}
