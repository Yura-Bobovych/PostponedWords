using System;
using System.Collections.Generic;

namespace PostponedWords.Models.Db
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
