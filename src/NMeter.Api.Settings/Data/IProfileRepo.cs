using NMeter.Api.Settings.Models;

namespace NMeter.Api.Settings.Data
{
    public interface IProfileRepo
    {
        Task<Profile> GetPlanProfileAsync(int planId);
        Task<bool> CreatePlanProfileAsync(int planId, Profile profile);
        Task<bool> UpdatePlanProfileAsync(int planId, Profile profile);
        Task DeletePlanProfileAsync(int planId);

    }
}