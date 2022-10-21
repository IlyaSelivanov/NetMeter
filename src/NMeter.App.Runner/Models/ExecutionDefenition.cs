namespace NMeter.App.Runner.Models
{
    public class ExecutionDefenition
    {
        public string Id { get; set; }

        public Dictionary<int, ExecutionStep> Steps { get; set; } = new Dictionary<int, ExecutionStep>();

        public Type DataType { get; set; }
    }
}