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
                var executionThread = CrateExecutionThread(i);
                _executionInstance.ExecutionThreads.Add(executionThread.Id, executionThread);
            }

            return this;
        }

        public ExecutionInstance Build()
        {
            _executionInstance.Id = _planExecution.Execution.Id.ToString();
            return _executionInstance;
        }

        private ExecutionThread CrateExecutionThread(int i)
        {
            var executionId = _planExecution.Execution.Id;
            var duration = _planExecution.Plan.Profile.Duration;

            if (duration == 0)
            {
                return new SingleExecutionThreadBuilder(_serviceProvider, _planExecution)
                        .SetId($"execution-{executionId}-thread-{i}")
                        .CreateSteps()
                        .Build();
            }
            else
            {
                return new TimedExecutionThreadBuilder(_serviceProvider, _planExecution)
                        .SetDuration(duration)
                        .SetId($"execution-{executionId}-thread-{i}")
                        .CreateSteps()
                        .Build();
            }
        }
    }
}