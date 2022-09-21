using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/[controller]")]
    public class ExecutionController : ControllerBase
    {
        private readonly IExecutionRepo _executionRepo;

        public ExecutionController(IExecutionRepo executionRepo)
        {
            _executionRepo = executionRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Execution>>> GetAllPlanExecutions(int planId)
        {
            return Ok(await _executionRepo.GetAllPlanExecutionsAsync(planId));
        }

        [HttpGet("{executionId:int}", Name = nameof(GetPlanExecution))]
        public async Task<ActionResult<Execution>> GetPlanExecution(int planId, int executionId)
        {
            return Ok(await _executionRepo.GetPlanExecutionAsync(planId, executionId));
        }

        [HttpPost]
        public async Task<ActionResult<Execution>> CreatePlanExecution(int planId, Execution execution)
        {
            if(await _executionRepo.CreatePlanExecutionAsync(planId, execution))
                return CreatedAtRoute(nameof(GetPlanExecution),
                    new {planId = planId, executionId = execution.Id},
                    execution);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Execution>> UpdatePlanExecution(int planId, Execution execution)
        {
            if(await _executionRepo.UpdatePlanExecutionAsync(planId, execution))
                return CreatedAtRoute(nameof(GetPlanExecution),
                    new {planId = planId, executionId = execution.Id},
                    execution);

            return NoContent();
        }

        [HttpDelete("executionId:int")]
        public async Task<ActionResult> DeletePlanExecution(int planId, int executionId)
        {
            await _executionRepo.DeletePlanExecutionAsync(planId, executionId);

            return NoContent();
        }
    }
}