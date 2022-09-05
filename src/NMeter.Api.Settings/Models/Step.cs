using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Step
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue,
            ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Order { get; set; }

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