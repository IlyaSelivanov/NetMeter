using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/[controller]")]
    public class StepController : ControllerBase
    {
        private readonly IStepRepo _stepRepo;

        public StepController(IStepRepo stepRepo)
        {
            _stepRepo = stepRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Step>>> GetAllPlanSteps(int planId)
        {
            return Ok(await _stepRepo.GetAllPlanStepsAsync(planId));
        }

        [HttpGet("{stepId:int}", Name = nameof(GetPlanStep))]
        public async Task<ActionResult<Step>> GetPlanStep(int planId, int stepId)
        {
            return Ok(await _stepRepo.GetPlanStepAsync(planId, stepId));
        }

        [HttpPost]
        public async Task<ActionResult<Step>> CreatePlanStep(int planId, Step step)
        {
            if (await _stepRepo.CreatePlanStepAync(planId, step))
                return CreatedAtRoute(nameof(GetPlanStep),
                    new { planId = planId, stepId = step.Id },
                    step);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Step>> UpdatePlanStep(int planId, Step step)
        {
            if (await _stepRepo.UpdatePlanStepAsync(planId, step))
                return CreatedAtRoute(nameof(GetPlanStep),
                    new { planId = planId, stepId = step.Id },
                    step);

            return NoContent();
        }

        [HttpDelete("{stepId:int}")]
        public async Task<ActionResult> DeletePlanStep(int planId, int stepId)
        {
            await _stepRepo.DeletePlanStepAsync(planId, stepId);

            return NoContent();
        }
    }
}