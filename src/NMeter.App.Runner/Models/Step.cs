namespace NMeter.App.Runner.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Path { get; set; }

        public int Method { get; set; }

        public string Body { get; set; }
    }
}