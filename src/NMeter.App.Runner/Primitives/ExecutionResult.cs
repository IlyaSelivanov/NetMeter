namespace NMeter.App.Runner.Primitives
{
    public class ExecutionResult
    {
        public int ResponseCode { get; set; }
        
        public string ResponseBody { get; set; }

        public Dictionary<string, string> ResponseHeaders { get; set; } = new Dictionary<string, string>();

        public int ExecutionTime { get; set; }
    }
}