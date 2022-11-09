using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Tests.Data
{
    public static class PlanExecutionData
    {
        public static PlanExecution GetData()
        {
            return new PlanExecution
            {
                Plan = new Plan
                {
                    Id = 1,
                    Name = "Test Plan #1",
                    BaseUrl = @"https://www.google.com"
                },
                Execution = new Execution
                {

                }
            };
        }
    }
}