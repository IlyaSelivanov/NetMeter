using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionThreadBuilder
    {
        private ExecutionThread _executionThread = new ExecutionThread();
        private readonly Plan _plan;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExecutionThreadBuilder> _logger;

        public ExecutionThreadBuilder(IServiceProvider serviceProvider,
            Plan plan) : this(plan)
        {
            _serviceProvider = serviceProvider;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<ExecutionThreadBuilder>();
        }

        public ExecutionThreadBuilder(Plan plan)
        {
            _plan = plan;
        }

        public ExecutionThreadBuilder SetId(string id)
        {
            _executionThread.Id = id;
            return this;
        }

        public ExecutionThreadBuilder CreateSteps()
        {
            foreach(var step in _plan.Steps)
                _executionThread.Steps.Add(new HttpRequestStep(_serviceProvider,
                    new Uri(_plan.BaseUrl),
                    step));

            return this;
        }

        public ExecutionThread Build()
        {
            return _executionThread;
        }
    }
}