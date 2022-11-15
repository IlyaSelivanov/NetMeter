using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Interfaces
{
    public interface IStep
    {
        Task Execute();
    }
}