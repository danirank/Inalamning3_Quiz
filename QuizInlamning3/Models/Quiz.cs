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

        public Quiz(List<Question> questions, List<Player> players)
        {

            Questions = questions;
            Players = players;
        }



        //Kollar om svaret är rätt
        public bool CheckAnswer(int answer, int correctAnswer) => answer == correctAnswer;

        public int CorrectAnswer(List<Question> questions, int questionIndex) => questions[questionIndex].CorrectAnswerIndex;

        //Lägger till spelare i spelarlistan 
        public void AddPlayerToList(Player player) => Players.Add(player);

        public string ShowQuestionText(List<Question> questions, int questionIndex) => questions[questionIndex].QuestionText;

        public bool IsImageQuestion (List<Question> question,int questionIndex)
        {
            return question[questionIndex].QType == QType.Image;
        }  
        public string[] GetAnswers(List<Question> questions, int questionIndex) => questions[questionIndex].Answers;

        //Ranka på highScore för leaderboard
        public List<Player> SortPlayersByHighScore(List<Player> players)
        {
            players = players.OrderByDescending(x => x.PercentageScore).ToList();
            int rank = 1;
            int SameScore = 1; 

            players[0].Rank = rank;

            for (int i = 1; i < players.Count; i++)
            {

                var player = players[i];

                if (player.PercentageScore == players[i - 1].PercentageScore)
                {
                    player.Rank = rank;
                    SameScore++;
                    continue;
                    
                } else
                {
                    rank += SameScore;
                    player.Rank = rank;
                    
                    SameScore = 1;
                }

            }
                
            return players;
        }

        public List<string> AllCategories(List<Question> questions)
        {
            var categories = questions.Select(x => x.Category).Distinct().ToList();

            return categories; 
        }

        public List<Question> QuestionsByCategory(string categoryInput)
        {
            var category  = Questions.Where(q => q.Category == categoryInput).ToList();
            return category;
        }
        
        //Används inte
        public int GetRandomQuestionIndex(List<Question> questions, List<int> usedIndexes)
        {
            
            Random r = new Random();
            int maxValue = questions.Count;
            int randomIndex = r.Next(maxValue);
            

            while (usedIndexes.Contains(randomIndex))
            {
                randomIndex = r.Next(maxValue);
            }

            return randomIndex;
        }

        //public int IndexOfCurrentQuestion
        
    }
}
