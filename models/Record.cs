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
    public class RecordDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        

        public static RecordDTO ConvertToRecordDTO(Record record)
        {
            return new RecordDTO
            {
                Id = record.Id,
                Date = record.Date,
                Text = record.Text,
            };
        }
    }
}