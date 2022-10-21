using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionStepBody
    {
        Task<ExecutionResult> RunAsync(IExecutionStepContext context);
    }
}