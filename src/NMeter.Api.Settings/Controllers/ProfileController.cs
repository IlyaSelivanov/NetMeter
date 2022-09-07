using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepo _profileRepo;

        public ProfileController(IProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }

        [HttpGet(Name = "GetPlanProfile")]
        public async Task<ActionResult<Profile>> GetPlanProfile(int planId)
        {
            var profile = await _profileRepo.GetPlanProfileAsync(planId);

            return Ok(profile);
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> CreatePlanProfile(int planId, Profile profile)
        {
            if(await _profileRepo.CreatePlanProfileAsync(planId, profile))
                return CreatedAtRoute(nameof(GetPlanProfile), new { planId = planId }, profile);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Profile>> UpdatePlanProfile(int planId, Profile profile)
        {
            if(await _profileRepo.UpdatePlanProfileAsync(planId, profile))
                return CreatedAtRoute(nameof(GetPlanProfile), new { planId = planId }, profile);
            
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePlanProfile(int planId)
        {
            await _profileRepo.DeletePlanProfileAsync(planId);

            return NoContent();
        }
    }
}