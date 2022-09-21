using NMeter.App.Runner.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddHostedService<BackgroundRunner>();
    })
    .Build();

await host.RunAsync();
