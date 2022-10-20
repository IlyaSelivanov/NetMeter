using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionThreadStepBuilder<TData, TStepBody>
        where TStepBody : IExecutionThreadStepBody
    {
        IExecutionThreadBuilder<TData> ExecutionThreadBuilder { get; }

        ExecutionThreadStep<TStepBody> Step { get; }
    }
}