using Application.Services;
using Domain.Entities;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionController : ControllerBase
    {
        private readonly IExecutionRepository _executionRepository;
        private readonly ExecutorService _executorService;

        public ExecutionController(
            ExecutorService executorService,
            IExecutionRepository executionRepository)
        {
            _executionRepository = executionRepository;
            _executorService = executorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Execution execution)
        {
            await _executionRepository.CreateExecution(execution);
            await _executorService.SetExecutionPlan(execution.PlanId);
            _executorService.Execution = execution;
            _executorService.CreateUsers();
            await _executorService.Execute();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _executionRepository.GetExecutions());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var execution = await _executionRepository.GetExecutionById(id);

            if (execution == null)
                return NotFound();

            return Ok(execution);
        }
    }
}
