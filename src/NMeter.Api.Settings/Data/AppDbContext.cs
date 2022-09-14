using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<PlanVariable> PlanVariables { get; set; }

        public DbSet<Execution> Executions { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<Header> Headers { get; set; }

        public DbSet<UrlParameter> UrlParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Plan o2o Profile
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Profile)
                .WithOne(p => p.Plan)
                .HasForeignKey<Profile>(p => p.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //Plan m2o PlanVariable
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Variables)
                .WithOne(v => v.Plan)
                .HasForeignKey(v => v.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //Plan m2o Execution
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Executions)
                .WithOne(e => e.Plan)
                .HasForeignKey(e => e.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //Plan m2o Step
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Steps)
                .WithOne(s => s.Plan)
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            //Step m2o Header
            modelBuilder.Entity<Step>()
                .HasMany(s => s.Headers)
                .WithOne(h => h.Step)
                .HasForeignKey(h => h.StepId)
                .OnDelete(DeleteBehavior.Cascade);

            //Step m2o UrlParameter
            modelBuilder.Entity<Step>()
                .HasMany(s => s.Parameters)
                .WithOne(p => p.Step)
                .HasForeignKey(p => p.StepId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}