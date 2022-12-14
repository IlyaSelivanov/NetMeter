using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Domain;
using NMeter.Api.Reporting.Models;
using NMeter.Api.Reporting.Services;
using NMeter.Api.Reporting.Tests.Data;

namespace NMeter.Api.Reporting.Tests;

[TestClass]
public class ResultDomainTest
{
    private readonly IServiceProvider _serviceProvider;

    public ResultDomainTest()
    {
        var serviceProviderFactory = new DefaultServiceProviderFactory();
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            // options.UseSqlServer("Server=localhost, 1433;Initial Catalog=NMeterDB;User ID=;Password=;TrustServerCertificate=True");
            options.UseInMemoryDatabase("InMemoryDb");
        });
        serviceCollection.AddDistributedMemoryCache();
        // serviceCollection.AddStackExchangeRedisCache(options =>
        //     options.Configuration = "localhost:6379"
        // );
        serviceCollection.AddTransient<IResultRepository, ResultRepository>();
        serviceCollection.AddTransient<IHashProvider, SHA256HashProvider>();
        serviceCollection.AddTransient<IResultDomain, ResultDomain>();
        serviceCollection.AddLogging();
        
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder
            .SetBasePath(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName!)
            .AddJsonFile("resultDomain.json");
        IConfiguration configuration = configurationBuilder.Build();

        serviceCollection.AddScoped<IConfiguration>(_ => configuration);

        _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
    }

    [TestMethod]
    public async Task GetExecutionResult_Positive_Test()
    {
        var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var resultDomain = _serviceProvider.GetRequiredService<IResultDomain>();

        DatabaseManager.SeedData(context);

        var executeResult = await resultDomain.GetExecutionResult(1, 
        new RequestSettings
        {
            FilterBy = "",
            SortBy = "",
            PageIndex = 0
        });

        Assert.AreEqual(3, executeResult.TotalRequestsAmount);
        Assert.AreEqual(3, executeResult.PagedResults.Results.Count());
        Assert.AreEqual(2, executeResult.SuccessAmount);
        Assert.AreEqual(100L, executeResult.MinResponseTime);
        Assert.AreEqual(200L, executeResult.MaxResponseTime);
    }

    [TestMethod]
    public async Task GetExecutionResult_Empty_Results_Negative_Test()
    {
        var context = _serviceProvider.GetRequiredService<AppDbContext>();
        var resultDomain = _serviceProvider.GetRequiredService<IResultDomain>();

        DatabaseManager.SeedData(context);

        var executeResult = await resultDomain.GetExecutionResult(2, 
        new RequestSettings
        {
            FilterBy = "",
            SortBy = "",
            PageIndex = 0
        });

        Assert.AreEqual(0, executeResult.TotalRequestsAmount);
        Assert.AreEqual(0, executeResult.PagedResults.Results.Count());
        Assert.AreEqual(0, executeResult.SuccessAmount);
        Assert.AreEqual(0L, executeResult.MinResponseTime);
        Assert.AreEqual(0L, executeResult.MaxResponseTime);
    }
}