using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Data
{
    public interface IResultRepository
    {
        Task<IQueryable<Result>> GetExecutionResultsAsync(int executionId);

        int GetExecutionResultsAmount(int excutionId);

        int GetExecutionSuccessAmount(int executionId);

        long GetMinSuccessResponseTime(int executionId);

        long GetMaxSuccessResponseTime(int executionId);

        double GetAvgSuccessResponseTime(int executionId);
    }
}