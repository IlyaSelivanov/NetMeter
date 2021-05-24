using Application.Services;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            _executorService.CreateUsers();
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
    }
}
