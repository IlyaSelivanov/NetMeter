using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Tests.Data;

namespace NMeter.Api.Reporting.Tests
{
    [TestClass]
    public class ResultRepositoryTest
    {
        private readonly IServiceProvider _serviceProvider;

        public ResultRepositoryTest()
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AppDbContext>(options =>
            {
                // options.UseSqlServer("Server=localhost, 1433;Initial Catalog=NMeterDB;User ID=;Password=;TrustServerCertificate=True");
                options.UseInMemoryDatabase("ResultRepositoryTestDb");
            });
            serviceCollection.AddTransient<IResultRepository, ResultRepository>();

            _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        [TestMethod]
        public void GetExecutionResultsAmount_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var count = repository.GetExecutionResultsAmount(1);

            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void GetExecutionResultsAmount_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var count = repository.GetExecutionResultsAmount(2);

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public async Task GetExecutionResultsAsync_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var results = await repository.GetExecutionResultsAsync(1);

            Assert.AreEqual(3, results.Count());
        }

        [TestMethod]
        public async Task GetExecutionResultsAsync_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var results = await repository.GetExecutionResultsAsync(2);

            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void GetExecutionSuccessAmount_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var results = repository.GetExecutionSuccessAmount(1);

            Assert.AreEqual(2, results);
        }

        [TestMethod]
        public void GetExecutionSuccessAmount_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var results = repository.GetExecutionSuccessAmount(2);

            Assert.AreEqual(0, results);
        }
        
        [TestMethod]
        public void GetMaxSuccessResponseTime_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetMaxSuccessResponseTime(1);

            Assert.AreEqual(200L, result);
        }

        [TestMethod]
        public void GetMaxSuccessResponseTime_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetMaxSuccessResponseTime(2);

            Assert.AreEqual(0L, result);
        }

        [TestMethod]
        public void GetMinSuccessResponseTime_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetMinSuccessResponseTime(1);

            Assert.AreEqual(100L, result);
        }

        [TestMethod]
        public void GetMinSuccessResponseTime_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetMinSuccessResponseTime(2);

            Assert.AreEqual(0L, result);
        }

        [TestMethod]
        public void GetAvgSuccessResponseTime_Positive_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetAvgSuccessResponseTime(1);
            
            Assert.AreEqual(150L, result);
        }

        [TestMethod]
        public void GetAvgSuccessResponseTime_Negative_Test()
        {
            var context = _serviceProvider.GetRequiredService<AppDbContext>();
            var repository = _serviceProvider.GetRequiredService<IResultRepository>();

            DatabaseManager.SeedData(context);
            var result = repository.GetAvgSuccessResponseTime(0);
            
            Assert.AreEqual(0L, result);
        }
    }
}