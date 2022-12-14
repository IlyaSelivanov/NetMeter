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
                }
            );

            context.SaveChanges();
        }
    }
}