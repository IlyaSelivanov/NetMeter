using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NMeter.Api.Reporting.Controllers;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Domain;
using NMeter.Api.Reporting.Models;
using NMeter.Api.Reporting.Services;
using NMeter.Api.Reporting.Tests.Data;

namespace NMeter.Api.Reporting.Tests
{
    [TestClass]
    public class ResultsControllerTest
    {
        private readonly IServiceProvider _serviceProvider;

        public ResultsControllerTest()
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceCollection = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder
                .SetBasePath(Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName!)
                .AddJsonFile("resultDomain.json");
            IConfiguration configuration = configurationBuilder.Build();

            serviceCollection.AddScoped<IConfiguration>(_ => configuration);
            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("ResultDomainTestDb");
            });
            serviceCollection.AddDistributedMemoryCache();
            serviceCollection.AddTransient<IResultRepository, ResultRepository>();
            serviceCollection.AddTransient<IHashProvider, SHA256HashProvider>();
            serviceCollection.AddTransient<IResultDomain, ResultDomain>();
            serviceCollection.AddLogging();            

            _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        [TestMethod]
        public async Task GetExecutionAggregatedResult_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var logger = _serviceProvider.GetRequiredService<ILogger<ResultsController>>();
            var resultDomain = _serviceProvider.GetRequiredService<IResultDomain>();
            var resultsController = new ResultsController(logger, resultDomain);

            DatabaseManager.SeedData(context);
            var okResult = (await resultsController.GetExecutionAggregatedResult(
                1, 
                new RequestSettings
                {
                    FilterBy = new FilterBy(string.Empty, null!),
                    SortBy = new SortBy(string.Empty, false)
                })).Result as OkObjectResult;
            var executeResult = okResult?.Value as ExecutionResult;
            

            Assert.AreEqual(3, executeResult?.PagedResults.Results.Count());
        }
    }
}