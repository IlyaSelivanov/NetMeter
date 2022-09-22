using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishPlanExecution(PlanExecution planExecution);
    }
}