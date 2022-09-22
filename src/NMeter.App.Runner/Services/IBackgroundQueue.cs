using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public interface IBackgroundQueue
    {
        ValueTask QueueBackgroundItemAsync(PlanExecution item);
        ValueTask<PlanExecution> DequeueBackgroundItemAsync();
    }
}