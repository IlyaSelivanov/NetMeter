using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Execution
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public ExecutionStatus Status { get; set; }

        [Required]
        public int planId { get; set; }

        public Plan Plan { get; set; }
    }
}