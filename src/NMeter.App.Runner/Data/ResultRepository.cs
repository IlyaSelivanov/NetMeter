using NMeter.App.Runner.Models;

namespace NMeter.App.Runner.Data
{
    public class ResultRepository : IResultRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ResultRepository> _logger;

        public ResultRepository(
            AppDbContext context,
            ILogger<ResultRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveResultAsync(Result result)
        {
            if(result != null)
            {
                _logger.LogDebug("Saving result to db");

                _context.Results.Add(result);
                await _context.SaveChangesAsync();

                _logger.LogDebug("Execution result saved to db");
            }
        }
    }
}