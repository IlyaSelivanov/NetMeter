using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Header
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