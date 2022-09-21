using System.Threading.Channels;

namespace NMeter.App.Runner.Services
{
    public class BackgroundQueue : IBackgroundQueue
    {
        private readonly Channel<IExecutableItem> _queue;

        public BackgroundQueue(int capacity)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<IExecutableItem>(options);
        }

        public async ValueTask<IExecutableItem> DequeueBackgroundItemAsync()
        {
            return await _queue.Reader.ReadAsync();
        }

        public async ValueTask QueueBackgroundItemAsync(IExecutableItem item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            await _queue.Writer.WriteAsync(item);
        }
    }
}