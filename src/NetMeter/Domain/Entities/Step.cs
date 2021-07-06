using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }
        [Required]
        public string Resource { get; set; }
        [Required]
        public int Method { get; set; }
        public string Parameters { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public string Variables { get; set; }
        public string UserId { get; set; }

        public int PlanId { get; set; }
        public Plan Plan { get; set; }
    }
}
