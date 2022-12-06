using System.Timers;

namespace NMeter.App.Runner.Services
{
    public class TimedExecutionThread : ExecutionThread
    {
        private bool _isElapsed = false;

        public int Duration { get; set; }

        public override async Task Start()
        {
            Status = ThreadStatus.InProgress;

            var interval = (double)(TimeSpan.FromSeconds(Duration).TotalMilliseconds);
            var timer = new System.Timers.Timer(interval);
            timer.Elapsed += Timer_Elapsed;

            timer.Start();

            while (!_isElapsed)
            {
                foreach (var step in Steps.OrderBy(s => s.Order))
                    await step.Execute();
            }

            timer.Stop();
            timer.Dispose();
            
            Status = ThreadStatus.Completed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _isElapsed = true;
        }
    }
}