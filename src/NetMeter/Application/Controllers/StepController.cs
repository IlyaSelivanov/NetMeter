using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StepController : ControllerBase
    {
        private readonly IGenericRepository<Step> _repository;

        public StepController(IGenericRepository<Step> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var step = await _repository.Get(id);

            if (step == null)
                return NotFound();

            return Ok(step);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Step step)
        {
            await _repository.Create(step);
            return Ok(step.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Step step)
        {
            await _repository.Update(step);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}
