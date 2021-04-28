using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Execution
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Status { get; set; } = 0;

        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public List<Result> Results { get; set; } = new List<Result>();

    }
}
