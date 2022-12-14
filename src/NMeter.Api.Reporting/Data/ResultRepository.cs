using Microsoft.EntityFrameworkCore;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Data
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _context;

        public ResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetExecutionResultsAmount(int executionId)
        {
            return _context.Results
                .Where(r => r.ExecutionId == executionId)
                .Count();
        }

        public async Task<IQueryable<Result>> GetExecutionResultsAsync(int executionId)
        {
            var results = await _context.Results
                .Where(r => r.ExecutionId == executionId)
                .ToListAsync();

            return results.AsQueryable();
        }

        public int GetExecutionSuccessAmount(int executionId)
        {
            return _context.Results
                .Where(r => r.ExecutionId == executionId
                    && r.ResponseCode == 200)
                .Count();
        }

        public long GetMaxSuccessResponseTime(int executionId)
        {
            var results = _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime);
            
            if(results.Any())
                return results.Max();

            return 0L;
        }

        public long GetMinSuccessResponseTime(int executionId)
        {
            var results = _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime);
            
            if(results.Any())
                return results.Min();
            
            return 0L;
        }

        public double GetAvgSuccessResponseTime(int executionId)
        {
            var results = _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime);
            
            if(results.Any())
                return results.Average();
            
            return 0L;
        }
    }
}