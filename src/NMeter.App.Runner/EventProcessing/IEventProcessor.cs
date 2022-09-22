namespace NMeter.App.Runner.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}