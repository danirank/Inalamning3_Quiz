using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace QuizInlamning3.Models
{
    public enum Category
    {
        History,
        Geography,
        Science,
        Sports,
        Entertainment,
        ArtAndLiterature,
        PopCulture,
        Technology,
        GeneralKnowledge,
        FoodAndDrink,
        Nature
    }

    public class Question
    {
        public Category Category { get; set; }
        public string QuestionText { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public Question(Category category, string questionText, string[]answers, int correctAnswerIndex) 
        {
            Category = category;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

    }
}
