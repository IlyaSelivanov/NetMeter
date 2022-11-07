using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interfaces
{
    public interface IExecutionQueue
    {
        ValueTask QueueBackgroundItemAsync(PlanExecution item);
        ValueTask<PlanExecution> DequeueBackgroundItemAsync();
    }
}