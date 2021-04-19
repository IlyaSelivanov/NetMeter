using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository
{
    public interface IPlanRepository
    {
        Task<List<Plan>> GetPlans();
        Task<Plan> GetPlanById(int id);
        Task CreatePlan(Plan person);
        Task UpdatePlan(Plan person);
        Task DeletePlan(int id);
    }
}
