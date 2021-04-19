using Application.Helpers;
using Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VirtualUser
    {
        private Plan _plan;
        private RestClient _restClient;
        List<RestRequest> _requests;

        public VirtualUser(Plan plan)
        {
            _plan = plan;
            _requests = new List<RestRequest>();
            _restClient = new RestClient(_plan.EndpointUrl);
        }

        public void CreateRequests()
        {
            if (_plan.Steps.Count == 0)
                throw new ApplicationException("Unexpected zero steps number");

            foreach (var step in _plan.Steps)
                _requests.Add(CreateRequest(step));
        }

        public async Task Run()
        {
            if (_requests.Count == 0)
                CreateRequests();

            foreach (var request in _requests)
            {
                var response =  await _restClient.ExecuteAsync(request);
                Console.WriteLine(response.Content);
            }
        }

        private RestRequest CreateRequest(Step step)
        {
            RestRequest request = new RequestBuilder()
                .Resource(step.Resource)
                .Method(step.Method)
                .Headers(step.Headers)
                .Parameters(step.Parameters)
                .Body(step.Body)
                .Build();

            return request;
            
        }
    }
}
