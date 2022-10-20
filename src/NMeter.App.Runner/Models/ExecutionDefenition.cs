namespace NMeter.App.Runner.Models
{
    public class ExecutionDefenition
    {
        public string Id { get; set; }
        
        public Dictionary<int, ExecutionThread> Threads {get; set;} = new Dictionary<int, ExecutionThread>();
    }
}