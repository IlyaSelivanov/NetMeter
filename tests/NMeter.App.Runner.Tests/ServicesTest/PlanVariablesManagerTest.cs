using Microsoft.Extensions.Logging;
using NMeter.App.Runner.Primitives;
using NMeter.App.Runner.Services;
using NMeter.App.Runner.Tests.Data;

namespace NMeter.App.Runner.Tests
{
    [TestClass]
    public class PlanVariablesManagerTest
    {
        [TestMethod]
        public async Task RefreshPlanVariablesAsync_Positive_Test()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var httpResponseMessage = HttpResponseMessageData.GetData();
            var globalVariables = new List<PlanGlobalVariable>
            {
                new PlanGlobalVariable
                {
                    Name = "str",
                    Value = string.Empty,
                    Expression = "$[0].str"
                }
            };

            await planVariablesManager.RefreshPlanVariablesAsync(httpResponseMessage, globalVariables);

            Assert.AreEqual("str0", globalVariables?.ElementAtOrDefault(0)?.Value);
        }
    }
}