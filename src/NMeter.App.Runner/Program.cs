using NMeter.App.Runner.AsyncDataServices;
using NMeter.App.Runner.EventProcessing;
using NMeter.App.Runner.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddHostedService<BackgroundRunner>();
        services.AddHostedService<MessageBusSubscriber>();
        services.AddSingleton<IBackgroundQueue>(ctx =>
        {
            int queueCapacity = 100;
            return new BackgroundQueue(queueCapacity);
        });
        services.AddSingleton<IEventProcessor, EventProcessor>();
    })
    .Build();

await host.RunAsync();
