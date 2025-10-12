using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizInlamning3.Services;

namespace QuizInlamning3.Models
{
    public class Quiz
    {
        
        public List<Player> Players = new List<Player>();
        public List<Question> Questions = new List<Question>();
        
        public Quiz(List<Question> questions)
        {
            
            Questions = questions;
        }



        //Kollar om svaret är rätt
        public bool CheckAnswer(int answer, int correctAnswer) => answer == correctAnswer;

        public int CorrectAnswer(int questionIndex) => Questions[questionIndex].CorrectAnswerIndex;

        //Lägger till spelare i spelarlistan 
        public void AddPlayerToList(Player player) => Players.Add(player);

        public string ShowQuestionText(int questionIndex) => Questions[questionIndex].QuestionText;

        public string[] GetAnswers(int questionIndex) => Questions[questionIndex].Answers;
        
        //Ranka för highScore
        public List<Player> SortByHighScore()
        {
            return Players.OrderBy(x => x.HighScore).ToList();
        }

        
    }
}
