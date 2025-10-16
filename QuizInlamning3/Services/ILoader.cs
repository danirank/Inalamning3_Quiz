using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Services
{
    public interface ILoader<T>
    {
        Task<List<T>> LoadAsync(string path);
    }
}
