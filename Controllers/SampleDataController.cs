using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace PostponedWords.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SampleDataController : Controller
    {
		private readonly ILogger<WordsController> _logger;
		public SampleDataController(ILogger<WordsController> logger)
		{
			_logger = logger;	 			
		}
		private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

		[Authorize(Policy = "BaseUser")]
		[HttpGet]
		public string  GetData([FromQuery]string word)
		{
			_logger.LogDebug("get word from getdata"+word);
			return word;
		}
		[Authorize(Policy = "BaseUser")]
		[HttpPost]
		public string PostData([FromBody]WordSchema word)
		{
			_logger.LogDebug("get word from postdata" + word.word);
			return word.word;
		}

		public class WordSchema
		{
			public string word { get; set; }
		}
		public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
