using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IPlanRepo
    {
        Task<IEnumerable<Plan>> GetPlansAsync();
        Task<Plan> GetPlanByIdAsync(int id);
        Task CreatPlanAsync(Plan plan);
        Task UpdatePlanAsync(int planId, Plan plan);
        Task DeletePlanAsync(int planId);
    }
}