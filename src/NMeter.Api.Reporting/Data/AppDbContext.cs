using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Result> Results => Set<Result>();
    }
}