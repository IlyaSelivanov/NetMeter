using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IPlanRepository
    {
        Task CreatePlan(Plan plan);
        Task DeletePlan(int id);
        Task<Plan> GetPlanById(int id);
        Task<List<Plan>> GetPlans();
        Task UpdatePlan(Plan plan);
    }
}