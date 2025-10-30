using QuizInlamning3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizInlamning3.Models
{
    public static class QuizNames
    {
        public static ObservableCollection<QuizNamesItems> Names { get; } = new ObservableCollection<QuizNamesItems>();
        
        public static async Task LoadAsync()
        {
            try
            {

            var loader = new ListLoader<QuizNamesItems>();  
            var list = await loader.LoadAsync("QuizNames.txt");

            Names.Clear();
            foreach (var item in list)
                Names.Add(new QuizNamesItems { Name = item.Name });
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }

    public class QuizNamesItems
    {
        public string Name { get; set; }
    }
}
