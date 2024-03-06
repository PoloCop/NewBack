using Microsoft.EntityFrameworkCore;
using NewBack.Models;

namespace NewBack.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Event>? Events { get; set; }
    
    // public DbSet<User>? Users { get; set; }
}