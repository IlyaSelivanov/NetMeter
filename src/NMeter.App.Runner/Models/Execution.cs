using System.ComponentModel.DataAnnotations;

namespace NMeter.App.Runner.Models
{
    public class Execution
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ExecutionStatus Status { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }

        public ICollection<Result> Results { get; set; }
    }

    public enum ExecutionStatus
    {
        @New = 0,
        Running, 
        Completed,
        Canceled
    }
}