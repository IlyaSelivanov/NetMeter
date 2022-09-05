using Microsoft.EntityFrameworkCore;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public static class DatabaseManager
    {
        public static void PrepareDatabase(IApplicationBuilder applicationBuilder,
            IWebHostEnvironment environment)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                if (context != null)
                    InitiateDb(context, environment);
            }
        }

        private static void InitiateDb(AppDbContext context, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
                SeedData(context);
            else if (environment.IsProduction())
                ApplyMigations(context);
        }

        private static void ApplyMigations(AppDbContext context)
        {
            try
            {
                Console.WriteLine($"--> Applying migrations...");
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't apply migrations: {ex}");
            }
        }

        private static void SeedData(AppDbContext context)
        {
            Console.WriteLine($"Seeding data...");

            context.Plans.Add(
                new Plan { Id = 1, Name = "Test Plan #1", BaseUrl = @"https://www.google.com" }
            );

            context.Add(
                new Profile { Id = 1, UsersNumber = 1, Duration = 10, PlanId = 1}
            );

            context.AddRange(
                new Step
                {
                    Id = 1,
                    Path = @"/",
                    Order = 0,
                    Method = Method.GET,
                    PlanId = 1
                },
                new Step
                {
                    Id = 2,
                    Path = @"/search?q=dotnet+core",
                    Order = 0,
                    Method = Method.GET,
                    PlanId = 1
                }
            );

            context.SaveChanges();
        }
    }
}