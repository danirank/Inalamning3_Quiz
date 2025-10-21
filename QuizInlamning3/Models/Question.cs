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
       
        GeneralKnowledge,
        FoodAndDrink,
        Sports,
        Animals,
        Geography,
        ScienceAndNature
    }

   public enum QType
    {
        Text, Image 
    }

    public class Question
    {
        public Category Category { get; set; }

        public QType QType { get; set; }
        public string QuestionText { get; set; }

        public string ImagePath { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public Question() { }

        //Konstruktor för textfrågor
        public Question(Category category, QType type, string questionText, string[] answers, int correctAnswerIndex)
        {
            Category = category;
            QType = type;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

        //Konstruktor för bildfrågor
        public Question(Category category, QType type, string questionText, string imagePath, string[] answers, int correctAnswerIndex)
        {
            Category = category;
            QType = type;
            QuestionText = questionText;
            ImagePath = imagePath;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

    }


}
