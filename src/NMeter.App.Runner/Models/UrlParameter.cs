namespace NMeter.App.Runner.Models
{
    public class UrlParameter
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int StepId { get; set; }

        public Step Step { get; set; }
    }
}