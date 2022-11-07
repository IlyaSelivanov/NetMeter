using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionBuilder
    {
        private ExecutionInstance _executionInstance = new ExecutionInstance();

        private readonly PlanExecution _planExecution;

        public ExecutionBuilder(PlanExecution planExecution)
        {
            _planExecution = planExecution;
        }

        public ExecutionBuilder CreateThreads()
        {
            var executionId = _planExecution.Execution.Id;
            var usersNumber = _planExecution.Plan.Profile.UsersNumber <= 0
                ? 1
                : _planExecution.Plan.Profile.UsersNumber;

            for (int i = 0; i < usersNumber; i++)
            {
                var executionThreadBuilder = new ExecutionThreadBuilder(_planExecution.Plan.Steps);
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
            return _executionInstance;
        }
    }
}