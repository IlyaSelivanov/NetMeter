using System.ComponentModel.DataAnnotations;

namespace NMeter.App.Runner.Models
{
    public class Result
    {
        public int Id { get; set; }

        public int ResponseCode { get; set; }

        public string ResponseBody { get; set; }

        public string ResponseHeaders { get; set; }

        public int StepId { get; set; }

        public int ExecutionId { get; set; }

        public Execution Execution { get; set; }
    }
}