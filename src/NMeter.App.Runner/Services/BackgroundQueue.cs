using System.Threading.Channels;

namespace NMeter.App.Runner.Services
{
    public class BackgroundQueue : IBackgroundQueue
    {
        private readonly Channel<HttpRequestItem> _queue;

        public BackgroundQueue(int capacity)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<HttpRequestItem>(options);
        }

        public async ValueTask<HttpRequestItem> DequeueBackgroundItemAsync()
        {
            return await _queue.Reader.ReadAsync();
        }

        public async ValueTask QueueBackgroundItemAsync(HttpRequestItem item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            await _queue.Writer.WriteAsync(item);
        }
    }
}