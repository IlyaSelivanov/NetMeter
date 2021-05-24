using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IGenericRepository<Step> _repository;

        public StepController(IGenericRepository<Step> repository)
        {
            _repository = repository;
        }

        // GET: api/<StepController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.Get());
        }

        // GET api/<StepController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var step = await _repository.Get(id);

            if (step == null)
                return NotFound();

            return Ok(step);
        }

        // POST api/<StepController>
        [HttpPost]
        public async Task<IActionResult> Post(Step step)
        {
            await _repository.Create(step);
            return Ok(step.Id);
        }

        // PUT api/<StepController>
        [HttpPut]
        public async Task<IActionResult> Put(Step step)
        {
            await _repository.Update(step);
            return NoContent();
        }

        // DELETE api/<StepController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}
