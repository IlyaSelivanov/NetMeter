namespace NMeter.Api.Settings.Models
{
    public class PlanExecution
    {
        public Execution Execution { get; set; }
        public Plan Plan { get; set; }
        public string Event { get; set; }
    }
}