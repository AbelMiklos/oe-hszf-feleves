using GMYEL8_HSZF_2024251.Application;
using GMYEL8_HSZF_2024251.Persistence.MsSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GMYEL8_HSZF_2024251.Console
{
    public class Program
    {
        public static async Task Main()
        {
            var configuration = GetConfiguration();

            var host = CreateAppHost(configuration);
            await host.StartAsync();
        }

        private static IHost CreateAppHost(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string is not set in appsettings.");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMsSqlDbContext(connectionString);
                    services.AddMsSqlDataProviders();
                    services.AddApplicationServices();
                });

            return host.Build();
        }

        private static IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return configuration;
        }
    }
}
