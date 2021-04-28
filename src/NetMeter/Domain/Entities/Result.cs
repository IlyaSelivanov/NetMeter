namespace Domain.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public string RequestResource { get; set; }
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
        public long ResponseTime { get; set; }

        public int ExecutionId { get; set; }
        public Execution Execution { get; set; }
    }
}
