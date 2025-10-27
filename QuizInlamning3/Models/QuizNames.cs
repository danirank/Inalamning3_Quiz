using QuizInlamning3.Services;
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
        public static ObservableCollection<QuizNamesItems> Names { get; } = new ObservableCollection<QuizNamesItems>();
        
        public static async Task LoadAsync()
        {
            var loader = new ListLoader<QuizNamesItems>();  
            var list = await loader.LoadAsync("Data/QuizNames.txt");

            Names.Clear();
            foreach (var item in list)
                Names.Add(new QuizNamesItems { Name = item.Name });
        }

    }

    public class QuizNamesItems
    {
        public string Name { get; set; }
    }
}
