using Application.Repository;
using Application.Services;
using Domain.Entities;
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

        // POST api/<ExecutionController>
        [HttpPost]
        public async Task<ActionResult> Post(Execution execution)
        {
            await _executionRepository.CreateExecution(execution);
            await _executorService.SetExecutionPlan(execution.PlanId);
            _executorService.Execution = execution;
            _executorService.CreateUsers();
            await _executorService.Execute();

            return NoContent();
        }
    }
}
