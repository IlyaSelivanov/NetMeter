namespace NMeter.App.Runner.Models
{
    public class PlanVariable
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}