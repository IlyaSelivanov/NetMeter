namespace NMeter.App.Runner.Models
{
    public class Step
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public string Path { get; set; } = string.Empty;

        public Method Method { get; set; }

        public string Body { get; set; } = string.Empty;

        public int PlanId { get; set; }

        public Plan Plan { get; set; }

        public ICollection<Header> Headers { get; set; } = new List<Header>();

        public ICollection<UrlParameter> Parameters { get; set; } = new List<UrlParameter>();
    }

    public enum Method
    {
        GET = 0,
        POST,
        PUT,
        DELETE
    }
}