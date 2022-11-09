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
                    BaseUrl = @"https://www.google.com",
                    Profile = new Profile
                    {
                        Id = 1,
                        UsersNumber = 1,
                        Duration = 0,
                        PlanId = 1
                    },
                    Steps = new List<Step>
                    {
                        new Step
                        {
                            Id = 1,
                            Path = @"/",
                            Order = 0,
                            Method = Method.GET,
                            PlanId = 1
                        },
                        new Step
                        {
                            Id = 2,
                            Path = @"/search?q=dotnet+core",
                            Order = 0,
                            Method = Method.GET,
                            PlanId = 1
                        }
                    }
                },
                Execution = new Execution
                {
                    Id = 1,
                    Status = ExecutionStatus.New,
                    PlanId = 1
                }
            };
        }
    }
}