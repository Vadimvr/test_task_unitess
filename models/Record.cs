using System;

namespace models
{
    public class Record
    {
        public int Id { get; set; }
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }
    public class RecordDTO
    {
        public int Id { get; set; }
        public int x1 { get; set; }
        public int x2 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }


        public static RecordDTO ConvertToRecordDTO(Record record)
        {
            return new RecordDTO
            {
                Id = record.Id,
                x1 = record.X1,
                x2 = record.X2,
                y1 = record.Y1,
                y2 = record.Y2,
            };
        }
    }
}