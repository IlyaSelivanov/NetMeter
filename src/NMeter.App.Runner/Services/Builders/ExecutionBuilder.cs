using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionBuilder
    {
        private ExecutionInstance _executionInstance = new ExecutionInstance();
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ExecutionBuilder> _logger;
        private readonly PlanExecution _planExecution;

        public ExecutionBuilder(IServiceProvider serviceProvider,
            PlanExecution planExecution)
        {
            _serviceProvider = serviceProvider;
            _planExecution = planExecution;
            _logger = LoggerFactory
                .Create(configure => configure.AddConsole())
                .CreateLogger<ExecutionBuilder>();
        }

        public ExecutionBuilder CreateThreads()
        {
            var executionId = _planExecution.Execution.Id;
            var usersNumber = _planExecution.Plan.Profile.UsersNumber <= 0
                ? 1
                : _planExecution.Plan.Profile.UsersNumber;

            for (int i = 0; i < usersNumber; i++)
            {
                var executionThreadBuilder = new ExecutionThreadBuilder<SingleExecutionThread>(_serviceProvider,
                    _planExecution);
                var executionThread = executionThreadBuilder
                    .SetId($"execution-{executionId}-thread-{i}")
                    .CreateSteps()
                    .Build();
                    
                _executionInstance.ExecutionThreads.Add(executionThread.Id, executionThread);
            }

            return this;
        }

        public ExecutionInstance Build()
        {
            _executionInstance.Id = _planExecution.Execution.Id.ToString();
            return _executionInstance;
        }
    }
}