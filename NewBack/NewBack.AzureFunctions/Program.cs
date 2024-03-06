using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewBack.DbContext;

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
             
        
      

                services.AddLogging();

            })
            .Build();

        host.Run();
    }
}