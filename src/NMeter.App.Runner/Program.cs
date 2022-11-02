using Microsoft.EntityFrameworkCore;
using NMeter.App.Runner.AsyncDataServices;
using NMeter.App.Runner.Data;
using NMeter.App.Runner.EventProcessing;
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
    })
    .Build();

await host.RunAsync();
