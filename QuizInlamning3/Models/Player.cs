using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Models
{
    public class Player
    {
        public string PlayerName { get; private set; }

        public int NumberOfCorrectAnswers { get; set; }

        public int HighScore { get; set; }

        public Player(string playerName, int highScore)
        {
            PlayerName = playerName;
            HighScore = highScore;
            NumberOfCorrectAnswers = 0;
        }

        public string Info() => $"Name: {PlayerName}\nScore: {NumberOfCorrectAnswers}";
    }
}
