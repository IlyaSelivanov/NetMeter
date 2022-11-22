namespace NMeter.App.Runner.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BaseUrl { get; set; }

        public Profile Profile { get; set; }

        public ICollection<Execution> Executions { get; set; }

        public ICollection<PlanVariable> Variables { get; set; }

        public ICollection<Step> Steps { get; set; }
    }
}