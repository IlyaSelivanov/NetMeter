using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<PlanVariable> PlanVariables { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<Header> Headers { get; set; }

        public DbSet<UrlParameter> UrlParameters { get; set; }
    }
}