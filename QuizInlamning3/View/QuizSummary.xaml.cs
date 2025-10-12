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
    /// Interaction logic for QuizSummary.xaml
    /// </summary>
    /// 
    public partial class QuizSummary : UserControl
    {
        private Quiz _quiz;
        public QuizSummary(Quiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;

        }
    }
}
