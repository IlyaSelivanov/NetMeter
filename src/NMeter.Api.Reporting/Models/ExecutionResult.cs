namespace NMeter.Api.Reporting.Models
{
    public class ExecutionResult
    {
        public int TotalRequestsAmount { get; set; }

        public int SuccessAmount { get; set; }

        public int SuccessPercentage { get; set; }

        public long MinResponseTime { get; set; }

        public long MaxResponseTime { get; set; }

        public long AvgResponseTime { get; set; }

        public required PagedResults PagedResults { get; set; }
    }

    public class PagedResults
    {
        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public required IEnumerable<Result> Results { get; set; }
    }
}