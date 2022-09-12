using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IHeaderRepo
    {
        Task<IEnumerable<Header>> GetAllStepHeadersAsync(int stepId);
        Task<Header> GetStepHeaderAsync(int stepId, int headerId);
        Task<bool> CreateStepHeaderAsync(int stepId, Header header);
        Task<bool> UpdateStepHeaderAsync(int stepId, Header header);
        Task DeleteStepHeaderAsync(int stepId, int headerId);
    }
}