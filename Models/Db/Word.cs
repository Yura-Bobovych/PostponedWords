using System;
using System.Collections.Generic;

namespace PostponedWords.Models.Db
{
    public partial class Word
    {
        public int Id { get; set; }
        public string WordText { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
    }
}
