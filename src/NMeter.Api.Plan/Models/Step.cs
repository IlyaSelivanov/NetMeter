using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Plan.Models
{
    public class Step
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public Method Method { get; set; }

        public string Body { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<UrlParameter> Parameters { get; set; }
    }
}