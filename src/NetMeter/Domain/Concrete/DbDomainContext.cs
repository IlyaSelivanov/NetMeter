using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Concrete
{
    public class DbDomainContext : DbContext
    {
        public DbSet<Plan> Plans { get; }
        public DbSet<Step> Steps { get; }

        public DbDomainContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
