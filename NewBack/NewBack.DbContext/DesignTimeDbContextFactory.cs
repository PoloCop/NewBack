using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NewBack.DbContext;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql( "server=localhost;port=8889;user=root;password=root;database=BddEvaluationDev;",
            MySqlServerVersion.LatestSupportedServerVersion);

        return new AppDbContext(optionsBuilder.Options);
    }
}