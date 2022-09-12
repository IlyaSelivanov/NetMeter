using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/step/{stepId:int}/[controller]")]
    public class StepParameterController : ControllerBase
    {
        private readonly IStepParameterRepo _stepParameterRepo;

        public StepParameterController(IStepParameterRepo stepParameterRepo)
        {
            _stepParameterRepo = stepParameterRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UrlParameter>>> GetAllStepParameters(int stepId)
        {
            return Ok(await _stepParameterRepo.GetAllStepParametersAsync(stepId));
        }

        [HttpGet("{paramId:int}", Name = nameof(GetStepParameter))]
        public async Task<ActionResult<UrlParameter>> GetStepParameter(int stepId, int paramId)
        {
            return Ok(await _stepParameterRepo.GetStepParameterAsync(stepId, paramId));
        }

        [HttpPost]
        public async Task<ActionResult<UrlParameter>> CreateStepParameter(int planId, int stepId,
            UrlParameter parameter)
        {
            if (await _stepParameterRepo.CreateStepParameterAsync(stepId, parameter))
            {
                return CreatedAtRoute(nameof(GetStepParameter),
                    new { planId = planId, stepId = stepId, paramId = parameter.Id },
                    parameter);
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<UrlParameter>> UpdateStepParameter(int planId, int stepId,
            UrlParameter parameter)
        {
            if (await _stepParameterRepo.UpdateStepParameterAsync(stepId, parameter))
            {
                return CreatedAtRoute(nameof(GetStepParameter),
                    new { planId = planId, stepId = stepId, paramId = parameter.Id },
                    parameter);
            }

            return NoContent();
        }

        [HttpDelete("{paramId:int}")]
        public async Task<ActionResult> DeleteStepParameter(int stepId, int paramId)
        {
            await _stepParameterRepo.DeleteStepParameterAsync(stepId, paramId);

            return NoContent();
        }
    }
}