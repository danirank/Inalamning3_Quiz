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
namespace QuizInlamning3.View
{
    /// <summary>
    /// Interaction logic for ShowLeaderBoard.xaml
    /// </summary>
    public partial class ShowLeaderBoard : UserControl
    {
        private List<Player> _leaderboard;
        private Action<UserControl> _navigate;
        private Quiz _quiz;
        public ShowLeaderBoard(Quiz quiz, List<Player> players, Action<UserControl> navigate)
        {
            InitializeComponent();
            _quiz = quiz;
            _leaderboard = _quiz.SortPlayersByHighScore(players);
            _navigate = navigate;
            DisplayLeaderBoard();
        }

        public void DisplayLeaderBoard()
        {
            


           leaderBoardData.ItemsSource = _leaderboard;

            
        }

        private void backToMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            _navigate(new MenuView(_quiz, _navigate));
        }
    }
}
