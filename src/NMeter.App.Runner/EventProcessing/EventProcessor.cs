using System.Text.Json;
using System.Text.Json.Serialization;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Services;

namespace NMeter.App.Runner.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly ILogger<EventProcessor> _logger;
        private readonly IBackgroundQueue _backgroundQueue;
        private const string PLAN_EXECUTION_PUBLISHED = "PlanExecution_Published";

        public EventProcessor(ILogger<EventProcessor> logger,
            IBackgroundQueue backgroundQueue)
        {
            _logger = logger;
            _backgroundQueue = backgroundQueue;
        }

        public async Task ProcessEvent(string message)
        {
            _logger.LogInformation($"--> Processing bus event...");

            var @event = JsonSerializer.Deserialize<BusEvent>(message,
                options: new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

            var eventType = GetEventType(@event);
            switch (eventType)
            {
                case EventType.PlanExecutionPublished:
                    await ProceedPlanExecution(message);
                    break;
                default:
                    break;
            }
        }

        private async Task ProceedPlanExecution(string message)
        {
            _logger.LogInformation($"--> Adding plan execution to a queue.");

            var planExecution = JsonSerializer.Deserialize<PlanExecution>(message,
                options: new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });

            await _backgroundQueue.QueueBackgroundItemAsync(planExecution);
        }

        private EventType GetEventType(BusEvent @event)
        {
            _logger.LogInformation($"--> Determining event type.");

            switch (@event.Event)
            {
                case PLAN_EXECUTION_PUBLISHED:
                    return EventType.PlanExecutionPublished;
                default:
                    return EventType.Undetermined;
            }
        }
    }

    public enum EventType
    {
        Undetermined = -1,
        PlanExecutionPublished
    }
}