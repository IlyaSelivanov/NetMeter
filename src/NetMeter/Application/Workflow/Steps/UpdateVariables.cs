using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Application.Workflow.Steps
{
    public class UpdateVariables : StepBody
    {
        private List<StepVariable> _stepVariables;

        public object Step { get; set; }
        public List<KeyValueParameter> Variables { get; set; }
        public IRestResponse Response { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Updating variables...");

            Step step = Step as Step;

            if (step.Variables != null && !string.IsNullOrEmpty(step.Variables))
            {
                _stepVariables = JsonConvert.DeserializeObject<List<StepVariable>>(step.Variables);
                SaveOrUpdateVariables();
            }

            return ExecutionResult.Next();
        }

        private void SaveOrUpdateVariables()
        {
            foreach(var stepVariable in _stepVariables)
            {
                string value = null;

                if (stepVariable.Source == (int)VariableSource.Content)
                    value = GetContentVariable(stepVariable);

                if (stepVariable.Source == (int)VariableSource.Headers)
                    value = GetHeaderVariable(stepVariable);

                if (stepVariable.Source == (int)VariableSource.Cookies)
                    value = GetCookiesVariable(stepVariable);

                if (Variables.Any(v => v.Key.Equals(stepVariable.Name)))
                    Variables.FirstOrDefault(v => v.Key.Equals(stepVariable.Name)).Value = value;
                else
                    Variables.Add(new KeyValueParameter { Key = stepVariable.Name, Value = value });
            }
        }

        private string GetCookiesVariable(StepVariable stepVariable)
        {
            return (string)Response.Cookies
                .FirstOrDefault(c => c.Name.Equals(stepVariable.Value))
                ?.Value;
        }

        private string GetHeaderVariable(StepVariable stepVariable)
        {
            return (string)Response.Headers
                .FirstOrDefault(h => h.Name.Equals(stepVariable.Value))
                ?.Value;
        }

        private string GetContentVariable(StepVariable stepVariable)
        {
            JToken jObject = null;
            try
            {
                jObject = JToken.Parse(Response.Content);
            }
            catch(JsonReaderException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return (string)jObject.SelectToken(stepVariable.Value);
        }
    }
}
