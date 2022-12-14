using Microsoft.Extensions.DependencyInjection;
using NMeter.Api.Reporting.Services;

namespace NMeter.Api.Reporting.Tests
{
    [TestClass]
    public class HashProviderTest
    {
        private readonly IServiceProvider _serviceProvider;

        public HashProviderTest()
        {
            var serviceProviderFactory = new DefaultServiceProviderFactory();
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IHashProvider, SHA256HashProvider>();
            serviceCollection.AddLogging();

            _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        [TestMethod]
        public void GenerateHash_Positive_Test()
        {
            var hashProvider = _serviceProvider.GetRequiredService<IHashProvider>();

            var obj1 = new { s = "string", i = 0 };
            var obj2 = new { s = "string", i = 0 };

            var hash1 = hashProvider.GenerateHash(obj1);
            var hash2 = hashProvider.GenerateHash(obj2);

            Assert.AreEqual(hash1, hash2);
        }

        [TestMethod]
        public void GenerateHash_Negative_Test()
        {
            var hashProvider = _serviceProvider.GetRequiredService<IHashProvider>();

            var obj1 = new { s = "string", i = 0 };
            var obj2 = new { s = "string", i = 1 };

            var hash1 = hashProvider.GenerateHash(obj1);
            var hash2 = hashProvider.GenerateHash(obj2);

            Assert.AreNotEqual(hash1, hash2);
        }
    }
}