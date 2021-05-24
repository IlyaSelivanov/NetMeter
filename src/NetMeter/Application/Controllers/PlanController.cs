using Domain.Entities;
using Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        //private readonly IGenericRepository<Plan> _repository;
        private readonly IPlanRepository _repository;

        public PlanController(IPlanRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<PlanController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.GetPlans());
        }

        // GET api/<PlanController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var plan = await _repository.GetPlanById(id);

            if (plan == null)
                return NotFound();

            return Ok(plan);
        }

        // POST api/<PlanController>
        [HttpPost]
        public async Task<IActionResult> Post(Plan plan)
        {
            await _repository.CreatePlan(plan);
            return Ok(plan.Id);
        }

        // PUT api/<PlanController>
        [HttpPut]
        public async Task<IActionResult> Put(Plan plan)
        {
            await _repository.UpdatePlan(plan);
            return NoContent();
        }

        // DELETE api/<PlanController>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeletePlan(id);
            return NoContent();
        }
    }
}
