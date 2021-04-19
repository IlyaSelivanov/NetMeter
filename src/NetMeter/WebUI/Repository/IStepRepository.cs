using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository
{
    public interface IStepRepository
    {
        Task<List<Step>> GetSteps();
        Task<Step> GetStepById(int id);
        Task CreateStep(Step step);
        Task UpdateStep(Step step);
        Task DeleteStep(int id);
    }
}
