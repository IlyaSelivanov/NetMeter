namespace NMeter.App.Runner.Services
{
    public interface IBackgroundQueue
    {
        ValueTask QueueBackgroundItemAsync(HttpRequestItem item);
        ValueTask<HttpRequestItem> DequeueBackgroundItemAsync();
    }
}