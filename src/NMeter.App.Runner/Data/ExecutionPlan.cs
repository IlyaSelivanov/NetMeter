namespace NMeter.App.Runner.Data
{
    public class ExecutionPlan
    {
        public int ExecutionId { get; set; }

        public int PlanId { get; set; }

        public int UsersNumber { get; set; }

        public int Duration { get; set; }

        public SortedList<int, Func<CancellationToken, ValueTask>> Steps { get; set; } = new SortedList<int, Func<CancellationToken, ValueTask>>();
    }
}