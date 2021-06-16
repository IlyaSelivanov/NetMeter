using Application.Services;
using Application.Workflow;
using Application.Workflow.Steps;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowCore.Interface;

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

            services.AddDbContext<EfDbContext>();

            services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddScoped<EfDbContext>();
            services.AddScoped<ExecutorService>();
            services.AddScoped(typeof(IGenericRepository<Plan>), typeof(EfPlanRepository));
            services.AddScoped(typeof(IGenericRepository<Step>), typeof(EfStepRepository));
            services.AddScoped(typeof(IGenericRepository<Execution>), typeof(EfExecutionRepository));
            services.AddScoped(typeof(IGenericRepository<Result>), typeof(EfResultRepository));

            services.AddWorkflow();
            services.AddTransient<SaveResults>();
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

            var host = app.ApplicationServices.GetService<IWorkflowHost>();
            host.RegisterWorkflow<RequestWorkflow, RequestData>();
            host.Start();
        }
    }
}
