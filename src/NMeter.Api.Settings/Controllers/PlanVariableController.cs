using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/[controller]")]
    public class PlanVariableController : ControllerBase
    {
        private readonly IPlanVariableRepo _planVariableRepo;

        public PlanVariableController(IPlanVariableRepo planVariableRepo)
        {
            _planVariableRepo = planVariableRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanVariable>>> GetAllPlanVariables(int planId)
        {
            return Ok(await _planVariableRepo.GetAllPlanVariablesAsync(planId));
        }

        [HttpGet("{variableId:int}", Name = "GetPlanVariable")]
        public async Task<ActionResult<PlanVariable>> GetPlanVariable(int planId, int variableId)
        {
            return Ok(await _planVariableRepo.GetPlanVariableAsync(planId, variableId));
        }

        [HttpPost]
        public async Task<ActionResult<PlanVariable>> CreatePlanVariable(int planId, PlanVariable planVariable)
        {
            if (await _planVariableRepo.CreatePlanVariableAsync(planId, planVariable))
                return CreatedAtRoute(nameof(GetPlanVariable),
                    new { planId = planId, variableId = planVariable.Id },
                    planVariable);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<PlanVariable>> UpdatePlanVariable(int planId, PlanVariable planVariable)
        {
            if (await _planVariableRepo.UpdatePlanVariableAsync(planId, planVariable))
                return CreatedAtRoute(nameof(GetPlanVariable),
                    new { planId = planId, variableId = planVariable.Id },
                    planVariable);

            return NoContent();
        }

        [HttpDelete("{variableId:int}")]
        public async Task<ActionResult> DeletePlanVariable(int planId, int variableId)
        {
            await _planVariableRepo.DeletePlanVariableAsync(planId, variableId);

            return NoContent();
        }
    }
}