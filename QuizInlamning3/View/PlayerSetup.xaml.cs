using QuizInlamning3.Models;
using QuizInlamning3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace QuizInlamning3.View
{
    /// <summary>
    /// Interaction logic for PlayerSetup.xaml
    /// </summary>
    public partial class PlayerSetup : UserControl
    {
        private Player _player;
        private Quiz _quiz;
        private List<Question> _currentQuestions;
        private ObservableCollection<Player> _activePlayers;
        private Action<UserControl> _navigate;
        private HashSet<string> _selectedCategories;
        private static readonly Regex _numericRegex = new Regex("^[0-9]+$");
        public PlayerSetup(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();

            
            _quiz = quiz;
            _navigate = navigate;
            _activePlayers = new ObservableCollection<Player>();
            _selectedCategories = new HashSet<string>();
            _currentQuestions = new List<Question>();
            ChangeQuiz();
            LoadCategories();
            
            
        }
        //Ladda quiz

        private async void changeQuiz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (changeQuiz.SelectedIndex == 0)
            {
                return;
            }
            _currentQuestions.Clear();
            _quiz.Questions.Clear();
            string selectedQuiz = changeQuiz.SelectedItem.ToString();

            headerSetup.Text = selectedQuiz;

            string filePath = $"Data/{selectedQuiz}.txt";

            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show($"Filen hittas inte: {filePath}");
                return;
            }


            ListLoader<Question> loadquestions = new ListLoader<Question>();

            var questions = await loadquestions.LoadAsync(filePath);
            
            _quiz.Name = selectedQuiz;
            _currentQuestions = questions;
            _quiz.Questions = questions;
            LoadCategories();
        }
        private void ChangeQuiz()
        {
            
            changeQuiz.ItemsSource = QuizNames.Names;
            changeQuiz.SelectedItem = QuizNames.Names.First();
        }
        private void LoadCategories()
        {


            HeaderText();
            _selectedCategories.Clear();
            CategoryPanel.Children.Clear();
            SelectAllCategories.IsChecked = false;
           
            foreach (var category in _quiz.AllCategories(_quiz.Questions))
            {
                var checkBox = new CheckBox
                {
                    Content = category.ToString(),
                    Tag = category,
                    Margin = new Thickness(5)
                    
                };

                checkBox.Checked += Category_CheckedChanged;
                checkBox.Unchecked += Category_CheckedChanged;
                CategoryPanel.Children.Add(checkBox);
            }
            

        }

        //Hjlpmetoder för quiz
        private void SelectRandomQuestions()
        {
            int numberOfquestions = GetNumberOfQuestions();
            Random r = new Random();
            var allowedCategories = _selectedCategories; 

            var questions = _quiz.Questions.Where(x => allowedCategories.Contains(x.Category)).ToList();

            int maxQuizQuestions = questions.Count;

            if (numberOfquestions > maxQuizQuestions)
            {
                MessageBox.Show($"Only {questions.Count} number of questions avalieble in selcted categories");
                return;
            }
            //Blanda lista utifrån random tal 
            questions = questions.OrderBy(x => r.Next()).Take(numberOfquestions).ToList();
            
            _currentQuestions = questions;
            

        }
        private int GetNumberOfQuestions()
        {
            int numberOfQuestions = int.Parse(QuestionCountTxtBox.Text);

            return numberOfQuestions;
        }
        private void StartQuizBtn_Click(object sender, RoutedEventArgs e)
        {

            SelectRandomQuestions();


            if (_currentQuestions.Count == 0)
            {
                MessageBox.Show("No Questions");
                return;
            }

            if(_activePlayers.Count == 0)
            {
                MessageBox.Show("No active PLayers");
                    return;
            }

          List<Player> players = _activePlayers.ToList();

                _navigate(new PlayQuiz(_quiz,players,_currentQuestions,_navigate));
            
            
        }

        private void HeaderText()
        {
            headerSetup.Text = "Quiz - " + _quiz.Name;
        }

        
        //Spelare
        private void addPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewPlayer();
            ActivePLayers();
        }
        private void ActivePLayers()
        {

            activePlayersData.ItemsSource = _activePlayers;
            
        }
       
        private void CreateNewPlayer()
        {
            
                string name = playerNameTxt.Text; 

                if (String.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Please enter a name");
                    return;

                } else
                {
                    _player = new Player(name, 0);
                    _activePlayers.Add(_player);
                }

            playerNameTxt.Text = string.Empty; 
            playerNameTxt.Focus();
            //MessageBox.Show($"{_player.PlayerName}");


            _quiz.Players.Add(_player);

            

        }

        private void QuestionCountTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled =!_numericRegex.IsMatch(e.Text);
        }



        //Val av kategorier
        private void SelectAllCategories_Checked(object sender, RoutedEventArgs e)
        {
            bool isChecked = SelectAllCategories.IsChecked == true;

            foreach (CheckBox cb in CategoryPanel.Children)
            {
                cb.IsChecked = isChecked;

            }
        }
        private void Category_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is string cat)
            {
                if (cb.IsChecked == true) _selectedCategories.Add(cat);
                else _selectedCategories.Remove(cat);
            }
        }

    }       
}
