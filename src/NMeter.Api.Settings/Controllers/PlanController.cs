using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepo _planRepo;

        public PlanController(IPlanRepo planRepo)
        {
            _planRepo = planRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plan>>> GetPlansAsync()
        {
            var plans = await _planRepo.GetPlansAsync();

            return Ok(plans);
        }
    }
}