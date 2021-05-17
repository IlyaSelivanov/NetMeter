using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Repository
{
    public class ExecutionRepository : IExecutionRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/execution";

        public ExecutionRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateExecution(Execution execution)
        {
            var response = await _httpService.Post<Execution>(url, execution);

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task<IEnumerable<Execution>> GetPlanExecutions(int planId)
        {
            var response = await _httpService.Get<IEnumerable<Execution>>(url);
            return response.Response.Where(e => e.PlanId == planId);
        }
    }
}
