using System.Text.Json;
using System.Text.Json.Nodes;
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
            }

            foreach (var @var in planVariables)
            {
                if (string.IsNullOrEmpty(@var.Expression))
                    continue;

                var node = GetNodeByPath(jsonNode, @var.Expression);

                @var.Value = node == null ? string.Empty : node.ToString();
            }
        }

        public void UpdateRequestData(ICollection<PlanGlobalVariable> planVariables, Step step)
        {
            throw new NotImplementedException();
        }

        private bool TryGetNodeArray(JsonNode jsonNode, out JsonArray jsonArray)
        {
            try
            {
                jsonArray = jsonNode.AsArray();
                return true;
            }
            catch (InvalidOperationException)
            {
                jsonArray = new JsonArray();
                return false;
            }
        }

        private JsonNode GetNodeByPath(JsonNode node, string path)
        {
            if (node == null)
                return default;

            if (node.GetPath().Equals(path))
                return node;

            if (TryGetNodeArray(node, out JsonArray jsonArray))
            {
                foreach (var childNode in jsonArray)
                {
                    var result = GetNodeByPath(childNode, path);
                    if (result != null)
                        return result;
                }
            }
            else
            {
                var jsonObject = node as JsonObject;
                if (jsonObject != null)
                {
                    foreach (var obj in jsonObject)
                    {
                        var result = GetNodeByPath(obj.Value, path);
                        if (result != null)
                            return result;
                    }
                }
            }

            return default;
        }
    }
}