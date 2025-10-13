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
    /// Interaction logic for PlayerSetup.xaml
    /// </summary>
    public partial class PlayerSetup : UserControl
    {
        private Player _player;
        private Quiz _quiz;
        private Action<UserControl> _navigate;
        public PlayerSetup(Quiz quiz, Action<UserControl> navigate)
        {
            InitializeComponent();

            _quiz = quiz;
            _navigate = navigate;
            
        }

        
        private void playerName_Click(object sender, RoutedEventArgs e)
        {
            string name = playerName.Text;
            if(String.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name");

            } else
            {

            _player = new Player(playerName.Text,0);

            

            _navigate(new PlayQuiz(_quiz, _player,_navigate));
            
            }
        }
    }
}
