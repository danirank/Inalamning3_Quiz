using QuizInlamning3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace QuizInlamning3.Services
{
    public class TriviaApiLoader 
    {



        public static async Task GetQuestions()
        {
            try
            {

                HttpClient client = new HttpClient();
                string apiUrl = "https://opentdb.com/api.php?amount=10";

                var response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                };

                var api = JsonSerializer.Deserialize<ApiQuestion>(json, options);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



    }

}



