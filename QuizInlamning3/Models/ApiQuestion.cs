using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Models
{
    public class ApiQuestion
    {
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
        public string Correct_Answer { get; set; }
        public string[] Incorrect_Answers { get; set; }

        

    }
}
