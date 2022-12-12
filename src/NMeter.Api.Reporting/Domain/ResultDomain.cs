using Microsoft.Extensions.Caching.Distributed;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Domain
{
    public class ResultDomain : IResultDomain
    {
        private readonly IDistributedCache _cache;
        private readonly AppDbContext _context;

        public ResultDomain(IDistributedCache cache, AppDbContext context)
        {
            _cache = cache;
            _context = context;
        }

        public async Task<ExecutionResult> GetExecutionResult(int executionId, RequestSettings requestSettings)
        {
            throw new NotImplementedException();
        }
    }
}