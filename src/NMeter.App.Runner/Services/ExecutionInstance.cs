namespace NMeter.App.Runner.Services
{
    public class ExecutionInstance
    {
        public string Id { get; set; }

        public Dictionary<string, ExecutionThread> ExecutionThreads { get; set; } = new Dictionary<string, ExecutionThread>();

        public Task RunExecution()
        {
            foreach(var thread in ExecutionThreads)
                Task.Run(() => thread.Value.Start());
            
            return Task.CompletedTask;
        }
    }
}