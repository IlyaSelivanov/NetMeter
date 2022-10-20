using NMeter.App.Runner.Interface;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionThreadStepBuilder<TData, TStepBody> : IExecutionStepBuilder<TData, TStepBody>
        where TStepBody : IExecutionStepBody
    {
        public IExecutionBuilder<TData> ExecutionBuilder => throw new NotImplementedException();

        public ExecutionStep<TStepBody> Step => throw new NotImplementedException();
    }
}