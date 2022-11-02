using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Result
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ResponseCode { get; set; }

        public string ResponseBody { get; set; }

        public string ResponseHeaders { get; set; }

        public int StepId { get; set; }

        [Required]
        public int ExecutionId { get; set; }

        public Execution Execution { get; set; }
    }
}