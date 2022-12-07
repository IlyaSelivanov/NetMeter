using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Result> Results => Set<Result>();
    }
}