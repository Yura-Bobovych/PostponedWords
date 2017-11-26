using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PostponedWords.Models.Db;
using PostponedWords.Models;

namespace PostponedWords.Middleware
{
	public interface IDbWordsMiddleware
	{
		bool Exist(string word);

		/// <summary>
		/// Add each word + meaning is new word
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		Task<List<Word>> AddWord(string word);
		/// <summary>
		/// Get the word data from db if exist otherwise get it from api and add to db 
		/// </summary>
		/// <typeparam name="List"></typeparam>
		/// <returns></returns>
		Task<List<Word>> GetWord(string wordText);

		void AddDictionary(String dictionaryName, int UserId);
		void AddWordToDictionary(int dictionatyId, int wordId);
		List<DictionaryList> GetUserDictionaries(int UserId);






	}
}
