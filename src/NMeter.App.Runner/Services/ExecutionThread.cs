using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public abstract class ExecutionThread
    {
        public string Id { get; set; }

        public ThreadStatus Status { get; set; } = ThreadStatus.Ready;

        public List<IStep> Steps { get; set; } = new List<IStep>();

        public ICollection<PlanGlobalVariable> PlanGlobalVariables { get; set; }

        public abstract Task Start();
    }

    public enum ThreadStatus
    { 
        Ready = 0,
        InProgress,
        Completed
    }
}