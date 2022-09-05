using System.ComponentModel.DataAnnotations;

namespace NMeter.Api.Settings.Models
{
    public class Profile
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public int UsersNumber { get; set; }

        public int Duration { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}