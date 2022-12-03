using System;

namespace models
{
    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string HiddenText { get; set; }
    }
}