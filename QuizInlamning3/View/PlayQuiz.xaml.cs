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
        public PlayQuiz(Quiz quiz, List<Question> currentQuestions, Action<UserControl> navigate)
        {
            InitializeComponent();
           
            _quiz = quiz;
            _navigate = navigate;
            _player = _quiz.Players[_playerIndex]; 
            _quiz.Questions = currentQuestions;
            
            ResetColorOnAnswerButtonsAndHover();
            ShowQuestion();


        }

      
        private void ShowQuestion()
        {
            //_questionIndex = _quiz.GetRandomQuestionIndex(_quiz.Questions,_usedIndexes);
            //_usedIndexes.Add(_questionIndex); //Ändra eftersom frågorna redan ska vara utvalda
            ShowPlayerScore();
            ShowNumberOfQuestions();
            QuestionText();
            ShowAnswers(_quiz.GetAnswers(_questionIndex));


        }
        private void QuestionText()
        {
            questionTxtBox.Text = _quiz.ShowQuestionText(_questionIndex);

            if (_quiz.IsImageQuestion(_questionIndex))
            {
                var imagePath = _quiz.Questions[_questionIndex].ImagePath;
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
            int totalQuestions = _quiz.Questions.Count;

            infoQuestions.Text = $"Question {currenQuestion}/{totalQuestions}";

        }
        private void ShowPlayerScore()
        {
            infoPlayer.Text = _player.Info();
        }
        private void ChangeColorOnBtn(int answer, Button btn)
        {
            
            int correctAnswer = _quiz.CorrectAnswer(_questionIndex);
           
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
        private void FinishQuiz()
        {

            
            //TODO: Spara över
            _player.HighScore = _player.NumberOfCorrectAnswers;
            

             _playerIndex++;
            CheckPlayerIndex(_playerIndex);

            if (_runQuiz)
            {
                _player = _quiz.Players[_playerIndex];
                _questionIndex = 0;
                ShowQuestion();
            } else
            {
                _navigate(new ShowLeaderBoard(_quiz, _navigate));
            }
            //_quiz.AddPlayerToList(_player);
            //_navigate(new MenuView(_quiz,_navigate));
            

        }
        private void NextQuestionBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!_questionMarked)
            {
                MessageBox.Show("At least take a guess","No answer!");
                return;
            }
            _questionMarked = false;
            string finishQuiz = "Finish quiz";

            ResetColorOnAnswerButtonsAndHover();

            
            if (_questionIndex == _quiz.Questions.Count-2)
            {
                NextQuestionBtn.Content = finishQuiz;
            }

            if (_questionIndex == _quiz.Questions.Count-1)
            {
                FinishQuiz();
                return;
                
            }
            _questionIndex++;
            ShowQuestion();
            //TODO: Logik för när nästa spelare tar över 

           


        }

        //TODO: Bygg ihop med RunQuiz? 
        private void answerBtn_Click(object sender, RoutedEventArgs e)
        {
            

            if (!_questionMarked)
            {
                
                var btn = (Button)sender;
                int answer = int.Parse(btn.Tag.ToString());
                int correctAnswer = _quiz.CorrectAnswer(_questionIndex);
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
