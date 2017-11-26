using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostponedWords.Models.Db;
using PostponedWords.Models;

namespace PostponedWords.Middleware
{
    public interface IWordsApiMiddleware
    {		
		 Task<List<Word>> GetWord(string wordText);

	}
}
