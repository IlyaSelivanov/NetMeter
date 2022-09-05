using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Plan.Models
{
    public class UrlParameter
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public KeyValue Value { get; set; }

        [Required]
        public int StepId { get; set; }

        public Step Step { get; set; }
    }
}