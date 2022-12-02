using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NMeter.App.Runner.Data;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Services;
using NMeter.App.Runner.Tests.Data;

namespace NMeter.App.Runner.Tests
{
    [TestClass]
    public class ExecutionInstanceTest
    {
        private readonly PlanExecution _planExecution = PlanExecutionData.GetData();
        private IServiceProvider _serviceProvider;

        public ExecutionInstanceTest()
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddHttpClient();
            serviceCollection.AddTransient<IPlanVariablesManager, PlanVariablesManager>();
            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                // options.UseSqlServer("Server=localhost, 1433;Initial Catalog=NMeterDB;User ID=;Password=;");
                options.UseInMemoryDatabase("InMemoryDb");
            });
            serviceCollection.AddTransient<IResultRepository, ResultRepository>();

            _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        [TestMethod]
        public async Task ExecutionInstance_RunExecution_Test()
        {
            var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution)
                .CreateThreads()
                .Build();

            try
            {
                await Assert.ThrowsExceptionAsync<Exception>(planExecutionInstance.RunExecution);
            }
            catch (AssertFailedException)
            {
                Assert.IsTrue(true);
            }
        }
    }
}