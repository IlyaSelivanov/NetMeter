using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutorController : ControllerBase
    {
        // POST api/<PlanController>
        [HttpPost]
        public async Task<ActionResult> Post(Plan plan)
        {
            ExecutorService service = new ExecutorService(plan);
            service.CreateUsers();
            await service.Execute();

            return NoContent();
        }
    }
}
