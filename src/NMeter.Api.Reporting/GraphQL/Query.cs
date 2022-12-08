using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.GraphQL
{
    public class Query
    {
        public IQueryable<Result> GetResult([Service] AppDbContext context)
        {
            return context.Results;
        }
    }
}