using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionThreadBuilder
    {
        List<ExecutionThreadStep> Steps { get; }

        void AddStep(ExecutionThreadStep step);
    }

    public interface IExecutionThreadBuilder<TData> : IExecutionThreadBuilder
    {
        IExecutionThreadStepBuilder<TData, TStep> CreateStep<TStep>() 
            where TStep : IExecutionThreadStepBody;
    }
}