namespace NMeter.App.Runner.Models
{
    public class ExecutionThread
    {
        public int Id { get; set; }
        
        public Dictionary<int, ExecutionThreadStep> Steps { get; set; } = new Dictionary<int, ExecutionThreadStep>();
    }
}