using Application.Helpers;
using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Application.Workflow.Steps
{
    public class CreateRequest : StepBody
    {
        private List<KeyValueParameter> _parameters;
        private List<KeyValueParameter> _headers;

        public object Step { get; set; }
        public List<KeyValueParameter> Variables { get; set; }
        public UserRequest Request { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine("Creating request...");

            Step step = Step as Step;

            if (step is null)
                throw new ApplicationException("CreateRequest step - no step");

            if(step.Parameters != null)
            {
                _parameters = JsonConvert.DeserializeObject<List<KeyValueParameter>>(step.Parameters);
                GetParametersFromVariables();
            }

            if(step.Headers != null)
            {
                _headers = JsonConvert.DeserializeObject<List<KeyValueParameter>>(step.Headers);
                GetHeadersFromVariables();
            }

            Request = BuildRequest(step);

            return ExecutionResult.Next();
        }

        private void GetParametersFromVariables()
        {
            GetVariables(_parameters);
        }

        private void GetHeadersFromVariables()
        {
            GetVariables(_headers);
        }

        private void GetVariables(IEnumerable<KeyValueParameter> collection)
        {
            if (collection == null || collection.Count() == 0)
                return;

            Regex regex = new Regex(@"%(.*?)%");

            foreach (var item in collection)
            {
                MatchCollection mc = regex.Matches(item.Value);
                if(mc.Count > 0)
                {
                    string varName = item.Value.Replace("%", "");
                    item.Value = Variables.Where(v => v.Key.Equals(varName))
                        .FirstOrDefault()
                        .Value;
                }
            }
        }

        private UserRequest BuildRequest(Step step)
        {
            RestRequest request = new RequestBuilder()
                .Resource(step.Resource)
                .Method(step.Method)
                .Headers(_headers)
                .Parameters(_parameters)
                .Body(step.Body)
                .Build();

            return new UserRequest { RestRequest = request, RequestResource = step.Resource };
        }
    }
}
