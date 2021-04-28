using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Repository;
using WebUI.Services;

namespace WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:5001/") });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<IExecutionRepository, ExecutionRepository>();
        }
    }
}
