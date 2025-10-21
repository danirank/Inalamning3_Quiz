using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public EditQuestions(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            
            ShowAllQuestions();
            if (_questions.Any())
                ListAllQuestionsText.SelectedIndex = 1;
        }

        //TODO: Snygga till edit, lägga til nya frågor. Spara
        private void ShowAllQuestions()
        {
            _questions = new ObservableCollection<Question>(_quiz.Questions);
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

            for (int i = answersTxtbox.Children.Count - 1; i >= 0; i--)
            {
                var child = answersTxtbox.Children[i];
                if (!ReferenceEquals(child, TxtQuestionText) &&
                    !ReferenceEquals(child, imageStringTxtbox))
                {
                    answersTxtbox.Children.RemoveAt(i);
                }
            }



            for (int i = 0; i < q.Answers.Length; i++)
            {
                var tb = new TextBox
                {
                    Text = q.Answers[i],
                    Margin = new Thickness(10),
                
                };
               

                answersTxtbox.Children.Add(tb);
            }
        }

        private void ListAllQuestionsText_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowAnswers();
        }
    }
}
