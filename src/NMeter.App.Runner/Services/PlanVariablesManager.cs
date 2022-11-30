using System.Text.Json;
using System.Text.Json.Nodes;
using NMeter.App.Runner.Extensions;
using NMeter.App.Runner.Interfaces;
using NMeter.App.Runner.Models;
using NMeter.App.Runner.Primitives;

namespace NMeter.App.Runner.Services
{
    public class PlanVariablesManager : IPlanVariablesManager
    {
        private readonly ILogger<PlanVariablesManager> _logger;

        public PlanVariablesManager(ILogger<PlanVariablesManager> logger)
        {
            _logger = logger;
        }

        public async Task RefreshPlanVariablesAsync(HttpResponseMessage response,
            ICollection<PlanGlobalVariable> planVariables)
        {
            if (response == null)
                return;

            if (planVariables == null || planVariables.Count == 0)
                return;

            JsonNode jsonNode = null;
            var content = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return;

            try
            {
                jsonNode = JsonNode.Parse(content);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex.Message);
                return;
            }

            foreach (var variable in planVariables)
            {
                if (string.IsNullOrEmpty(variable.Expression))
                    continue;

                var node = jsonNode.GetNodeByPath(variable.Expression);

                variable.Value = node == null ? string.Empty : node.ToString();
            }
        }

        public void UpdateRequestData(ICollection<PlanGlobalVariable> planVariables, Step step)
        {
            UpdateHeaders(planVariables, step.Headers);
            UpdateUrlParameters(planVariables, step.Parameters);
            UpdateBody(planVariables, step.Body);
        }

        private void UpdateBody(ICollection<PlanGlobalVariable> planVariables, string body)
        {
            if(planVariables == null || planVariables.Count == 0)
                return;

            if(string.IsNullOrEmpty(body))
                return;

            JsonNode jsonNode = null; 
            
            try
            {
                jsonNode = JsonNode.Parse(body);
            }
            catch(JsonException ex)
            {
                _logger.LogError(ex.Message);
                return;
            }

            foreach(var variable in planVariables)
                jsonNode.AddUpdateJsonNode(new KeyValuePair<string, object>(variable.Name, variable.Value));

            
        }

        private void UpdateUrlParameters(ICollection<PlanGlobalVariable> planVariables, ICollection<UrlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        private void UpdateHeaders(ICollection<PlanGlobalVariable> planVariables, ICollection<Header> headers)
        {
            throw new NotImplementedException();
        }
    }
}