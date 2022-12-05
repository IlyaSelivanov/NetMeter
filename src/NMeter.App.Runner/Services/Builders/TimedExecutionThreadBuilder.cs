using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class TimedExecutionThreadBuilder : ExecutionThreadBuilder<TimedExecutionThread, TimedExecutionThreadBuilder>
    {
        public TimedExecutionThreadBuilder(
            IServiceProvider serviceProvider,
            PlanExecution planExecution) : base(serviceProvider, planExecution)
        { }

        public TimedExecutionThreadBuilder SetDuration(int duration)
        {
            (_executionThread as TimedExecutionThread).Duration = duration;
            return this;
        }

        public override TimedExecutionThread Build()
        {
            return _executionThread as TimedExecutionThread;
        }
    }
}