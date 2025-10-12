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
using QuizInlamning3.Models;
using QuizInlamning3.Services;
using QuizInlamning3.View;

namespace QuizInlamning3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Quiz _quiz;
        public MainWindow()
        {
            InitializeComponent();
            var playerLoader = new ListLoader<Player>();
            var questionLoader = new ListLoader<Question>();

            List<Question> questions = questionLoader.Load("Data/Questions.txt");
            List<Player> players = playerLoader.Load("Data/Players.txt"); 

            _quiz = new Quiz(questions);
            _quiz.Players = players;



            MainContent.Content = new MenuView(_quiz, Navigate);
        }

        public void Navigate(UserControl newView)
        {
            MainContent.Content = newView; 
        }
    }
}
