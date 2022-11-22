using System.Text.Json.Nodes;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public class PlanVariablesManager : IPlanVariablesManager
    {
        public void RefreshPlanVariables(HttpResponseMessage response, 
            ICollection<PlanGlobalVariable> planVariables)
        {
            var content = response.Content.ReadAsStringAsync().Result;
            var contentNode = JsonNode.Parse(content);
        }

        public void UpdateRequestData(ICollection<PlanGlobalVariable> planVariables, Step step)
        {
            throw new NotImplementedException();
        }
    }
}