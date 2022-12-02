using Microsoft.EntityFrameworkCore;
using NMeter.App.Runner.AsyncDataServices;
using NMeter.App.Runner.Data;
using NMeter.App.Runner.EventProcessing;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient();
        services.AddHostedService<BackgroundRunner>();
        services.AddHostedService<MessageBusSubscriber>();
        services.AddSingleton<IExecutionQueue>(ctx =>
        {
            int queueCapacity = 100;
            return new ExecutionQueue(queueCapacity);
        });
        services.AddSingleton<IEventProcessor, EventProcessor>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(context.Configuration.GetConnectionString("NMeterDB"));
        });
        services.AddTransient<IPlanVariablesManager, PlanVariablesManager>();
        services.AddScoped<IResultRepository, ResultRepository>();
    })
    .Build();

await host.RunAsync();
