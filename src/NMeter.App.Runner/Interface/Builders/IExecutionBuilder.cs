using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Interface
{
    public interface IExecutionBuilder
    {
        List<ExecutionThread> Threads { get; }

        void AddTread(ExecutionThread thread);
    }

    public interface IExecutionBuilder<TData> : IExecutionBuilder
    {
        IExecutionThreadBuilder<TData> CreateThread();
    }
}