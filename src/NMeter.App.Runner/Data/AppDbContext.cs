using Microsoft.EntityFrameworkCore;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        public DbSet<Plan> Plans { get; set; }

        public DbSet<Execution> Executions { get; set; }

        public DbSet<Result> Results { get; set; }
    }
}