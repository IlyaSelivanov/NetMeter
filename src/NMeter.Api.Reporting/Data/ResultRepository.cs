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
            return _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime)
                .Max();
        }

        public long GetMinSuccessResponseTime(int executionId)
        {
            return _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime)
                .Min();
        }

        public double GetAvgSuccessResponseTime(int executionId)
        {
            return _context.Results
                .Where(r => r.ExecutionId == executionId && r.ResponseCode == 200)
                .Select(r => r.ResponseTime)
                .Average();
        }
    }
}