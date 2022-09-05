using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Plan
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BaseUrl { get; set; }

        public Profile Profile { get; set; }

        public ICollection<PlanVariable> Variables { get; set; }

        public ICollection<Step> Steps { get; set; }
    }
}