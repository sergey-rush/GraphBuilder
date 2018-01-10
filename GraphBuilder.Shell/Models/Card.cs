using System;
using System.ComponentModel;

namespace GraphBuilder.Shell.Models
{
    [DisplayName("Карточка")]
    public class Card
    {
        public string Header { get; set; }
        public string Info { get; set; }
        public string Source { get; set; }
        public string Summary { get; set; }
        public int Score { get; set; }
        public int Index { get; set; }
        
        public DateTime Date { get; set; }
    }
}