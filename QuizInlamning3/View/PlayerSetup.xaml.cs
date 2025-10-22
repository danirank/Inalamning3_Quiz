using QuizInlamning3.Models;
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
        private HashSet<Category> _selectedCategories;
        private static readonly Regex _numericRegex = new Regex("^[0-9]+$");
        public PlayerSetup(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();

            //TODO: Lås 10 frågor som är samma tills användaren ber om nya. Eller spelet startas om 
            // - Sätt _quiz.Questions till 10 RandomFrågor, ska kunna vara en kategori
           
            //TODO: Välja kategor eller alla 
            _quiz = quiz;
            _navigate = navigate;
            _activePlayers = new ObservableCollection<Player>();
            _selectedCategories = new HashSet<Category>();
            _currentQuestions = new List<Question>();
            LoadCategories();
            
            
        }

        private void LoadCategories()
        {
            foreach (var category in Enum.GetValues(typeof(Category)))
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
        private void Category_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox cb && cb.Tag is Category cat)
            {
                if (cb.IsChecked == true) _selectedCategories.Add(cat);
                else _selectedCategories.Remove(cat);
            }
        }
        private void ActivePLayers()
        {

            activePlayersData.ItemsSource = _activePlayers;
            
        }
       
        private int GetNumberOfQuestions()
        {
            int numberOfQuestions = int.Parse(QuestionCountTxtBox.Text);

            return numberOfQuestions;
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

        private void addPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewPlayer();
            ActivePLayers();
        }

        private void QuestionQountTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled =!_numericRegex.IsMatch(e.Text);
        }

        private void SelectAllCategories_Checked(object sender, RoutedEventArgs e)
        {
            bool isChecked = SelectAllCategories.IsChecked == true;

            foreach (CheckBox cb in CategoryPanel.Children)
            {
                cb.IsChecked = isChecked;

            }
        }
    }       
}
