using GMYEL8_HSZF_2024251.Application;
using GMYEL8_HSZF_2024251.Console.Middleware;
using GMYEL8_HSZF_2024251.Persistence.MsSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GMYEL8_HSZF_2024251.Console;

public static class AppStart
{
    public static IConfiguration ReadConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        return configuration;
    }

    public static IHost CreateAppHost(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string is not set in appsettings.");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MiddlewarePipeline>();
                services.AddSingleton<ICustomMiddleware, CustomExceptionHandlingMiddleware>();

                services.AddMsSqlDbContext(connectionString);
                services.AddMsSqlDataProviders();
                services.AddApplicationServices();
            });

        host.ConfigureLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Error);
        });

        return host.Build();
    }
}
