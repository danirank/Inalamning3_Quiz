using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Models
{
    public class Question
    {
        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public Question(string questionText, string[]answers, int correctAnsIndex) 
        {
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnsIndex;

        }

    }
}
