using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PostponedWords.Models.Db;
using Newtonsoft.Json;
using PostponedWords.Models.JsonSchema;
using Microsoft.Extensions.Logging;

namespace PostponedWords.Middleware
{
	public class WordsApiMiddleware:IWordsApiMiddleware
	{


		private readonly ILogger<WordsApiMiddleware> _logger;
		public WordsApiMiddleware(ILogger<WordsApiMiddleware> logger)
		{
			_logger = logger;
		}

		#region pablic
		/// <summary>
		/// Get word data from api and create word obj for each meaning
		/// </summary>
		/// <param name="wordText"></param>
		/// <returns></returns>
		public async Task<List<Word>> GetWord(string wordText)
		{
			string wordJson = await GetWordFromApi(wordText);
			_logger.LogDebug("Json form Words API: \n " + wordJson);
			ApiWord apiWord = JsonConvert.DeserializeObject<ApiWord>(wordJson);
			List<Word> wordsInQuery = new List<Word>();
			_logger.LogDebug(apiWord.ToString());
			foreach (Results res in apiWord.Results)
			{
				if (wordsInQuery.Count >= 4)
					break;
				if (res.Examples == null)
					continue;

				Word word = new Word();
				word.WordText = apiWord.Word;
				word.Meaning = res.Definition;
				word.Example = res.Examples.FirstOrDefault();
				wordsInQuery.Add(word);

			}

			
			return wordsInQuery;
		}
		#endregion

		#region private 
		private async Task<string> GetWordFromApi(string word)
		{
			WebResponse response = null;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://wordsapiv1.p.mashape.com/words/{word}");
			request.Headers["X-Mashape-Key"] = "y0gl4NyjTlmshwNGIm7O4HcHzdlCp1mBlpKjsndD8Yz653sWjK";
			request.Headers["Accept"] = "application/json";
			request.Credentials = CredentialCache.DefaultCredentials;

			try
			{
				response = await request.GetResponseAsync();
			}
			catch (WebException ex) { return ""; }

			if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
			{
				Stream dataStream = response.GetResponseStream();
				StreamReader reader = new StreamReader(dataStream);
				string responseFromServer = reader.ReadToEnd();

				return responseFromServer;
			}
			return "";
		}
		#endregion
	}
}