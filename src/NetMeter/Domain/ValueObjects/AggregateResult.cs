namespace Domain.ValueObjects
{
    public class AggregateResult
    {
        public int ResponseCode { get; set; }
        public int Number { get; set; }
        public double AverageResponseTime { get; set; }
    }
}
