using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionStepBuilder<TData, TStepBody>
        where TStepBody : IExecutionStepBody
    {
        IExecutionBuilder<TData> ExecutionBuilder { get; }

        ExecutionStep<TStepBody> Step { get; }
    }
}