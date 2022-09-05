using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class PlanVariable
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}