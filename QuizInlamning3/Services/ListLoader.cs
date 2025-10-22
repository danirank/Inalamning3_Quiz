using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizInlamning3.Services
{
    public class ListLoader<T> : ILoader<T> where T : class
    {
        public async Task<List<T>> LoadAsync (string filepath)
        {
            var list = new List<T>();

            if (File.Exists(filepath))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;

                JsonStringEnumConverter converter = new JsonStringEnumConverter();
                options.Converters.Add(converter);


                using (var stream = File.OpenRead(filepath))
                {

                    list = await JsonSerializer.DeserializeAsync<List<T>>(stream, options);
                }
            }

            return list;
        }
    }

    
}
