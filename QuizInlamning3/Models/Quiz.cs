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

        public int CorrectAnswer(int questionIndex) => Questions[questionIndex].CorrectAnswerIndex;

        //Lägger till spelare i spelarlistan 
        public void AddPlayerToList(Player player) => Players.Add(player);

        public string ShowQuestionText(int questionIndex) => Questions[questionIndex].QuestionText;

        public string[] GetAnswers(int questionIndex) => Questions[questionIndex].Answers;

        //Ranka på highScore för leaderboard
        public List<Player> SortPlayersByHighScore()
        {
            var players = Players.OrderByDescending(x => x.HighScore).ToList();
            int rank = 1;
            int SameScore = 1; 

            players[0].Rank = rank;

            for (int i = 1; i < players.Count; i++)
            {

                var player = players[i];

                if (player.HighScore == players[i - 1].HighScore)
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

        

    
        

        
    }
}
