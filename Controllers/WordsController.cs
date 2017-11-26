using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostponedWords.Middleware;
using PostponedWords.Models.Db;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace PostponedWords.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class WordsController : Controller
    {
		private readonly ILogger<WordsController> _logger;
		private readonly IDbWordsMiddleware _wordMW;

		private readonly AppSettings _configuration;

		public WordsController(ILogger<WordsController> logger,IDbWordsMiddleware wordWm, IOptions<AppSettings> configuration)
		{
			_configuration = configuration.Value;
			_logger = logger;
			_wordMW = wordWm;
		}
		public async Task<string> Index()
		{
			return "index";
		}
		[Produces("application/json")]
		[HttpGet]
		public async Task<IActionResult> Search([FromQuery] string Word)
		{
			_logger.LogDebug("Get user wonted word: "+ Word);
			List<Word> wordList = await _wordMW.GetWord(Word);
			
			if (wordList != null)
			{
				_logger.LogDebug("Send user word: " + wordList.FirstOrDefault()?.WordText);
				return new OkObjectResult(wordList);
			}
			return null;
		}
		[Authorize(Policy = "BaseUser")]
		[Produces("application/json")]
		[HttpPost]
		public IActionResult AddDictionaty([FromBody]DictionarySchema Schema)
		{
			if (Schema == null)
				return new BadRequestResult(); 
			_wordMW.AddDictionary(Schema.DictionaryName, GetUserId());
			return new OkResult();	 			
		}
		[Authorize(Policy = "BaseUser")]
		[Produces("application/json")]
		[HttpPost]
		public  IActionResult AddWordToDictionary([FromBody] WordToDictionarSchema Schema)
		{
			if (Schema == null)
				return new BadRequestResult();
			_wordMW.AddWordToDictionary(Schema.DictionaryId,Schema.WordId);
			return new OkResult();
		}
		[Authorize(Policy = "BaseUser")]
		[Produces("application/json")]
		[HttpGet]
		public IActionResult GetUserDictionaries()
		{
		  List<DictionaryList> dictList =_wordMW.GetUserDictionaries(GetUserId());
			return new OkObjectResult(dictList);
		}

		private int GetUserId()
		{
			string id = (from c in User.Claims
						 where c.Type == _configuration.JwtClaimIdentifiers.Id
						 select c.Value).Single();
			return Convert.ToInt32(id);
		}
		public class WordToDictionarSchema
		{
			public int DictionaryId { get; set; }
			public int WordId { get; set; } 
		}
		public class DictionarySchema
		{
			public string DictionaryName { get; set; }
		}
		public class WordSchema
		{
			public string Word { get; set; }
		}
    }
}
