using Microsoft.Extensions.DependencyInjection;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Services;
using NMeter.App.Runner.Tests.Data;

namespace NMeter.App.Runner.Tests;

[TestClass]
public class ExecutionBuilderTest
{
    private readonly PlanExecution _planExecution = PlanExecutionData.GetData();
    private IServiceProvider _serviceProvider;

    public ExecutionBuilderTest()
    {
        var serviceProviderFactory = new DefaultServiceProviderFactory();
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddHttpClient();

        _serviceProvider = serviceProviderFactory.CreateServiceProvider(serviceCollection);
        
    }

    [TestMethod]
    public void Build_Execution_Positive_Test()
    {
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution).CreateThreads().Build();

        Assert.IsNotNull(planExecutionInstance);
    }

    [TestMethod]
    public void Build_Execution_Positive_Id_Test()
    {
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution).CreateThreads().Build();

        Assert.AreEqual(planExecutionInstance.Id, _planExecution.Execution.Id.ToString());
    }

    [TestMethod]
    public void Build_Execution_Positive_Threads_Amount_Test()
    {
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution).CreateThreads().Build();

        Assert.AreEqual(planExecutionInstance.ExecutionThreads.Count, 
            _planExecution.Plan.Profile.UsersNumber);
    }

    [TestMethod]
    public void Build_Execution_Positive_Steps_Amount_Test()
    {
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution).CreateThreads().Build();

        Assert.AreEqual(planExecutionInstance.ExecutionThreads.FirstOrDefault().Value.Steps.Count, 
            _planExecution.Plan.Steps.Count);
    }
}