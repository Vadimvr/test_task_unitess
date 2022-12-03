using api.Services.db;
using api.Services.db.Data;
namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // db
            builder.Services.AddServicesDB();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddScoped<DbInitializer>();
            }

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseItToSeedSqlServer();
            }

            app.MapControllers();

            app.Run();
        }
    }
}