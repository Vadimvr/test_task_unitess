using models;

namespace api.Services.db.Data
{
    public static class SomeRecordsGenerator
    {
        private static Random Random = new Random();

        public static List<Record> GenerateRecord()
        {
            List<Record> records = new List<Record>();

            for (int i = 0; i < 100; i++)
            {
                records.Add(
                    new Record()
                    {
                        Date = DateTime.UtcNow,
                        Text = $"Text: i = {i}",
                        HiddenText = $"Hidden text: i = {i}"
                    });
            }
            return records;
        }
    }
}
