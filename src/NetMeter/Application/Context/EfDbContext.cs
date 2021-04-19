using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public class EfDbContext : DbContext
    {
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Step> Steps { get; set; }

        public EfDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
