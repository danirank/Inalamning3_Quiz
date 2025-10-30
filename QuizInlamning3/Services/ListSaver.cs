using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace QuizInlamning3.Services
{
    public class ListSaver<T> : ISaver<T> where T : class
    {
        public async Task<List<T>> SaveAsync(List<T> classList, string fileName)
        {
            string filePath = FileHelper.GetAppLocalFolderPath(fileName);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            string json = JsonSerializer.Serialize(classList, options);

            
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            
            using (var writer = new StreamWriter(filePath, false))
            {
                await writer.WriteAsync(json);
            }

            return classList;
        }
    }
}

