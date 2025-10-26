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
        private bool _isNameConfirmed = true;
        
        public EditQuestions(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();
            _quiz = quiz;
            _navigate = navigate;
            _questions = new ObservableCollection<Question>(_quiz.Questions);
            ShowQuizezBtn();
            ShowAllQuestions();
            if (_questions.Any())
            {

                ListAllQuestionsText.SelectedIndex = 0;
            } else
            {
                MessageBox.Show("No questions availible");
            }
        }

        //Visa innehåll

        private void ListAllQuestionsText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            ShowAnswers();
        }
        private void ShowQuizezBtn()
        {
            List<QuizNamesItems> quizNames = QuizNames.Names.Skip(1).ToList();
            foreach (var quizName in quizNames)
            {
                
                Button btn = new Button()
                {
                    Content = quizName.Name,
                    
                };

                btn.Click += LoadAsync_Click;
                LoadQuizes.Children.Add(btn);
                
            }
        }
        private void ShowAllQuestions()
        {
            Header.Text = _quiz.Name;  
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
            categoryTxtBox.Text = q.Category;
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

        //Spara/ladda
        private async void LoadAsync_Click (object sender, RoutedEventArgs e)
        {
           Button btn = (Button)sender;

          

            string quizName = btn.Content.ToString();
            string filepath = $"Data/{quizName}.txt";

          


            var loaded = await LoadQuestionToEdit(filepath);
            if (loaded == null || loaded.Count == 0)
            {
                MessageBox.Show($"Kunde inte ladda frågor från {filepath}.");
                return;
            }

            
            _quiz.Name = quizName;
            Header.Text = _quiz.Name;

            
            _questions.Clear();
            foreach (var q in loaded)
                _questions.Add(q);

            if (_questions.Count > 0)
                ListAllQuestionsText.SelectedIndex = 0;

            _quiz.Name = quizName;
        }
        private async Task<List<Question>> LoadQuestionToEdit(string filePath)
        {
                
            try
            {

                ListLoader<Question> questionLoader = new ListLoader<Question>();

                return await questionLoader.LoadAsync(filePath);

            } catch (Exception ex) 
            { 
                MessageBox.Show("Load failed: " + ex.Message);
                
            }


            return new List<Question>();
        }
        private async Task SaveAsync()
        {
            List<QuizNamesItems> names = new List<QuizNamesItems>(QuizNames.Names);

            ListSaver<QuizNamesItems> saveQuizNames = new ListSaver<QuizNamesItems>();
            await saveQuizNames.SaveAsync(names, "Data/QuizNames.txt");
            
            ListSaver<Question> saveQuestions = new ListSaver<Question>();
            await saveQuestions.SaveAsync(_quiz.Questions, $"Data/{_quiz.Name}.txt");
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
                   question.Category = categoryTxtBox.Text;

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

        private async void SaveAndbackToMenubtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isCreatingNewQuiz)
            {
                _quiz.Questions = _questions.ToList();
                
            }

            if (!_isNameConfirmed)
            {
                MessageBox.Show("Confirm Quizname before saving");
                return;
            }
            
            try
            {
                await SaveAsync();
                _navigate(new MenuView(_quiz, _navigate));

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Rensa
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


        //Nytt quiz
        private void NameNewQuiz()
        {
            if (_isCreatingNewQuiz)
            {
                newQuizName.Visibility = Visibility.Visible;
                confirmNewQuizName.Visibility = Visibility.Visible;
               
                newQuizName.Focus();
            }
        }

        private void newQuizBtn_Click(object sender, RoutedEventArgs e)
        {

            _newQuizQuestions = new List<Question>();
            _isCreatingNewQuiz = true;
            _isNameConfirmed = false;
            
            Header.Text = "New quiz";
            NameNewQuiz();
            
            
            _questions.Clear();
            ClearAnswers();
            Question placeHolderQuestion = new Question()
            {
                QuestionText  = "Frågetext",
                Category = "Category",
                Answers = new string[] {" "," "," "," "},


            };
            
           _questions.Add(placeHolderQuestion);
            ListAllQuestionsText.SelectedIndex = 0;




        }

        private void newQuestion_Click(object sender, RoutedEventArgs e)
        {
            Question placeHolderQuestion = new Question()
            {
                QuestionText = "Frågetext",
                Category = "Category",
                Answers = new string[] { " ", " ", " ", " " },


            };

            _questions.Add(placeHolderQuestion);

            ListAllQuestionsText.SelectedIndex = _questions.Count - 1;
        }

        private void confirmNewQuizName_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            _quiz.Name = newQuizName.Text;
            Header.Text = _quiz.Name;
            newQuizName.Visibility = Visibility.Collapsed;
            btn.Visibility = Visibility.Collapsed;
            QuizNames.Names.Add(new QuizNamesItems { Name = _quiz.Name});
            _isNameConfirmed = true;
        }
       

        


    }
}
