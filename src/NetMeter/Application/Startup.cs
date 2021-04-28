using Application.Context;
using Application.Repository;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44353")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            IConnectionStringProvider connectionStringProvider = new ConnectionStringProvider(Configuration);
            string connString = connectionStringProvider.GetConnectionString("DomainDatabase");
            //string connString = Configuration.GetConnectionString("DomainDatabase");
            services.AddDbContext<EfDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });

            services.AddScoped<EfDbContext>();
            services.AddScoped<ExecutorService>();
            services.AddScoped<IPlanRepository, EfPlanRepository>();
            services.AddScoped<IStepRepository, EfStepRepository>();
            services.AddScoped<IResultRepository, EfResultRepository>();
            services.AddScoped<IExecutionRepository, EfExecutionRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
