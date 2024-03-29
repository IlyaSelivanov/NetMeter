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
            var httpResponseMessage = HttpResponseMessageData.GetRefreshData();
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

        [TestMethod]
        public async Task RefreshPlanVariablesAsync_Empty_Expression_Negative_Test()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var httpResponseMessage = HttpResponseMessageData.GetRefreshData();
            var globalVariables = new List<PlanGlobalVariable>
            {
                new PlanGlobalVariable
                {
                    Name = "str",
                    Value = string.Empty,
                    Expression = "$[0].str"
                },
                new PlanGlobalVariable
                {
                    Name = "int",
                    Value = string.Empty,
                    Expression = string.Empty
                }
            };

            await planVariablesManager.RefreshPlanVariablesAsync(httpResponseMessage, globalVariables);

            Assert.AreEqual(string.Empty, globalVariables?.ElementAtOrDefault(1)?.Value);
        }

        [TestMethod]
        public async Task RefreshPlanVariablesAsync_Response_Null_Negative_Test()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var globalVariables = new List<PlanGlobalVariable>
            {
                new PlanGlobalVariable
                {
                    Name = "str",
                    Value = string.Empty,
                    Expression = "$[0].str"
                }
            };



            try
            {
                await Assert.ThrowsExceptionAsync<Exception>(() => planVariablesManager.RefreshPlanVariablesAsync(null, globalVariables));
            }
            catch (AssertFailedException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task RefreshPlanVariablesAsync_Variables_Null_Negative_Test()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var httpResponseMessage = HttpResponseMessageData.GetRefreshData();
        
            try
            {
                await Assert.ThrowsExceptionAsync<Exception>(() => planVariablesManager.RefreshPlanVariablesAsync(httpResponseMessage, null));
            }
            catch (AssertFailedException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task RefreshPlanVariablesAsync_Variables_Empty_Negative_Test()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var httpResponseMessage = HttpResponseMessageData.GetRefreshData();
            var globalVariables = new List<PlanGlobalVariable>();
        
            try
            {
                await Assert.ThrowsExceptionAsync<Exception>(() => planVariablesManager.RefreshPlanVariablesAsync(httpResponseMessage, globalVariables));
            }
            catch (AssertFailedException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void UpdateRequestData_Upate_All_Positive()
        {
            var logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<PlanVariablesManager>();
            var planVariablesManager = new PlanVariablesManager(logger);
            var step = StepData.GetData();
            var globalVariables = new List<PlanGlobalVariable>
            {
                new PlanGlobalVariable
                {
                    Name = "body",
                    Value = "new_body",
                    Expression = "$.body"
                },
                new PlanGlobalVariable
                {
                    Name = "header",
                    Value = "new_header",
                },
                new PlanGlobalVariable
                {
                    Name = "parameter",
                    Value = "new_parameter",
                }
            };

            planVariablesManager.UpdateRequestData(globalVariables, step);

            Assert.AreEqual("new_header", step.Headers.ElementAtOrDefault(0)?.Value);
            Assert.AreEqual("new_parameter", step.Parameters.ElementAtOrDefault(0)?.Value);
            Assert.IsTrue(step.Body.Equals(@"{""body"":""new_body""}"), step.Body);
        }
    }
}