namespace NMeter.App.Runner.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public int UsersNumber { get; set; }

        public int Duration { get; set; }

        public int PlanId { get; set; }

        public Plan Plan { get; set; }
    }
}