namespace NMeter.App.Runner.Services
{
    public class ExecutionInstance
    {
        public string Id { get; set; }

        public Dictionary<string, ExecutionThread> ExecutionThreads { get; set; } = new Dictionary<string, ExecutionThread>();

        public async Task RunExecution()
        {
            await Task.Run(async () =>
            {
                List<Task> tasks = new List<Task>();

                foreach (var thread in ExecutionThreads)
                    tasks.Add(thread.Value.Start());

                await Task.WhenAll(tasks.ToArray());
            });
        }
    }
}