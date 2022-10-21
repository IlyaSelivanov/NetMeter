using NMeter.App.Runner.Models;
using NMeter.App.Runner.Steps;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionBuilder
    {
        List<ExecutionStep> Steps { get; }

        void AddStep(ExecutionStep step);

        IExecutionBuilder<T> UseData<T>();
    }

    public interface IExecutionBuilder<TData> : IExecutionBuilder
    {
        IExecutionStepBuilder<TData, TStep> Start<TStep>() where TStep : IExecutionStepBody;
        IExecutionStepBuilder<TData, InlineStepBody> Start(Func<IExecutionStepContext, ExecutionResult> body);
    }
}