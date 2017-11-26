using Microsoft.Extensions.Logging;
using PostponedWords.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostponedWords.Middleware
{
    public class DbWordsMiddleware:IDbWordsMiddleware
    {
		#region properties
		private readonly PostroneWords_v2Context _context;
		private readonly IWordsApiMiddleware _wordApi;
		private readonly ILogger<DbWordsMiddleware> _logger;
		#endregion

		public DbWordsMiddleware(PostroneWords_v2Context context,IWordsApiMiddleware wordApi,ILogger<DbWordsMiddleware> logger)
		{
			_wordApi = wordApi;
			_context = context;
			_logger = logger;
		}	
		
		#region public 
		public async Task<List<Word>> AddWord(string wordText)
		{
			List<Word> wordList = await _wordApi.GetWord(wordText);
			_context.Word.AddRange(wordList);
			_context.SaveChangesAsync();
		
			return wordList;
		}

		public void AddDictionary(string dictionaryName, int UserId)
		{
			DictionaryList dictionaryItem = new DictionaryList();
			dictionaryItem.Name = dictionaryName;
			dictionaryItem.UserId = UserId;
			_context.DictionaryList.Add(dictionaryItem);
			_context.SaveChanges();
			
			
		}

		public void AddWordToDictionary(int dictionatyId, int wordId)
		{
			Dictionary d = new Dictionary();
			d.DictionaryId = dictionatyId;
			d.WordId = wordId;
			d.WordAddDate = DateTime.Now;
			_context.Dictionary.Add(d);
			_context.SaveChanges();
		
		}

		public bool Exist(string word)
		{
			return _context.Word.Where(w => w.WordText == word).FirstOrDefault() != null ? true : false;
		}
		public async Task<List<Word>> GetWord(string wordText)
		{
			List<Word> wordList = _context.Word.Where(w => w.WordText == wordText).ToList();
			_logger.LogDebug("User search word count :"+ wordList.Count);
			if (!wordList.Any())
			{
				wordList = await this.AddWord(wordText);
			}
			
			return wordList;
		}

		public List<DictionaryList> GetUserDictionaries(int UserId)
		{
			return _context.DictionaryList.Where(dl => (dl.UserId==UserId)).ToList();
		}
		#endregion
	}

}
