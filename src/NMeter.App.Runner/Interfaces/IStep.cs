using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Interfaces
{
    public interface IStep
    {
        public int Order { get; set; }

        Task Execute();
    }
}