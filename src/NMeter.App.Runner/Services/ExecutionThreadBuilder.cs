using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionThreadBuilder
    {
        private ExecutionThread _executionThread = new ExecutionThread();

        private readonly IEnumerable<Step> _steps;

        public ExecutionThreadBuilder(IEnumerable<Step> steps)
        {
            _steps = steps;
        }

        public ExecutionThreadBuilder SetId(string id)
        {
            _executionThread.Id = id;
            return this;
        }

        public ExecutionThreadBuilder CreateSteps()
        {
            foreach(var step in _steps)
                _executionThread.Steps.Add(new HttpRequestStep());

            return this;
        }

        public ExecutionThread Build()
        {
            return _executionThread;
        }
    }
}