using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public class ExecutionThreadBuilder<T> where T: ExecutionThread, new()
    {
        private ExecutionThread _executionThread = new T();
        private readonly PlanExecution _planExecution;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExecutionThreadBuilder<T>> _logger;

        public ExecutionThreadBuilder(IServiceProvider serviceProvider,
            PlanExecution planExecution) : this(planExecution)
        {
            _serviceProvider = serviceProvider;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<ExecutionThreadBuilder<T>>();

            _executionThread.PlanGlobalVariables = new List<PlanGlobalVariable>();
        }

        public ExecutionThreadBuilder(PlanExecution planExecution)
        {
            _planExecution = planExecution;
        }

        public ExecutionThreadBuilder<T> SetId(string id)
        {
            _executionThread.Id = id;
            return this;
        }

        public ExecutionThreadBuilder<T> CreateSteps()
        {
            foreach (var variable in _planExecution.Plan.Variables)
                _executionThread.PlanGlobalVariables.Add(new PlanGlobalVariable
                {
                    Name = variable.Key,
                    Expression = variable.Value
                });

            foreach (var step in _planExecution.Plan.Steps)
            {
                _executionThread.Steps.Add(new HttpRequestStep(_serviceProvider,
                    _planExecution,
                    _executionThread.PlanGlobalVariables,
                    step));
            }

            return this;
        }

        public ExecutionThread Build()
        {
            return _executionThread;
        }
    }
}