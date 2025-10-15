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
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }

        public ApiQuestion (string type, string difficulty, string category, string question, string correct_answer, string[] incorrect_answers)
        {
            Type = type;
            Difficulty = difficulty;
            Category = category;
            Question = question;
            CorrectAnswer = correct_answer;
            IncorrectAnswers = incorrect_answers;
      
        }

    }
}
