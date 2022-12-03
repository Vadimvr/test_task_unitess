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
                builder.Services.AddSwaggerGen();
                builder.Services.AddScoped<DbInitializer>();
            }

            var app = builder.Build();


            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseItToSeedSqlServer();
            }

            app.MapControllers();

            app.Run();
        }
    }
}