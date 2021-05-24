using Application.Repository;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private readonly IStepRepository _repository;

        public StepController(IStepRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<StepController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetSteps());
        }

        // GET api/<StepController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var step = await _repository.GetStepById(id);

            if (step == null)
                return NotFound();

            return Ok(step);
        }

        // POST api/<StepController>
        [HttpPost]
        public async Task<IActionResult> Post(Step step)
        {
            await _repository.CreateStep(step);
            return Ok(step.Id);
        }

        // PUT api/<StepController>
        [HttpPut]
        public async Task<IActionResult> Put(Step step)
        {
            await _repository.UpdateStep(step);
            return NoContent();
        }

        // DELETE api/<StepController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteStep(id);
            return NoContent();
        }
    }
}
