using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using NMeter.Api.Reporting.Data;
using NMeter.Api.Reporting.Extensions;
using NMeter.Api.Reporting.Models;
using NMeter.Api.Reporting.Services;

namespace NMeter.Api.Reporting.Domain
{
    public class ResultDomain : IResultDomain
    {
        private readonly ILogger<ResultDomain> _logger;
        private readonly IDistributedCache _cache;
        private readonly IResultRepository _resultRepository;
        private readonly IConfiguration _configuration;
        private readonly IHashProvider _hashProvider;
        private int _resultsPerPage;

        public ResultDomain(
            ILogger<ResultDomain> logger,
            IDistributedCache cache,
            IResultRepository resultRepository,
            IConfiguration configuration,
            IHashProvider hashProvider)
        {
            _logger = logger;
            _cache = cache;
            _resultRepository = resultRepository;
            _configuration = configuration;
            _hashProvider = hashProvider;

            if (!int.TryParse(_configuration["ResultsPerPage"], out _resultsPerPage))
                _resultsPerPage = 100;
        }

        public async Task<ExecutionResult> GetExecutionAggregatedResult(int executionId, RequestSettings requestSettings)
        {
            try
            {
                var results = (await GetData(executionId, requestSettings))                
                    .AsQueryable()
                    .FilterBy(
                        requestSettings.FilterBy.filterPtoperty, 
                        requestSettings.FilterBy.filterValue)
                    .SortBy(
                        requestSettings.SortBy.sortProperty,
                        requestSettings.SortBy.desc
                    );

                var totalRequestsAmount = _resultRepository.GetExecutionResultsAmount(executionId);
                var successAmount = _resultRepository.GetExecutionSuccessAmount(executionId);
                var minResponseTime = _resultRepository.GetMinSuccessResponseTime(executionId);
                var maxResponseTime = _resultRepository.GetMaxSuccessResponseTime(executionId);
                var avgResponseTime = _resultRepository.GetAvgSuccessResponseTime(executionId);

                return new ExecutionResult
                {
                    TotalRequestsAmount = totalRequestsAmount,
                    SuccessAmount = successAmount,
                    SuccessPercentage = (double)successAmount / (double)totalRequestsAmount,
                    MinResponseTime = minResponseTime,
                    MaxResponseTime = maxResponseTime,
                    AvgResponseTime = avgResponseTime,

                    PagedResults = new PagedResults
                    {
                        PageIndex = requestSettings.PageIndex,
                        TotalPages = totalRequestsAmount / _resultsPerPage + 1,
                        Results = results
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new ExecutionResult()
                {
                    PagedResults = new PagedResults()
                    {
                        Results = new List<Result>()
                    }
                };
            }
        }

        private async Task<IEnumerable<Result>> GetData(int executionId, RequestSettings requestSettings)
        {
            var hash = _hashProvider.GenerateHash(new
            {
                ExecutionId = executionId,
                RequestSettings = requestSettings
            });

            var cachedJson = await _cache.GetStringAsync(hash);

            IEnumerable<Result> results;
            if (cachedJson == null)
            {
                results = await _resultRepository.GetExecutionResultsAsync(executionId);

                results = results
                    .Skip(requestSettings.PageIndex * _resultsPerPage)
                    .Take(_resultsPerPage)
                    .ToList();

                var jsonResults = JsonSerializer.Serialize(results);
                await _cache.SetStringAsync(hash, jsonResults);
            }

            else
                results = JsonSerializer.Deserialize<IEnumerable<Result>>(cachedJson) ?? new List<Result>();

            return results;
        }
    }
}