using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NMeter.App.Runner.Data;
using NMeter.App.Runner.Interfaces;
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
        serviceCollection.AddTransient<IPlanVariablesManager, PlanVariablesManager>();
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            //options.UseSqlServer("Server=localhost, 1433;Initial Catalog=NMeterDB;User ID=;Password=;");
            options.UseInMemoryDatabase("InMemoryDb");
        });
        serviceCollection.AddTransient<IResultRepository, ResultRepository>();

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

    [TestMethod]
    public void Build_Single_Execution_Positive_Test()
    {
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, _planExecution).CreateThreads().Build();

        var executionThread = planExecutionInstance.ExecutionThreads.FirstOrDefault().Value;
        var type = executionThread.GetType();

        Assert.AreEqual(typeof(SingleExecutionThread), type);
    }

    [TestMethod]
    public void Build_Timed_Execution_Positive_Test()
    {
        var planExecution = PlanExecutionData.GetTimedData();
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, planExecution).CreateThreads().Build();

        var executionThread = planExecutionInstance.ExecutionThreads.FirstOrDefault().Value;
        var type = executionThread.GetType();

        Assert.AreEqual(typeof(TimedExecutionThread), type);
    }

    [TestMethod]
    public void Build_Timed_Execution_Duration_Positive_Test()
    {
        var planExecution = PlanExecutionData.GetTimedData();
        var planExecutionInstance = new ExecutionBuilder(_serviceProvider, planExecution).CreateThreads().Build();

        var executionThread = (TimedExecutionThread)planExecutionInstance.ExecutionThreads.FirstOrDefault().Value;

        Assert.AreEqual(planExecution.Plan.Profile.Duration, executionThread.Duration);
    }
}