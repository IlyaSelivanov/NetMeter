using NMeter.App.Runner.Interface;

namespace NMeter.App.Runner.Models
{
    public abstract class ExecutionStepBody : IExecutionStepBody
    {
        public abstract ExecutionResult Run(IExecutionStepContext context);

        public Task<ExecutionResult> RunAsync(IExecutionStepContext context)
        {
            return Task.FromResult(Run(context));
        }
    }
}