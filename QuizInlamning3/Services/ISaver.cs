using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizInlamning3.Services
{
    public interface ISaver<T>
    {
        Task<List<T>> SaveAsync(List<T> listOfClassT, string filePath); 
    }
}
