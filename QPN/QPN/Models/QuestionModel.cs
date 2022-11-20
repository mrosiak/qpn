using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QPN.Models
{
    public class QuestionModel
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> WrongAnswer { get; set; }
        public int SortOrder { get; set; }
        public List<string> RelatedLinks { get; set; }
        public string Difficulty { get; set; }
    }
}