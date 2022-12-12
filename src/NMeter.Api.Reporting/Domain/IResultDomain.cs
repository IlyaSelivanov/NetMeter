using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Domain
{
    public interface IResultDomain
    {
        Task<ExecutionResult> GetExecutionResult(int executionId, RequestSettings requestSettings);
    }
}