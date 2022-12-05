using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class SingleExecutionThreadBuilder : ExecutionThreadBuilder<SingleExecutionThread, SingleExecutionThreadBuilder>
    {
        public SingleExecutionThreadBuilder(
            IServiceProvider serviceProvider,
            PlanExecution planExecution) : base(serviceProvider, planExecution)
        { }

        public override SingleExecutionThread Build()
        {
            return _executionThread as SingleExecutionThread;
        }
    }
}