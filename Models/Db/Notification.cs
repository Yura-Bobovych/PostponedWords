using System;
using System.Collections.Generic;

namespace PostponedWords.Models.Db
{
    public partial class Notification
    {
        public int UserId { get; set; }
        public string Telegram { get; set; }
        public bool EmailSend { get; set; }
        public bool TelegramSend { get; set; }
        public int TelegramHowOfften { get; set; }
        public int EmailHowOfften { get; set; }
        public int TelegramNotificationType { get; set; }
        public int EmailNotificationType { get; set; }
    }
}
