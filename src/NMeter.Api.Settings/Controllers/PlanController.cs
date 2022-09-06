using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IRepository<Plan> _planRepo;

        public PlanController(IRepository<Plan> planRepo)
        {
            _planRepo = planRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlansAsync()
        {
            var plans = await _planRepo.GetAllAsync();

            return Ok(plans);
        }

        [HttpGet("{planId:int}", Name = "GetPlanByIdAsync")]
        public async Task<ActionResult<Plan>> GetPlanByIdAsync(int planId)
        {
            var plan = await _planRepo.GetByIdAsync(planId);

            return Ok(plan);
        }

        [HttpPost]
        public async Task<ActionResult<Plan>> Create(Plan plan)
        {
            await _planRepo.CreateAsync(plan);

            return CreatedAtRoute(nameof(GetPlanByIdAsync), new { planId = plan.Id }, plan);
        }

        [HttpDelete("{planId:int}")]
        public async Task<ActionResult> Delete(int planId)
        {
            await _planRepo.DeleteAsync(planId);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Plan>> Update(Plan plan)
        {
            if(await _planRepo.UpdateAsync(plan))
                return CreatedAtRoute(nameof(GetPlanByIdAsync), new { planId = plan.Id }, plan);

            return NoContent();
        }
    }
}