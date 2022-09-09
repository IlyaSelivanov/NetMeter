using Microsoft.AspNetCore.Mvc;
using NMeter.Api.Settings.Data;
using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Controllers
{
    [ApiController]
    [Route("api/v1/plan/{planId:int}/step/{stepId:int}/[controller]")]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderRepo _headerRepo;

        public HeaderController(IHeaderRepo headerRepo)
        {
            _headerRepo = headerRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Header>>> GetAllStepHeaders(int stepId)
        {
            return Ok(await _headerRepo.GetAllStepHeadersAsync(stepId));
        }

        [HttpGet("{headerId:int}", Name = nameof(GetStepHeader))]
        public async Task<ActionResult<Header>> GetStepHeader(int stepId, int headerId)
        {
            return Ok(await _headerRepo.GetStepHeaderAsync(stepId, headerId));
        }

        [HttpPost]
        public async Task<ActionResult<Header>> CreateStepHeader(int stepId, Header header)
        {
            if (await _headerRepo.CreateStepHeaderAsync(stepId, header))
                return CreatedAtRoute(nameof(GetStepHeader),
                    new { stepId = stepId, headerId = header.Id },
                    header);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<Header>> UpdateStepHeader(int stepId, Header header)
        {
            if (await _headerRepo.UpdateStepHeaderAsync(stepId, header))
                return CreatedAtRoute(nameof(GetStepHeader),
                    new { stepId = stepId, headerId = header.Id },
                    header);

            return NoContent();
        }

        [HttpDelete("{headerId:int}")]
        public async Task<ActionResult> DeleteStepHeader(int stepId, int headerId)
        {
            await _headerRepo.DeleteStepHeaderAsync(stepId, headerId);

            return NoContent();
        }
    }
}