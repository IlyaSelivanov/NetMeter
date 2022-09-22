namespace NMeter.App.Runner.Services
{
    public interface IExecutableItem
    {
        Task ExecuteAsync();
        void Execute();
    }
}