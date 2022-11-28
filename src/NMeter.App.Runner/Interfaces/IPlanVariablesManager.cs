using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Interfaces
{
    public interface IPlanVariablesManager
    {
        void UpdateRequestData(ICollection<PlanGlobalVariable> planVariables, Step step);
        Task RefreshPlanVariablesAsync(HttpResponseMessage response, ICollection<PlanGlobalVariable> planVariables);
    }
}