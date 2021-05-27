using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Execution> Executions { get; set; }

        public EfDbContext(DbContextOptions options, IConnectionStringProvider connectionStringProvider) : base(options)
        {
            //Database.EnsureCreated();
            _connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = _connectionStringProvider.GetConnectionString("DomainDatabase");
            optionsBuilder.UseSqlServer(connString, b => b.MigrationsAssembly("Application"));
        }
    }
}
