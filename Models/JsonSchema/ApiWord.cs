using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostponedWords.Models.JsonSchema
{
	public class ApiWord
	{
		public string Word { get; set; }
		public List<Results> Results { get; set; }
		public override string ToString()
		{
			return "Word: " + Word + "\n Definition: " + Results.FirstOrDefault().Definition + "\n Example: " + Results.FirstOrDefault().Examples.FirstOrDefault();
		}


	}

	public class Results
	{
		public string Definition { get; set; }
		public string partOfSpeech { get; set; }
		public List<string> Synonyms { get; set; }
		public List<string> TypeOf { get; set; }
		public List<string> HasTypes { get; set; }
		public List<string> Derivation { get; set; }
		public List<string> Examples { get; set; }
	}
	
}
