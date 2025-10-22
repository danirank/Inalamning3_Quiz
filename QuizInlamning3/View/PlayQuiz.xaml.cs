using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuizInlamning3.Models;
using QuizInlamning3.Services;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace QuizInlamning3.View
{
    /// <summary>
    /// Interaction logic for PlayQuiz.xaml
    /// </summary>
    public partial class PlayQuiz : UserControl
    {
        private Quiz _quiz;
        private int _questionIndex = 0;
        private int _playerIndex = 0;
        private Player _player;
        private bool _questionMarked = false;
        private bool _runQuiz = true;
        private Action<UserControl> _navigate;
        private List<Question> _questions;
        public PlayQuiz(Quiz quiz, List<Question> currentQuestions, Action<UserControl> navigate)
        {
            InitializeComponent();
           
            _quiz = quiz;
            _navigate = navigate;
            _player = _quiz.Players[_playerIndex]; 
            _questions = currentQuestions;
            
            ResetColorOnAnswerButtonsAndHover();

            if (_questions.Count > 0)
            {

             ShowQuestion();
            }

            if (_questions.Count == 1)
            {
                NextPlayerStyleBtn();
            }

            if (_questions.Count == 1 && _quiz.Players.Count==1)
            {
                FinishQuizStyleBtn();
            }
        }

      
        private void ShowQuestion()
        {
            //_questionIndex = _quiz.GetRandomQuestionIndex(_quiz.Questions,_usedIndexes);
            //_usedIndexes.Add(_questionIndex); //Ändra eftersom frågorna redan ska vara utvalda
            ShowPlayerScore();
            PercantageScore(GetPercentage());
            ShowNumberOfQuestions();
            QuestionText();
            ShowAnswers(_quiz.GetAnswers(_questions, _questionIndex));


        }
        private void QuestionText()
        {
            questionTxtBox.Text = _quiz.ShowQuestionText(_questions,_questionIndex);

            if (_quiz.IsImageQuestion(_questions, _questionIndex))
            {
                var imagePath = _questions[_questionIndex].ImagePath;
                var fullPath = System.IO.Path.GetFullPath(imagePath);
                imageQuestion.Source = new BitmapImage(new Uri (fullPath, UriKind.RelativeOrAbsolute));
                imageQuestion.Visibility = Visibility.Visible;  
            } else
            {
                imageQuestion.Visibility = Visibility.Collapsed;
            }
          
        }
        private void DisableHover()
        {
            foreach (var b in new[] { answerIdx0Btn, answerIdx1Btn, answerIdx2Btn, answerIdx3Btn })
            {
                b.IsHitTestVisible = false; 
                
            }

        }
        private void ShowAnswers(string[] answers)
        {
            answerIdx0Btn.Content = answers[0];
            answerIdx1Btn.Content = answers[1];
            answerIdx2Btn.Content = answers[2];
            answerIdx3Btn.Content = answers[3];
        }
        private void ResetColorOnAnswerButtonsAndHover()
        {
            answerIdx0Btn.Background = Brushes.LightBlue;
            answerIdx1Btn.Background = Brushes.Yellow;
            answerIdx2Btn.Background = Brushes.Orange; 
            answerIdx3Btn.Background = Brushes.LightCoral;

            foreach (var b in new[] { answerIdx0Btn, answerIdx1Btn, answerIdx2Btn, answerIdx3Btn })
            {
                b.IsHitTestVisible = true;
            }
        }
        private void ShowNumberOfQuestions()
        {
            
            int currenQuestion = _questionIndex + 1;
            int totalQuestions = _questions.Count;

            infoQuestions.Text = $"Question {currenQuestion}/{totalQuestions}";

        }

        private double GetPercentage()
        {
             double percent = Math.Round((double)_player.NumberOfCorrectAnswers / _questions.Count * 100, 1);

            return percent;
        }

        private void PercantageScore(double percent)
        {
             percent = Math.Round((double)_player.NumberOfCorrectAnswers / _questions.Count * 100, 1);


            ScorePercentage.Text =  $" Percantage: {percent}%"; 
        }
        private void ShowPlayerScore()
        {
            infoPlayer.Text = $"{_player.Info()}";
            
        }
        private void ChangeColorOnBtn(int answer, Button btn)
        {
            
            int correctAnswer = _quiz.CorrectAnswer(_questions,_questionIndex);
           
            Button[] buttons = {answerIdx0Btn,answerIdx1Btn,answerIdx2Btn,answerIdx3Btn };
            Button correctButton = null;

            switch (correctAnswer)
            {
                case 0:
                    correctButton = answerIdx0Btn;
                    break;
                case 1:
                    correctButton = answerIdx1Btn;
                    break;
                case 2:
                    correctButton = answerIdx2Btn;
                    break;
                case 3:
                    correctButton = answerIdx3Btn;
                    break;
            }

            foreach (Button button in buttons)
            {
                if (button == correctButton)
                {
                    button.Background = Brushes.ForestGreen;
                    

                }
                else
                {
                    button.Background = Brushes.Red;
                }

            }


        }

        private bool CheckPlayerIndex(int index)
        {
            
            if (index  < _quiz.Players.Count)
            {
                _runQuiz = true;
            } else
            {
                _runQuiz = false;
            }
            return _runQuiz;
        }
        private async void FinishQuiz()
        {

            
            
            _player.HighScore = _player.NumberOfCorrectAnswers;
            _player.PercentageScore = _player.GetPercentageScore(_questions);

            _playerIndex++;

            //Kollar om det finns spelare som inte kört
            CheckPlayerIndex(_playerIndex);

            if (_runQuiz)
            {
                _player = _quiz.Players[_playerIndex];
                _questionIndex = 0;
                ShowQuestion();
            } else //Om alla kört, avsluta
            {
               


               

                _navigate(new ShowLeaderBoard(_quiz, _navigate));
            }
            //_quiz.AddPlayerToList(_player);
            //_navigate(new MenuView(_quiz,_navigate));
            

        }
        private void NextPlayerStyleBtn()
        {
            

            NextQuestionBtn.Content = "Next player";
            NextQuestionBtn.Background = new SolidColorBrush(Colors.Black);
            NextQuestionBtn.Foreground = new SolidColorBrush(Colors.White);
            
            
        }

        private void NextQuestionStyleBtn()
        {
            NextQuestionBtn.Content = "Next question";
            NextQuestionBtn.ClearValue(Button.BackgroundProperty);
            NextQuestionBtn.ClearValue(Button.ForegroundProperty);
            

        }

        private void FinishQuizStyleBtn()
        {
            NextQuestionBtn.Content = "Fisnish quiz";
            NextQuestionBtn.Background = new SolidColorBrush(Colors.Violet);


        }
        private void NextQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            NextQuestionStyleBtn();

            if (!_questionMarked)
            {

                MessageBox.Show("At least take a guess","No answer!");
                return;
            }
            _questionMarked = false;
            
            
            ResetColorOnAnswerButtonsAndHover();

            if ((_questionIndex == _questions.Count - 2 && _playerIndex < _quiz.Players.Count -1) )
            {
                NextPlayerStyleBtn();

            }

            else if (_questionIndex == _questions.Count-2)
            {
                FinishQuizStyleBtn();
            }

            if (_questionIndex == _questions.Count-1)
            {
                FinishQuiz();
                return;
                
            }
            _questionIndex++;
            ShowQuestion();
            
             

           


        }

        
        private void answerBtn_Click(object sender, RoutedEventArgs e)
        {
            

            if (!_questionMarked)
            {
                
                var btn = (Button)sender;
                int answer = int.Parse(btn.Tag.ToString());
                int correctAnswer = _quiz.CorrectAnswer(_questions,_questionIndex);
                bool isCorrect = _quiz.CheckAnswer(answer, correctAnswer);

                if (isCorrect) _player.NumberOfCorrectAnswers++;

                ChangeColorOnBtn(answer, btn);
                DisableHover();
                _questionMarked = true;
            } else
            {
                MessageBox.Show("Already locked in one answer");
            }
        }

       


        

    }
       
}
