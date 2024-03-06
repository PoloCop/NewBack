using Microsoft.EntityFrameworkCore;

namespace NewBack.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    // public DbSet<User>? Users { get; set; }
}