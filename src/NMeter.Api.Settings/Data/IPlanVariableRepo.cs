using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data 
{
    public interface IPlanVariableRepo
    {
        Task<IEnumerable<PlanVariable>> GetAllPlanVariablesAsync(int planId);
        Task<PlanVariable> GetPlanVariableAsync(int planId, int variableId);
        Task<bool> CreatePlanVariableAsync(int planId, PlanVariable planVariable);
        Task<bool> UpdatePlanVariableAsync(int planId, PlanVariable planVariable);
        Task DeletePlanVariableAsync(int planId, int variableId);
    }
}