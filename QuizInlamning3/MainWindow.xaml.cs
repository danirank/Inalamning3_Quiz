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
        //TODO: Kolla över språk, blandar sve/eng
        //TODO: Spara/ladda till specifikt folder
        private Quiz _quiz;
        public MainWindow()
        {
            InitializeComponent();
          
            Loaded += MainLoaded;

        }


        private async void MainLoaded(object sender, RoutedEventArgs e)
        {
            
                _quiz = await LoadQuizAsync();

                await QuizNames.LoadAsync();
            

            MainContent.Content = new MenuView(_quiz, Navigate);

        }

            //Laddar ett standardQuiz och tidigare spelare
        
        private async Task<Quiz> LoadQuizAsync()
        {
            try
            {
            var playerLoader = new ListLoader<Player>();
             var questionLoader = new ListLoader<Question>();
            var questionsTask = questionLoader.LoadAsync("Data/CsharpQuestions.txt");
            var playersTask = playerLoader.LoadAsync("Data/Players.txt");

            var questions = await questionsTask;
            var players = await playersTask;

            
            return new Quiz("CsharpQuestions", questions , players);

            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;

        }

        public void Navigate(UserControl newView)
        {
            MainContent.Content = newView; 
        }
    }
}
