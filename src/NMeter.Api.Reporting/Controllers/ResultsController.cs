using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Reporting.Domain;
using NMeter.Api.Reporting.Models;

namespace NMeter.Api.Reporting.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ILogger<ResultsController> _logger;
        private readonly IResultDomain _resultDomain;

        public ResultsController(
            ILogger<ResultsController> logger,
            IResultDomain resultDomain)
        {
            _logger = logger;
            _resultDomain = resultDomain;
        }

        [HttpGet("{executionId:int}")]
        public async Task<ActionResult<ExecutionResult>> GetExecutionAggregatedResult(int executionId, RequestSettings requestSettings)
        {
            var aggregatedResult = await _resultDomain.GetExecutionAggregatedResult(executionId, requestSettings);

            return Ok(aggregatedResult);
        }
    }
}