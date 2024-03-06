using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewBack.DbContext;
using NewBack.Repository;
using NewBack.Repository.Contracts;
using NewBack.Services;
using NewBack.Services.Contracts;

public class Program
{
    public static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((hostBuilder,services) =>
            {
        
                services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql( hostBuilder.Configuration.GetConnectionString("MyConnectionString"), 
                        MySqlServerVersion.LatestSupportedServerVersion));
             
        
                services.AddScoped<IEventService,EventService>();

        
                services.AddScoped<IEventRepository,EventRepository>();

                services.AddLogging();

            })
            .Build();

        host.Run();
    }
}