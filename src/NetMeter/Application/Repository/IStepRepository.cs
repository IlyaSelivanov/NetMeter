using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IStepRepository
    {
        Task CreateStep(Step step);
        Task DeleteStep(int id);
        Task<Step> GetStepById(int id);
        Task<List<Step>> GetSteps();
        Task UpdateStep(Step step);
    }
}