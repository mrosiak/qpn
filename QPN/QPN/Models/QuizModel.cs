using System.Collections.Generic;

namespace QPN.Models
{
    public class QuizModel
    {
        public List<QuestionModel> Questions { get; set; }
        public string SearchQuery { get; set; }
        public QuizModel()
        {
            Questions = new List<QuestionModel>();
        }
    }
}