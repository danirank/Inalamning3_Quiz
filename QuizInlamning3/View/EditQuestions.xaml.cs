using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace QuizInlamning3.View
{
    /// <summary>
    /// Interaction logic for EditQuestions.xaml
    /// </summary>
    ///
    public partial class EditQuestions : UserControl
    {
        private Quiz _quiz;
        private ObservableCollection<Question> _questions;
        private Action<UserControl> _navigate;
        private bool _isCreatingNewQuiz = false;
        private List<Question> _newQuizQuestions;
        public EditQuestions(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();
            _quiz = quiz;
            _navigate = navigate;
            _questions = new ObservableCollection<Question>(_quiz.Questions);
            
            ShowAllQuestions();
            if (_questions.Any())
            {

                ListAllQuestionsText.SelectedIndex = 0;
            } else
            {
                MessageBox.Show("No questions availible");
            }
        }

        
        private void ShowAllQuestions()
        {
           
            ListAllQuestionsText.ItemsSource = _questions;
            ListAllQuestionsText.DisplayMemberPath = "QuestionText";
            
        }
        private void ShowAnswers()
        {
            var q = ListAllQuestionsText.SelectedItem as Question;
            if (q == null) return;


            TxtQuestionText.Text = q.QuestionText;
            if (q.QType == QType.Image)
            {
                imageStringTxtbox.Visibility = Visibility.Visible;
                imageStringTxtbox.Text = q.ImagePath;
            }
            else
            {
                imageStringTxtbox.Visibility = Visibility.Collapsed;
                imageStringTxtbox.Text = string.Empty;
            }

            for (int i = AnswerPanel.Children.Count - 1; i >= 0; i--)
            {
                var child = AnswerPanel.Children[i];
                if (!ReferenceEquals(child, TxtQuestionText) &&
                    !ReferenceEquals(child, imageStringTxtbox))
                {
                    AnswerPanel.Children.RemoveAt(i);
                }
            }



            AnswerPanel.Children.Clear();

            for (int i = 0; i < q.Answers.Length; i++)
            {
                
                var rowPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(5)
                };

                
                var checkBox = new CheckBox
                {
                    Margin = new Thickness(5, 0, 10, 0),
                    IsChecked = (i == q.CorrectAnswerIndex),
                    Tag = i
                };

                
                var tb = new TextBox
                {
                    Text = q.Answers[i],
                    Width = 250,
                    Margin = new Thickness(5, 0, 0, 0),
                    Tag = i
                };

                
                rowPanel.Children.Add(checkBox);
                rowPanel.Children.Add(tb);

                
                AnswerPanel.Children.Add(rowPanel);
            
                if (checkBox.IsChecked == true)
                {
                    tb.Background = new SolidColorBrush(Colors.LightGreen);
                }

                checkBox.Checked += (s, e) => tb.Background = new SolidColorBrush(Colors.LightGreen);
                checkBox.Unchecked += (s, e) => tb.Background = new SolidColorBrush(Colors.White);
            }

        }

        private async Task SaveAsync()
        {
            ListSaver<Question> saveQuestions = new ListSaver<Question>();

            //Gör filvägen dynamisk
            await saveQuestions.SaveAsync(_quiz.Questions, "Data/ImagesQuestions.txt");
        }

        private void ListAllQuestionsText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ShowAnswers();
        }

        public void SaveQuestionToList()
        {
            int questionIndex = ListAllQuestionsText.SelectedIndex;
            Question question;

            if (_isCreatingNewQuiz)
            {
                question = _questions[questionIndex];
                  
            } 
            else
            {
                 question = _quiz.Questions[questionIndex];
            }

                if (question.QType == QType.Image)
                {
                    question.ImagePath = imageStringTxtbox.Text;
                }

                   question.QuestionText = TxtQuestionText.Text;

                   foreach (StackPanel child in AnswerPanel.Children)
                   {
                        var checkBox = (CheckBox)child.Children[0];
                        var textBox = (TextBox)child.Children[1];

                        if (checkBox.IsChecked == true)
                        {
                            question.CorrectAnswerIndex = (int)checkBox.Tag;
                        }

                        question.Answers[(int)textBox.Tag] = textBox.Text;
                   }

           

        }
        private void saveChangesBtn_Click(object sender, RoutedEventArgs e)
        {

            SaveQuestionToList();
            MessageBox.Show("Frågan är uppdaterad");

           
        }


        private async void backToMenubtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isCreatingNewQuiz)
            {
                _quiz.Questions = _questions.ToList(); 
            }
            // Spara till JSON 
            try
            {
                await SaveAsync();
                _navigate(new MenuView(_quiz, _navigate));

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void ClearAnswers()
        {
            foreach (var child in AnswerPanel.Children)
            {
                if (child is TextBox)
                {
                    TextBox textBox = (TextBox)child;
                    textBox.Text = "";
                }
            }
        }

        private void newQuizBtn_Click(object sender, RoutedEventArgs e)
        {
            _newQuizQuestions = new List<Question>();
            _isCreatingNewQuiz = true;
            Header.Text = "New quiz"; 
            
            _questions.Clear();
            ClearAnswers();
            Question placeHolderQuestion = new Question()
            {
                QuestionText  = "Frågetext", 
                Answers = new string[] {" "," "," "," "},


            };
            
            _questions.Add(placeHolderQuestion);
            ListAllQuestionsText.SelectedIndex = 0;




        }
    }
}
