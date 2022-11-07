using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public abstract class AbstractStep : IStep
    {
        protected abstract void BeforeExecution();

        protected abstract void AfterExecution();

        protected abstract void ExecuteStep();

        public void Execute()
        {
            BeforeExecution();

            ExecuteStep();

            AfterExecution();
        }
    }
}