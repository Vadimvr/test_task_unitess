using models;

namespace api.Services.db.Data
{
    public static class SomeRecordsGenerator
    {
        private static Random Random = new Random();

        public static List<Record> GenerateRecord()
        {
            List<Record> records = new List<Record>();

            for (int i = 0; i < 1; i++)
            {
                records.Add(
                    new Record()
                    {
                       X1= -i - 5,
                       X2= -i - 5,
                       Y1= -i - 5,
                       Y2= -i - 5,
                    });
            }
            return records;
        }
    }
}
