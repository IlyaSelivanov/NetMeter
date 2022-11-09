namespace NMeter.App.Runner.Models
{
    public class Header
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int StepId { get; set; }

        public Step Step { get; set; }
    }
}