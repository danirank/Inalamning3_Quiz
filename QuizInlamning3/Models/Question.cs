using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.ComponentModel;


namespace QuizInlamning3.Models
{
    

    public enum QType
    {
        Text, Image 
    }

    public class Question : INotifyPropertyChanged
    {
        public string Category { get; set; }

        public QType QType { get; set; }

        private string _questionText;
        
        public string QuestionText 
        {
            get => _questionText;
            
            set
            {
                if (_questionText != value)
                {
                    _questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
                
        }

        public string ImagePath { get; set; }

        public string[] Answers { get; set; }

        public int CorrectAnswerIndex {  get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
