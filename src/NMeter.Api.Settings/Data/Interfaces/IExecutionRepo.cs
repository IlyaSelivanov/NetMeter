using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IExecutionRepo
    {
        Task<IEnumerable<Execution>> GetAllPlanExecutionsAsync(int planId);
        Task<Execution> GetPlanExecutionAsync(int planId, int executionId);
        Task<bool> CreatePlanExecutionAsync(int planId, Execution execution);
        Task<bool> UpdatePlanExecutionAsync(int planId, Execution execution);
        Task DeletePlanExecutionAsync(int planId, int executionId);
    }
}