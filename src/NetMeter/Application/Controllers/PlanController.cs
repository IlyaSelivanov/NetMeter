using Application.Repository;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<Plan>>> Get()
        {
            return await _repository.GetPlans();
        }

        // GET api/<PlanController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plan>> Get(int id)
        {
            var plan = await _repository.GetPlanById(id);

            if (plan == null)
                return NotFound();

            return plan;
        }

        // POST api/<PlanController>
        [HttpPost]
        public async Task<ActionResult<int>> Post(Plan plan)
        {
            await _repository.CreatePlan(plan);
            return plan.Id;
        }

        // PUT api/<PlanController>
        [HttpPut]
        public async Task<ActionResult> Put(Plan plan)
        {
            await _repository.UpdatePlan(plan);
            return NoContent();
        }

        // DELETE api/<PlanController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _repository.DeletePlan(id);
            return NoContent();
        }
    }
}
