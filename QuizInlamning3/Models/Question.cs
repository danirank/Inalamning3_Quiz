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
        Nature,
        Animals,
        ScienceAndNature
    }

   public enum Type
    {
        Text, Image 
    }

    public class Question
    {
        public Category Category { get; set; }

        public Type Type { get; set; }
        public string QuestionText { get; set; }

        public string ImagePath { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public Question() { }

        //Konstruktor för textfrågor
        public Question(Category category, string questionText, string[]answers, int correctAnswerIndex) 
        {
            Category = category;
            Type = Type.Text;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

        //Konstruktor för bildfrågor
        public Question(Category category, string questionText, string imagePath, string[] answers, int correctAnswerIndex)
        {
            Category = category;
            Type = Type.Image;
            QuestionText = questionText;
            ImagePath = imagePath;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;

        }

    }


}
