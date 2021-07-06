using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EndpointUrl { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Users number invalid (1-100).")]
        public int UsersNumber { get; set; } = 1;
        public bool IsLooped { get; set; } = false;
        public int Duration { get; set; } = 0;
        public string UserId { get; set; }

        public List<Step> Steps { get; set; } = new List<Step>();
        public List<Execution> Executions { get; set; } = new List<Execution>();
    }
}
