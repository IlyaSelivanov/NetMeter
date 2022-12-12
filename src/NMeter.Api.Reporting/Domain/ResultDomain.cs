using Microsoft.Extensions.Caching.Distributed;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Domain
{
    public class ResultDomain : IResultDomain
    {
        private readonly IDistributedCache _cache;
        private readonly IResultRepository _resultRepository;
        private readonly IConfiguration _configuration;
        private int _resultsPerPage;

        public ResultDomain(
            IDistributedCache cache,
            IResultRepository resultRepository,
            IConfiguration configuration)
        {
            _cache = cache;
            _resultRepository = resultRepository;
            _configuration = configuration;
        }

        public async Task<ExecutionResult> GetExecutionResult(int executionId, RequestSettings requestSettings)
        {
            if (!int.TryParse(_configuration["ResultsPerPage"], out _resultsPerPage))
                _resultsPerPage = 100;

            var results = await _resultRepository.GetExecutionResults(executionId);

            var totalRequestsAmount = results.Count();
            var successAmount = results.Where(r => r.ResponseCode.Equals(200)).Count();
            var minResponseTime = results.MinBy(r => r.ResponseTime)?.ResponseTime ?? 0;
            var maxResponseTime = results.MaxBy(r => r.ResponseTime)?.ResponseTime ?? 0;

            return new ExecutionResult
            {
                TotalRequestsAmount = totalRequestsAmount,
                SuccessAmount = successAmount,
                SuccessPercentage = successAmount / totalRequestsAmount,
                MinResponseTime = minResponseTime,
                MaxResponseTime = maxResponseTime,

                PagedResults = new PagedResults
                {
                    PageIndex = requestSettings.PageIndex,
                    TotalPages = totalRequestsAmount / _resultsPerPage,
                    Results = results
                        .Skip(requestSettings.PageIndex * _resultsPerPage)
                        .Take(_resultsPerPage)
                }
            };
        }
    }
}