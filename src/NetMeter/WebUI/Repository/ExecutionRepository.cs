using Domain.Entities;
using System;
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
    }
}
