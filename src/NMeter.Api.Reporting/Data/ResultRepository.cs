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

        public async Task<IQueryable<Result>> GetExecutionResults(int executionId)
        {
            var results = await _context.Results
                .Where(r => r.ExecutionId == executionId)
                .ToListAsync();
                
            return results.AsQueryable();
        }
    }
}