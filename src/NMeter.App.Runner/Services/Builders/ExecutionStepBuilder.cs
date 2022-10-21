using NMeter.App.Runner.Interface;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionStepBuilder<TData, TStepBody> : IExecutionStepBuilder<TData, TStepBody>
        where TStepBody : IExecutionStepBody
    {
        private ExecutionBuilder<TData> _executionBuilder;
        private ExecutionStep<TStepBody> _step;

        public ExecutionStepBuilder(ExecutionBuilder<TData> executionBuilder, 
            ExecutionStep<TStepBody> step)
        {
            _executionBuilder = executionBuilder;
            _step = step;
        }

        public IExecutionBuilder<TData> ExecutionBuilder => throw new NotImplementedException();

        public ExecutionStep<TStepBody> Step => throw new NotImplementedException();
    }
}