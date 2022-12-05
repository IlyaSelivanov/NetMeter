using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public abstract class ExecutionThreadBuilder<TThread, TBuilder>
        where TThread : ExecutionThread, new()
        where TBuilder : ExecutionThreadBuilder<TThread, TBuilder>
    {
        protected ExecutionThread _executionThread = new TThread();
        protected readonly PlanExecution _planExecution;
        protected readonly IServiceProvider _serviceProvider;
        protected readonly ILogger<ExecutionThreadBuilder<TThread, TBuilder>> _logger;

        public ExecutionThreadBuilder(IServiceProvider serviceProvider,
            PlanExecution planExecution)
        {
            _serviceProvider = serviceProvider;
            _planExecution = planExecution;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<ExecutionThreadBuilder<TThread, TBuilder>>();

            _executionThread.PlanGlobalVariables = new List<PlanGlobalVariable>();
        }

        public TBuilder SetId(string id)
        {
            _executionThread.Id = id;
            return (TBuilder)this;
        }

        public TBuilder CreateSteps()
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

            return (TBuilder)this;
        }

        public abstract ExecutionThread Build();
    }
}