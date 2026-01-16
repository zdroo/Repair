using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repair.Infrastructure.Data;

namespace Repair.MigrationRunner
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var services = new ServiceCollection();
            services.AddDbContext<RepairDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLServer")));

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RepairDbContext>();
                Console.WriteLine("Applying pending migrations...");
                await db.Database.MigrateAsync();
                Console.WriteLine("Migrations applied successfully!");
            }
        }
    }
}
