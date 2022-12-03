using db;
using Microsoft.EntityFrameworkCore;

namespace api.Services.db
{
    internal static class ServiceRegistratorDB
    {
        public static IServiceCollection AddServicesDB(this IServiceCollection services)
        {
            return services.AddDbContext<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase(
                        databaseName: Guid.NewGuid().ToString()), ServiceLifetime.Singleton);
        }
    }
}
