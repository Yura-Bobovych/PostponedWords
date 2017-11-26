using System;
using System.Collections.Generic;

namespace PostponedWords.Models.Db
{
    public partial class Dictionary
    {
        public int Id { get; set; }
        public int DictionaryId { get; set; }
        public int WordId { get; set; }
        public DateTime WordAddDate { get; set; }
    }
}
