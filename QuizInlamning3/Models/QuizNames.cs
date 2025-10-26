using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Models
{
    public static class QuizNames
    {
        public static ObservableCollection<string> Names { get; set; } = new ObservableCollection<string>()
        {
          "--Välj quiz--", "CsharpQuestions","GeneralKnowledge"
        };

        
    }
}
