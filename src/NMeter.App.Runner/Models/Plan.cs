namespace NMeter.App.Runner.Models
{
    public class Plan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BaseUrl { get; set; }

        public Profile Profile { get; set; }

        public ICollection<Execution> Executions { get; set; } = new List<Execution>();

        public ICollection<PlanVariable> Variables { get; set; } = new List<PlanVariable>();

        public ICollection<Step> Steps { get; set; } = new List<Step>();
    }
}