using db;
using models;

namespace api.Services.db.Data
{
    internal class DbInitializer
    {
        internal static void Initialize(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();
            if (dbContext.Records.Any()) return;

            List<Record> records = SomeRecordsGenerator.GenerateRecord();

            foreach (var record in records)
            {
                dbContext.Records.Add(record);
            }

            dbContext.SaveChanges();
        }
    }
    internal static class DbInitializerExtension
    {
        public static IApplicationBuilder UseItToSeedSqlServer(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return app;
        }
    }
}
