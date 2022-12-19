using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Tests.Data
{
    public static class DatabaseManager
    {
        public static void SeedData(AppDbContext context)
        {
            if(context.Results.Any())
                return;
                
            context.Results.AddRange(
                //ExecutionId == 1
                new Result
                {
                    ResponseCode = 200,
                    ResponseBody = "",
                    ResponseHeaders = "",
                    ResponseTime = 100L,
                    ExecutionId = 1
                },
                new Result
                {
                    ResponseCode = 200,
                    ResponseBody = "",
                    ResponseHeaders = "",
                    ResponseTime = 200L,
                    ExecutionId = 1
                },
                new Result
                {
                    ResponseCode = 500,
                    ResponseBody = "",
                    ResponseHeaders = "",
                    ResponseTime = 200L,
                    ExecutionId = 1
                },
                //ExecutionId == 3
                new Result
                {
                    ResponseCode = 403,
                    ResponseBody = "B",
                    ResponseHeaders = "",
                    ResponseTime = 100L,
                    ExecutionId = 3
                },
                new Result
                {
                    ResponseCode = 200,
                    ResponseBody = "A",
                    ResponseHeaders = "",
                    ResponseTime = 200L,
                    ExecutionId = 3
                },
                new Result
                {
                    ResponseCode = 500,
                    ResponseBody = "C",
                    ResponseHeaders = "",
                    ResponseTime = 200L,
                    ExecutionId = 3
                }
            );

            context.SaveChanges();
        }
    }
}