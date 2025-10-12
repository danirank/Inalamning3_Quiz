using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizInlamning3.Services
{
    public class ListLoader<T> : ILoader<T> where T : class
    {
        public List<T> Load (string filepath)
        {
            var list = new List<T>();

            if (File.Exists(filepath))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;


                string text = File.ReadAllText(filepath);

                list = JsonSerializer.Deserialize<List<T>>(text, options);
            }

            return list;
        }
    }
}
