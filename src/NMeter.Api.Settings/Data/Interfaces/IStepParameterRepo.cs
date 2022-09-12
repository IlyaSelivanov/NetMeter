using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IStepParameterRepo
    {
        Task<IEnumerable<UrlParameter>> GetAllStepParametersAsync(int stepId);
        Task<UrlParameter> GetStepParameterAsync(int stepId, int parameterId);
        Task<bool> CreateStepParameterAsync(int stepId, UrlParameter parameter);
        Task<bool> UpdateStepParameterAsync(int stepId, UrlParameter parameter);
        Task DeleteStepParameterAsync(int stepId, int parameterId);
    }
}