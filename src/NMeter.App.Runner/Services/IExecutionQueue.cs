using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public interface IExecutionQueue
    {
        ValueTask QueueBackgroundItemAsync(PlanExecution item);
        ValueTask<PlanExecution> DequeueBackgroundItemAsync();
    }
}