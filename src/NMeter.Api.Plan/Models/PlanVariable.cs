using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Plan.Models
{
    public class PlanVariable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public KeyValue Variable { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}