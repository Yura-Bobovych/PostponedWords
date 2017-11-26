using System;
using System.Collections.Generic;

namespace PostponedWords.Models.Db
{
    public partial class DictionaryList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
