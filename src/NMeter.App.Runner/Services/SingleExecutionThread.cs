namespace NMeter.App.Runner.Services
{
    public class SingleExecutionThread : ExecutionThread
    {
        public override async Task Start()
        {
            Status = ThreadStatus.InProgress;

            foreach (var step in Steps.OrderBy(s => s.Order))
                await step.Execute();

            Status = ThreadStatus.Completed;
        }
    }
}