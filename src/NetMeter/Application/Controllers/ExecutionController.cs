using Application.Services;
using Domain.Abstract;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExecutionController : ControllerBase
    {
        private readonly IGenericRepository<Execution> _executionRepository;
        private readonly ExecutorService _executorService;

        public ExecutionController(
            ExecutorService executorService,
            IGenericRepository<Execution> executionRepository)
        {
            _executionRepository = executionRepository;
            _executorService = executorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Execution execution)
        {
            await _executionRepository.Create(execution);
            await _executorService.SetExecutionPlan(execution.PlanId);
            _executorService.Execution = execution;
            await _executorService.Execute();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _executionRepository.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var execution = await _executionRepository.Get(id);

            if (execution == null)
                return NotFound();

            return Ok(execution);
        }

        [HttpGet("AggregateResult/{id}")]
        public async Task<IActionResult> GetAggregateResult(int id)
        {
            var execution = await _executionRepository.Get(id);

            if (execution == null)
                return NotFound();

            var agregateResults = execution.Results
                .GroupBy(r => r.StatusCode)
                .Select(g => new AggregateResult
                {
                    ResponseCode = g.Key,
                    Number = g.Count(),
                    AverageResponseTime = g.Average(r => r.ResponseTime)
                }).ToList();

            return Ok(agregateResults);
        }
    }
}
