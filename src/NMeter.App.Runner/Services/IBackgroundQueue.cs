namespace NMeter.App.Runner.Services
{
    public interface IBackgroundQueue
    {
        ValueTask QueueBackgroundItemAsync(IExecutableItem item);
        ValueTask<IExecutableItem> DequeueBackgroundItemAsync();
    }
}