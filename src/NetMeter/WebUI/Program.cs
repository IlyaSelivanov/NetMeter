using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Auth;
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
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(@"https://localhost:5051/") });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IStepRepository, StepRepository>();
            services.AddScoped<IExecutionRepository, ExecutionRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            services.AddAuthorizationCore();
            services.AddScoped<JwtAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());
            services.AddScoped<ILoginService, JwtAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JwtAuthenticationStateProvider>());
        }
    }
}
