using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace QuizInlamning3.Models
{
    public class Question
    {
        public string Category { get; set; }
        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public Question(string category, string questionText, string[]answers, int correctAnswerIndex) 
        {
            Category = category;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

    }
}
