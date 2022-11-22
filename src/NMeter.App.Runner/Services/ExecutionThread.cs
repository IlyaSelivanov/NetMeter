using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public class ExecutionThread
    {
        public string Id { get; set; }

        public ThreadStatus Status { get; set; } = ThreadStatus.Ready;

        public List<IStep> Steps { get; set; } = new List<IStep>();

        public ICollection<PlanGlobalVariable> PlanGlobalVariables { get; set; }

        public async Task Start()
        {
            Status = ThreadStatus.InProgress;

            foreach(var step in Steps)
                await step.Execute();

            Status = ThreadStatus.Completed;
        }
    }

    public enum ThreadStatus
    { 
        Ready = 0,
        InProgress,
        Completed
    }
}