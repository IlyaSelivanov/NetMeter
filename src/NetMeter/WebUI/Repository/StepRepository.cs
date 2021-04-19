using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Services;

namespace WebUI.Repository
{
    public class StepRepository : IStepRepository
    {
        private readonly IHttpService _httpService;
        private string url = "api/step";

        public StepRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateStep(Step step)
        {
            var response = await _httpService.Post<Step>(url, step);

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task DeleteStep(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }

        public async Task<Step> GetStepById(int id)
        {
            var response = await _httpService.Get<Step>($"{url}/{id}");
            return response.Response;
        }

        public async Task<List<Step>> GetSteps()
        {
            var response = await _httpService.Get<List<Step>>(url);
            return response.Response;
        }

        public async Task UpdateStep(Step step)
        {
            var response = await _httpService.Put<Step>(url, step);

            if (!response.Success)
                throw new ApplicationException(await response.GetBody());
        }
    }
}
