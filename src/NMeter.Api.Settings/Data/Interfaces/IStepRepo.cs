using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IStepRepo
    {
        Task<IEnumerable<Step>> GetAllPlanStepsAsync(int planId);
        Task<Step> GetPlanStepAsync(int planId, int stepId);
        Task<bool> CreatePlanStepAync(int planId, Step step);
        Task<bool> UpdatePlanStepAsync(int planId, Step step);
        Task DeletePlanStepAsync(int planId, int stepId);
    }
}