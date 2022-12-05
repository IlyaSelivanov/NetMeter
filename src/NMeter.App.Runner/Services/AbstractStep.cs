using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public abstract class AbstractStep : IStep
    {
        public int Order { get ; set; }

        protected abstract Task BeforeExecution();

        protected abstract Task AfterExecution();

        protected abstract Task ExecuteStep();

        public async Task Execute()
        {
            await BeforeExecution();

            await ExecuteStep();

            await AfterExecution();
        }
    }
}