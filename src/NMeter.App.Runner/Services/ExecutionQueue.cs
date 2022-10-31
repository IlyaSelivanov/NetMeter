using System.Threading.Channels;
using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Services
{
    public class ExecutionQueue : IExecutionQueue
    {
        private readonly Channel<PlanExecution> _queue;

        public ExecutionQueue(int capacity)
        {
            var options = new BoundedChannelOptions(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<PlanExecution>(options);
        }

        public async ValueTask<PlanExecution> DequeueBackgroundItemAsync()
        {
            return await _queue.Reader.ReadAsync();
        }

        public async ValueTask QueueBackgroundItemAsync(PlanExecution item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            await _queue.Writer.WriteAsync(item);
        }
    }
}