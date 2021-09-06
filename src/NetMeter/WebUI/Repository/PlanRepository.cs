using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Repository
{
    public class PlanRepository : IPlanRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/plan"; // const

        public PlanRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreatePlan(Plan plan)
        {
            var response = await _httpService.Post<Plan>(url, plan);

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task DeletePlan(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task<Plan> GetPlanById(int id)
        {
            var response = await _httpService.Get<Plan>($"{url}/{id}");
            return response.Response;
        }

        public async Task<List<Plan>> GetPlans()
        {
            var response = await _httpService.Get<List<Plan>>(url);
            return response.Response;
        }

        public async Task UpdatePlan(Plan plan)
        {
            var response = await _httpService.Put<Plan>(url, plan);

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }
    }
}
