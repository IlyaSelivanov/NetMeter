using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Domain.Concrete
{
    public class EfDbContext : IdentityDbContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Execution> Executions { get; set; }

        public EfDbContext(DbContextOptions options, 
            IConnectionStringProvider connectionStringProvider,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            //Database.EnsureCreated();
            _connectionStringProvider = connectionStringProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = _connectionStringProvider.GetConnectionString("DomainDatabase");
            optionsBuilder.UseSqlServer(connString, b => b.MigrationsAssembly("Application"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Plan>()
                .HasQueryFilter(plan => plan.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value);
            builder.Entity<Step>()
                .HasQueryFilter(step => step.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value);
            builder.Entity<Execution>()
                .HasQueryFilter(execution => execution.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value);
            builder.Entity<Result>()
                .HasQueryFilter(result => result.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value);

            base.OnModelCreating(builder);
        }
    }
}
