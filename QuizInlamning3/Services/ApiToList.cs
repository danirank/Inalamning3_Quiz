using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizInlamning3.Services
{
    public class TriviaApiLoader : ILoader<ApiQuestion>
    {
        public List<ApiQuestion> Load(string path)
        {
            //TODO: Implement Http client 
            throw new NotImplementedException();
        }
    }

}


}
