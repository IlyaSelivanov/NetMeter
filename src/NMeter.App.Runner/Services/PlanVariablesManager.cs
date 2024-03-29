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

            if (planVariables == null || !planVariables.Any())
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
            UpdateBody(planVariables, step);
        }

        private void UpdateBody(ICollection<PlanGlobalVariable> planVariables, Step step)
        {
            if (planVariables == null || !planVariables.Any())
                return;

            if (string.IsNullOrEmpty(step.Body))
                return;

            JsonNode jsonNode = null;

            try
            {
                jsonNode = JsonNode.Parse(step.Body);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex.Message);
                return;
            }

            foreach (var variable in planVariables)
                jsonNode.UpdateJsonNode(new KeyValuePair<string, object>(variable.Name, variable.Value));

            step.Body = jsonNode.ToJsonString();
        }

        private void UpdateUrlParameters(ICollection<PlanGlobalVariable> planVariables, ICollection<UrlParameter> parameters)
        {
            if (planVariables == null || !planVariables.Any())
                return;

            if (parameters == null || !parameters.Any())
                return;

            var intersect = parameters.Join(planVariables,
                p => p.Key,
                pv => pv.Name,
                (p, pv) => new { p.Key, pv.Value });

            if(intersect.Any())
            {
                foreach(var i in intersect)
                {
                    var parameter = parameters
                        .Where(p=> p.Key.Equals(i.Key))
                        .FirstOrDefault();

                    parameter.Value = i.Value;
                }
            }
        }

        private void UpdateHeaders(ICollection<PlanGlobalVariable> planVariables, ICollection<Header> headers)
        {
            if (planVariables == null || !planVariables.Any())
                return;

            if (headers == null || !headers.Any())
                return;

            var intersect = headers.Join(planVariables,
                p => p.Key,
                pv => pv.Name,
                (p, pv) => new { p.Key, pv.Value });

            if(intersect.Any())
            {
                foreach(var i in intersect)
                {
                    var parameter = headers
                        .Where(p=> p.Key.Equals(i.Key))
                        .FirstOrDefault();

                    parameter.Value = i.Value;
                }
            }
        }
    }
}