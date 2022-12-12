using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Data
{
    public interface IResultRepository
    {
        Task<IQueryable<Result>> GetExecutionResults(int executionId);
    }
}