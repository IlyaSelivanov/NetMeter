using NMeter.App.Runner.Models;

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
    }
}