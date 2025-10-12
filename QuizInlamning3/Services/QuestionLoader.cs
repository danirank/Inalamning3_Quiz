using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

using System.Threading.Tasks;

using QuizInlamning3.Models;

namespace QuizInlamning3.Services
{
    public static class QuestionLoader 
    {
        public static List<Question> Load(string filepath)
        {
            List<Question> list = new List<Question>();

            if (File.Exists(filepath))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
               

                string text = File.ReadAllText(filepath);
                list = JsonSerializer.Deserialize<List<Question>>(text,options);    
            } 
           
            return list;
        }
        
    }

   
}
