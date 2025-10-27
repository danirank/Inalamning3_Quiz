using QuizInlamning3.Models;
using QuizInlamning3.View;
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

namespace QuizInlamning3.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        private Quiz _quiz;
        private List<Question> _questions;
        private Action<UserControl> _navigate;
        public MenuView(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();
            _quiz = quiz;
            _navigate = navigate;
            
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            
            _navigate(new QuizSetup(_quiz,_navigate));

        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            
            _navigate(new EditQuestions(_quiz,_navigate));
        }

        private void leaderboardBtn_Click(object sender, RoutedEventArgs e)
        {
           

            _navigate(new ShowLeaderBoard(_quiz,_quiz.Players,_navigate));
        }
    }
}
