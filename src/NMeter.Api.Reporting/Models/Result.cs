namespace NMeter.Api.Reporting.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int ResponseCode { get; set; }

        public required string ResponseBody { get; set; }

        public required string ResponseHeaders { get; set; }

        public long ResponseTime { get; set; }

        public int StepId { get; set; }

        public int ExecutionId { get; set; }
    }
}