using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Execution> Executions { get; set; }

        public EfDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
