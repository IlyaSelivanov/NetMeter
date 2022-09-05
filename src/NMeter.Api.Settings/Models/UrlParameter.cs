using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class UrlParameter
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int StepId { get; set; }

        public Step Step { get; set; }
    }
}