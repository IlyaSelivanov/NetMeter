using NMeter.App.Runner.Interface;

namespace NMeter.App.Runner.Models
{
    public class ExecutionContext : IExecutionStepContext
    {
        public object Data { get; set; }
    }
}